using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Io;
using Jsonize.Abstractions.Configuration;
using Jsonize.Abstractions.Interfaces;
using Jsonize.Abstractions.Models;

namespace Jsonize.Parser
{
    public class JsonizeParser : IJsonizeParser
    {
        private readonly IBrowsingContext _browsingContext;
        private readonly JsonizeParserConfiguration _jsonizeParserConfiguration = new JsonizeParserConfiguration();
        private readonly HtmlParserOptions _htmlParserOptions = new HtmlParserOptions();

        /// <summary>
        /// Creates an <see cref="JsonizeParser"/> instance with default <see cref="BrowsingContext"/>,
        /// <see cref="Configuration"/>, <see cref="HtmlParserOptions"/>, and <see cref="JsonizeParserConfiguration"/>.
        /// </summary>
        public JsonizeParser()
        {
            var configuration = new Configuration().WithJs().WithCss();
            _browsingContext = new BrowsingContext(configuration);
        }
        
        /// <summary>
        /// Creates an <see cref="JsonizeParser"/> instance with the given <see cref="BrowsingContext"/>,
        /// with the default <see cref="JsonizeParserConfiguration"/>.
        /// </summary>
        /// <param name="browsingContext">The <see cref="BrowsingContext"/> to initialize with.</param>
        public JsonizeParser(IBrowsingContext browsingContext)
        {
            _browsingContext = browsingContext;
        }
        
        /// <summary>
        /// Creates an <see cref="JsonizeParser"/> instance with the given <see cref="BrowsingContext"/>,
        /// and <see cref="HtmlParserOptions"/>,
        /// with the default <see cref="JsonizeParserConfiguration"/>.
        /// </summary>
        /// <param name="browsingContext">The <see cref="BrowsingContext"/> to initialize with.</param>
        /// <param name="htmlParserOptions">The <see cref="HtmlParserOptions"/> to initialize the parser with.</param>
        public JsonizeParser(IBrowsingContext browsingContext, HtmlParserOptions htmlParserOptions)
        {
            _browsingContext = browsingContext;
            _htmlParserOptions = htmlParserOptions;
        }

        /// <summary>
        /// Creates an <see cref="JsonizeParser"/> instance with the given <see cref="JsonizeConfiguration"/>,
        /// with the default <see cref="Configuration"/>, <see cref="HtmlParserOptions"/>, and <see cref="BrowsingContext"/>.
        /// </summary>
        /// <param name="jsonizeParserConfiguration">Tbe <see cref="JsonizeParserConfiguration"/> to initialize with.</param>
        public JsonizeParser(JsonizeParserConfiguration jsonizeParserConfiguration)
        {
            _jsonizeParserConfiguration = jsonizeParserConfiguration;
        }

        /// <summary>
        /// Creates an <see cref="JsonizeParser"/> instance with the given <see cref="JsonizeConfiguration"/>,
        /// and <see cref="BrowsingContext"/>. The <see cref="HtmlParserOptions"/> will be the default.
        /// </summary>
        /// <param name="browsingContext">The <see cref="BrowsingContext"/> to initialize with.</param>
        /// <param name="jsonizeParserConfiguration">Tbe <see cref="JsonizeParserConfiguration"/> to initialize with.</param>
        public JsonizeParser(IBrowsingContext browsingContext, JsonizeParserConfiguration jsonizeParserConfiguration)
        {
            _browsingContext = browsingContext;
            _jsonizeParserConfiguration = jsonizeParserConfiguration;
        }

        /// <summary>
        /// Creates an <see cref="JsonizeParser"/> instance with the given <see cref="JsonizeConfiguration"/>,
        /// and <see cref="BrowsingContext"/>.
        /// </summary>
        /// <param name="browsingContext">The <see cref="BrowsingContext"/> to initialize with.</param>
        /// <param name="htmlParserOptions">The <see cref="HtmlParserOptions"/> to initialize with.</param>
        /// <param name="jsonizeParserConfiguration">Tbe <see cref="JsonizeParserConfiguration"/> to initialize with.</param>
        public JsonizeParser(IBrowsingContext browsingContext, 
            HtmlParserOptions htmlParserOptions, 
            JsonizeParserConfiguration jsonizeParserConfiguration)
        {
            _browsingContext = browsingContext;
            _htmlParserOptions = htmlParserOptions;
            _jsonizeParserConfiguration = jsonizeParserConfiguration;
        }
        
