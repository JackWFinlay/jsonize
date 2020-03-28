using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
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
                Attributes = GetAttributes(documentNode.Attributes),
                Children = new List<JsonizeNode>()
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

            foreach (IElement childElement in element.Children)
            {
                JsonizeNode childJsonizeNode = new JsonizeNode();
                
                childJsonizeNode.Node = childElement.NodeType.ToString();
                childJsonizeNode.Tag = childElement.TagName;
                childJsonizeNode.Text = GetInnerText(childElement);
                childJsonizeNode.Attributes = GetAttributes(childElement.Attributes);

                if (childElement.HasChildNodes)
                {
                    childJsonizeNode.Children = new List<JsonizeNode>();
                    await GetChildNodesAsync(childJsonizeNode, childElement);
                }

                parentNode.Children.Add(childJsonizeNode);
            }
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