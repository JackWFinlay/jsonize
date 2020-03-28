using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using JackWFinlay.Jsonize.Abstractions.Configuration;
using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Abstractions.Models;
using JackWFinlay.Jsonize.Configuration;

namespace JackWFinlay.Jsonize.Parser.AngleSharp
{
    public class AngleSharpJsonizeParser : IJsonizeParser
    {
        private readonly IBrowsingContext _browsingContext;
        private readonly JsonizeConfiguration _jsonizeConfiguration;
        
        public AngleSharpJsonizeParser()
        {
            IConfiguration configuration = new global::AngleSharp.Configuration().WithJs().WithCss();
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
            // No point trying to parse children that don't exist.
            if (!element.Children.Any())
            {
                return;
            }

            List<JsonizeNode> childNodes = new List<JsonizeNode>();
            
            foreach (IElement childElement in element.Children)
            {
                JsonizeNode childJsonizeNode = new JsonizeNode();
                
                childJsonizeNode.Node = childElement.NodeType.ToString().ToLowerInvariant();
                childJsonizeNode.Tag = childElement.TagName.ToLowerInvariant();
                childJsonizeNode.Text = GetInnerText(childElement);
                childJsonizeNode.Attributes = GetAttributes(childElement.Attributes);

                if (childElement.HasChildNodes)
                {
                    await GetChildNodesAsync(childJsonizeNode, childElement);
                }

                childNodes.Add(childJsonizeNode);
            }

            parentNode.Children = childNodes;
        }

        private string GetInnerText(IElement element)
        {
            string innerText = _jsonizeConfiguration.TextTrimHandling == TextTrimHandling.Trim
                ? element.GetInnerText().Trim()
                : element.GetInnerText();
            
            // Return innerText if it has a value, else we have to check the EmptyTextNodeHandling setting.
            if (!string.IsNullOrWhiteSpace(innerText))
            {
                return innerText;
            }

            // innerText is null here, if we still want to include the text node, we return one with an empty string in it.
            innerText = _jsonizeConfiguration.EmptyTextNodeHandling == EmptyTextNodeHandling.Include 
                ? string.Empty 
                : null;

            return innerText;
        }

        private IDictionary<string, object> GetAttributes(INamedNodeMap attributeMap)
        {
            IDictionary<string, object> attributes = new Dictionary<string, object>();

            foreach (var attribute in attributeMap)
            {
                if (attribute.Name.Equals("class", StringComparison.InvariantCultureIgnoreCase)
                    && _jsonizeConfiguration.ClassAttributeHandling == ClassAttributeHandling.Array)
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