        public async Task<JsonizeNode> ParseAsync(string htmlString)
        {
            var htmlParser = new HtmlParser(_htmlParserOptions, _browsingContext);
            var document = await htmlParser.ParseDocumentAsync(htmlString, default);

            return await ParseDocument(document);
        }

        public async Task<JsonizeNode> ParseAsync(Stream htmlStream)
        {
            var htmlParser = new HtmlParser(_htmlParserOptions, _browsingContext);
            var document = await htmlParser.ParseDocumentAsync(htmlStream, default);

            return await ParseDocument(document);
        }

        private async Task<JsonizeNode> ParseDocument(IHtmlDocument document)
        {
            var documentNode = document.GetRoot();

            var parentNode = new JsonizeNode()
            {
                NodeType = "Document",
                Attr = new Dictionary<string, object>()
            };

            await GetChildNodesAsync(parentNode, documentNode);

            return parentNode;
        }

        private async Task GetChildNodesAsync(JsonizeNode parentNode, INode element)
        {
            // No point trying to parse children that don't exist.
            if (!element.Descendents().Any())
            {
                return;
            }

            var childNodes = new List<JsonizeNode>();
            
            foreach (var childNode in element.ChildNodes)
            {
                var nodeType = childNode.NodeType;
                var innerText = GetInnerText(childNode);

                if (_jsonizeParserConfiguration.EmptyTextNodeHandling == EmptyTextNodeHandling.Ignore
                    && nodeType == NodeType.Text
                    && string.IsNullOrWhiteSpace(innerText)
                )
                {
                    continue;
                }

                IDictionary<string, object> attributes = new Dictionary<string, object>();

                if (childNode is IElement childElement)
                {
                    attributes = GetAttributes(childElement.Attributes?.ToList());
                }

                var childJsonizeNode = new JsonizeNode
                {
                    NodeType = nodeType.ToString(),
                    Tag = childNode.NodeName.ToLowerInvariant(),
                    Text = innerText,
                    Attr = attributes
                };

                if (childNode.HasChildNodes)
                {
                    await GetChildNodesAsync(childJsonizeNode, childNode);
                }

                childNodes.Add(childJsonizeNode);
            }

            parentNode.Children = childNodes;
        }

        private string GetInnerText(INode element)
        {
            var innerText = element.NodeType switch
            {
                NodeType.Comment => element.TextContent,
                _ =>  element.ChildNodes.OfType<IText>().Select(m => m.Text).FirstOrDefault()
            };

            innerText = _jsonizeParserConfiguration.TextTrimHandling == TextTrimHandling.Trim
                ? innerText?.Trim()
                : innerText;

            // Return innerText if it has a value, else we have to check the EmptyTextNodeHandling setting.
            if (!string.IsNullOrWhiteSpace(innerText))
            {
                return innerText;
            }

            // innerText is null here, if we still want to include the text node, we return one with an empty string in it.
            innerText = _jsonizeParserConfiguration.EmptyTextNodeHandling == EmptyTextNodeHandling.Include 
                ? string.Empty 
                : null;

            return innerText;
        }

        private IDictionary<string, object> GetAttributes(IEnumerable<IAttr> attributesList)
        {
            var attributes = new Dictionary<string, object>();

            foreach (var attribute in attributesList)
            {
                if (attribute.Name.Equals("class", StringComparison.InvariantCultureIgnoreCase)
                    && _jsonizeParserConfiguration.ClassAttributeHandling == ClassAttributeHandling.Array)
                {
                    var classList = ParseClassList(attribute);
                    attributes.Add(attribute.Name, classList);
                }
                else
                {
                    attributes.Add(attribute.Name, attribute.Value);
                }
            }

            return attributes;
        }

        private static IEnumerable<string> ParseClassList(IAttr attribute)
        {
            var trimmed = attribute.Value.Trim();
            var classList = trimmed.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
            return classList;
        }
    }
}