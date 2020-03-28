using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Text;
using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Abstractions.Models;
using JackWFinlay.Jsonize.Configuration;

namespace JackWFinlay.Jsonize.Parser.AngleSharpParser
{
    public class AngleSharpJsonizeParser : IJsonizeParser
    {
        private IBrowsingContext _browsingContext;
        private JsonizeConfiguration _jsonizeConfiguration;
        
        public AngleSharpJsonizeParser()
        {
            IConfiguration configuration = new AngleSharp.Configuration().WithJs().WithCss();
            _browsingContext = new BrowsingContext(configuration);

            _jsonizeConfiguration = new JsonizeConfiguration();
        }
        
        public AngleSharpJsonizeParser(IBrowsingContext browsingContext) : this()
        {
            _browsingContext = browsingContext;
        }

        public AngleSharpJsonizeParser(IConfiguration configuration) : this()
        {
            _browsingContext = new BrowsingContext(configuration);
        }

        public AngleSharpJsonizeParser(JsonizeConfiguration jsonizeConfiguration) : this()
        {
            _jsonizeConfiguration = jsonizeConfiguration;
        }

        public async Task<JsonizeNode> ParseAsync(string htmlString)
        {
            IDocument document = await _browsingContext.OpenAsync(req => req.Content(htmlString));

            var documentNode = document.DocumentElement;

            JsonizeNode parentNode = new JsonizeNode()
            {
                Node = documentNode.NodeName,
                Attributes = GetAttributes(documentNode.Attributes)
            };

            await GetChildNodesAsync(parentNode, documentNode);

            return parentNode;
        }

        private async Task GetChildNodesAsync(JsonizeNode parentNode, IElement element)
        {
            List<JsonizeNode> childJsonizeNodes = new List<JsonizeNode>();
                
            // No point trying to parse children that don't exist.
            if (!element.Children.Any())
            {
                parentNode.Children = null;
                return;
            }
            
            JsonizeNode childJsonizeNode = new JsonizeNode();

            foreach (IElement childElement in element.Children)
            {
                childJsonizeNode.Node = element.NodeType.ToString();
                childJsonizeNode.Tag = element.TagName;
                childJsonizeNode.Text = GetInnerText(element);
                childJsonizeNode.Attributes = GetAttributes(element.Attributes);

                if (childElement.HasChildNodes)
                {
                    await GetChildNodesAsync(childJsonizeNode, childElement);
                }

                childJsonizeNodes.Add(childJsonizeNode);
            }

            parentNode.Children = childJsonizeNodes;
        }

        private string GetInnerText(IElement element)
        {
            string innerText = _jsonizeConfiguration.TextTrimHandling == TextTrimHandling.Trim
                ? element.TextContent.Trim()
                : element.TextContent;

            if (_jsonizeConfiguration.EmptyTextNodeHandling != EmptyTextNodeHandling.Include
                && string.IsNullOrWhiteSpace(innerText))
            {
                return innerText;
            }
            
            if (!element.HasChildNodes)
            {
                innerText = innerText.Equals("") ? null : innerText;
            }

            return innerText;
        }

        private IDictionary<string, object> GetAttributes(INamedNodeMap attributeMap)
        {
            IDictionary<string, object> attributes = new Dictionary<string, object>();

            foreach (var attribute in attributeMap)
            {
                if (attribute.Name.Equals("class") && _jsonizeConfiguration.ClassAttributeHandling == ClassAttributeHandling.Array)
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

        private IEnumerable<string> ParseClassList(IAttr attribute)
        {
            string trimmed = attribute.Value.Trim();
            List<string> classList = trimmed.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
            return classList;
        }
    }
}