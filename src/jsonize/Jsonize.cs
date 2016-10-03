using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using static System.Net.WebUtility;

namespace JackWFinlay.Jsonize
{
    public class Jsonize
    {
        private HtmlDocument _htmlDoc;
        private bool showEmptyTextNodes;

        /// <summary>
        /// Creates a new Jsonize object.
        /// </summary>
        public Jsonize()
        {
            _htmlDoc = new HtmlDocument();
        }

        /// <summary>
        /// Constructs a Jsonize object with the supplied HtmlDocument.
        /// </summary>
        /// <param name="htmlDoc"></param>
        public Jsonize(HtmlDocument htmlDoc)
        {
            _htmlDoc = htmlDoc;
            
        }

        public Jsonize(string html)
        {
            _htmlDoc = new HtmlDocument();
            _htmlDoc.LoadHtml(html);
        }

        /// <summary>
        /// Sets whether or not to show empty text nodes. Default is False.
        /// </summary>
        /// <param name="showEmptyTextNodes">Whether or not to show empty text nodes</param>
        public void ShowEmptyTextNodes(bool showEmptyTextNodes)
        {
            this.showEmptyTextNodes = showEmptyTextNodes;
        }
        
        /// <summary>
        /// Resturns a JObject of the HTML document.
        /// </summary>
        /// <returns>The JSON representation of an HTML document as a JObject.</returns>
        public JObject ParseHtmlAsJson()
        {
            Node parentNode = new Node();
            HtmlNode parentHtmlNode = _htmlDoc.DocumentNode;

            parentNode.node = parentHtmlNode.NodeType.ToString();
            GetChildren(parentNode, parentHtmlNode);

            JsonSerializer _jsonWriter = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return JObject.FromObject(parentNode, _jsonWriter);
        }

        /// <summary>
        /// Returns a JSON string representation of the HTML document.
        /// </summary>
        /// <returns>The string representation of the HTML document.</returns>
        public string ParseHtmlAsJsonString()
        {
            return ParseHtmlAsJson().ToString() ;
        }

        private void GetChildren(Node parentNode, HtmlNode parentHtmlNode)
        {
            foreach (HtmlNode htmlNode in parentHtmlNode.ChildNodes)
            {
                bool addToParent = false;
                Node childNode = new Node();

                if (parentNode.child == null)
                {
                    parentNode.child = new List<Node>();
                }

                if (!htmlNode.Name.StartsWith("#"))
                {
                    childNode.tag = htmlNode.Name;
                    addToParent = true;
                }


                string trimmedInnerText = HtmlDecode(htmlNode.InnerText.Trim());
                if (showEmptyTextNodes || !String.IsNullOrWhiteSpace(trimmedInnerText))
                {
                    if (!htmlNode.HasChildNodes)
                    {   
                        childNode.text = trimmedInnerText;
                    }

                    childNode.node = htmlNode.NodeType.ToString();
                    addToParent = true;
                }

                if (htmlNode.HasAttributes)
                {
                    if (childNode.attr == null)
                    {
                        childNode.attr = new System.Dynamic.ExpandoObject();
                    }

                    AddAttributes(htmlNode, childNode);
                    addToParent = true;
                }

                if (htmlNode.HasChildNodes)
                {
                    GetChildren(childNode, htmlNode);
                    addToParent = true;
                }

                if (addToParent)
                {
                    parentNode.child.Add(childNode);
                }
            }

        }

        private static bool IsEmptyChildNode(Node childNode)
        {
            return (!object.ReferenceEquals(childNode.attr, null) &&
                    !object.ReferenceEquals(childNode.child, null) &&
                    !object.ReferenceEquals(childNode.tag, null) &&
                    !object.ReferenceEquals(childNode.text, null)
                   );
        }

        private void AddAttributes(HtmlNode htmlNode, Node childNode)
        {
            IDictionary<string, object> attributeDict = childNode.attr as IDictionary<string, object>;

            List<HtmlAttribute> attributes = htmlNode.Attributes.ToList<HtmlAttribute>();
            foreach (HtmlAttribute attribute in attributes)
            {
                if (attribute.Name.Equals("class"))
                {
                    string[] classes = attribute.Value.Split(' ');
                    List<string> classList = new List<string>();
                    foreach (string @class in classes)
                    {
                        classList.Add(@class);
                    }
                    attributeDict["class"] = classList;
                }
                else
                {
                    attributeDict[attribute.Name] = attribute.Value;
                }
            }
        }

    }
}
