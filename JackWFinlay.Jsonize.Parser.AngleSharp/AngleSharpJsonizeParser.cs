using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using JackWFinlay.Jsonize.Abstractions.Configuration;
using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Parser.AngleSharp
{
    public class AngleSharpJsonizeParser : IJsonizeParser
    {
        private readonly IBrowsingContext _browsingContext;
        private readonly JsonizeParserConfiguration _jsonizeParserConfiguration = new JsonizeParserConfiguration();
        
        /// <summary>
        /// Creates an <see cref="AngleSharpJsonizeParser"/> instance with default <see cref="BrowsingContext"/>,
        /// <see cref="Configuration"/>, and <see cref="JsonizeParserConfiguration"/>.
        /// </summary>
        public AngleSharpJsonizeParser()
        {
            IConfiguration configuration = new Configuration().WithJs().WithCss();
            _browsingContext = new BrowsingContext(configuration);
        }
        
        /// <summary>
        /// Creates an <see cref="AngleSharpJsonizeParser"/> instance with the given <see cref="BrowsingContext"/>,
        /// with the default <see cref="JsonizeParserConfiguration"/>.
        /// </summary>
        /// <param name="browsingContext">The <see cref="BrowsingContext"/> to initialize with.</param>
        public AngleSharpJsonizeParser(IBrowsingContext browsingContext) : this()
        {
            _browsingContext = browsingContext;
        }

        /// <summary>
        /// Creates an <see cref="AngleSharpJsonizeParser"/> instance with the given <see cref="JsonizeConfiguration"/>,
        /// with the default <see cref="Configuration"/> and <see cref="BrowsingContext"/>.
        /// </summary>
        /// <param name="jsonizeParserConfiguration">Tbe <see cref="JsonizeParserConfiguration"/> to initialize with.</param>
        public AngleSharpJsonizeParser(JsonizeParserConfiguration jsonizeParserConfiguration) : this()
        {
            _jsonizeParserConfiguration = jsonizeParserConfiguration;
        }

        /// <summary>
        /// Creates an <see cref="AngleSharpJsonizeParser"/> instance with the given <see cref="JsonizeConfiguration"/>,
        /// and <see cref="BrowsingContext"/>.
        /// </summary>
        /// <param name="browsingContext">The <see cref="BrowsingContext"/> to initialize with.</param>
        /// <param name="jsonizeParserConfiguration">Tbe <see cref="JsonizeParserConfiguration"/> to initialize with.</param>
        public AngleSharpJsonizeParser(IBrowsingContext browsingContext, JsonizeParserConfiguration jsonizeParserConfiguration) : this()
        {
            _browsingContext = browsingContext;
            _jsonizeParserConfiguration = jsonizeParserConfiguration;
        }
        
        public async Task<JsonizeNode> ParseAsync(string htmlString)
        {
            IDocument document = await _browsingContext.OpenAsync(req => req.Content(htmlString));

            var documentNode = document.GetRoot();

            JsonizeNode parentNode = new JsonizeNode()
            {
                NodeType = "Document",
                Attributes = new Dictionary<string, object>()
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

            List<JsonizeNode> childNodes = new List<JsonizeNode>();
            
            foreach (INode childNode in element.Descendents())
            {
                // Only process direct descendents.
                // Unfortunately elements.Descendents returns the entire tree of descendents.
                // This is a problem as we need to use it to get children as an INode object,
                // so we can get all the Nodes (including doctype, comments, text, etc.),
                // not just elements (html, body, p, etc.).
                if (!childNode.Parent.Equals(element))
                {
                    continue;
                }

                NodeType nodeType = childNode.NodeType;
                string innerText = GetInnerText(childNode);

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

                JsonizeNode childJsonizeNode = new JsonizeNode
                {
                    NodeType = nodeType.ToString(),
                    Tag = childNode.NodeName.ToLowerInvariant(),
                    Text = innerText,
                    Attributes = attributes
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
            string innerText = null;

            switch (element.NodeType)
            {
                case NodeType.Comment:
                    innerText = element.TextContent;
                    break;
                default:
                    innerText = element.ChildNodes.OfType<IText>().Select(m => m.Text).FirstOrDefault();
                    break;
            }

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
            IDictionary<string, object> attributes = new Dictionary<string, object>();

            foreach (IAttr attribute in attributesList)
            {
                if (attribute.Name.Equals("class", StringComparison.InvariantCultureIgnoreCase)
                    && _jsonizeParserConfiguration.ClassAttributeHandling == ClassAttributeHandling.Array)
                {
                    IEnumerable<string> classList = ParseClassList(attribute);
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
            string trimmed = attribute.Value.Trim();
            List<string> classList = trimmed.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
            return classList;
        }
    }
}