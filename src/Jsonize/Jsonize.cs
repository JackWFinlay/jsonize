using AngleSharp;
using AngleSharp.Attributes;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebUtility;
using AngleSharp.Dom;

namespace JackWFinlay.Jsonize
{
    public class Jsonize
    {
        private  IDocument _htmlDoc;
        private HtmlParser _htmlParser;
        private EmptyTextNodeHandling _emptyTextNodeHandling;
        private NullValueHandling _nullValueHandling;
        private TextTrimHandling _textTrimHandling;
        private ClassAttributeHandling _classAttributeHandling;

        /// <summary>
        /// Gets or sets the <see cref="HtmlDocument"/> for the <see cref="Jsonize"/> object.
        /// </summary>
        /// <value><see cref="HtmlDocument"/></value>
        public IDocument HtmlDoc
        {
            //get { return _htmlDoc ?? new Document(); }
            get { return _htmlDoc; }
            set { _htmlDoc = value; }
        }

        /// <summary>
        /// Gets or sets the Html <see cref="string"/> for the <see cref="Jsonize"/> object.
        /// </summary>
        /// <value>Html <see cref="string"/></value>
        public string HtmlString
        {
            get { return _htmlDoc.ToString() ?? ""; }
            //set { ; }
        }

        /// <summary>
        /// Creates a new <see cref="Jsonize"/> object.
        /// </summary>
        public Jsonize()
        {
            // Fix #26: Form tag parsed as a text node.
            //HtmlNode.ElementsFlags.Remove("form");
            //_htmlDoc = _htmlDoc ?? new HtmlDocument();
            _htmlParser = new HtmlParser();
            _emptyTextNodeHandling = JsonizeConfiguration.DefaultEmptyTextNodeHandling;
            _nullValueHandling = JsonizeConfiguration.DefaultNullValueHandling;
            _textTrimHandling = JsonizeConfiguration.DefaultTextTrimHandling;
            _classAttributeHandling = JsonizeConfiguration.DefaultClassAttributeHandling;
        }

        /// <summary>
        /// Constructs a <see cref="Jsonize"/> object with the supplied <see cref="IDocument"/> .
        /// </summary>
        /// <param name="htmlDoc">IDocument loaded with the html string</param>
        public Jsonize(IDocument htmlDoc) : this()
        {
            _htmlDoc = htmlDoc;
        }

        /// <summary>
        /// Constructs a <see cref="Jsonize"/> object with the supplied HTML <see cref="string"/>.
        /// </summary>
        /// <param name="html">Html string</param>
        public Jsonize(string html) : this()
        {
            _htmlDoc = _htmlParser.Parse(html);
            // _htmlDoc = new HtmlDocument();
            // _htmlDoc.LoadHtml(html);
        }

        /// <summary>
        /// Send an HTTP GET request to fetch HTML and then construct a <see cref="Jsonize"/> object.
        /// </summary>
        /// <param name="httpUrl">Url for HTTP GET request</param>
        /// <returns>Jsonize object constructed with the response body</returns>
        public static async Task<Jsonize> FromHttpUrl(string httpUrl)
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(httpUrl))
            {
                var html = await response.Content.ReadAsStringAsync();
                return new Jsonize(html);
            }
        }

        /// <summary>
        /// Construct a Jsonize object from an html <see cref="string"/>.
        /// </summary>
        /// <param name="htmlString">The html document as a <see cref="string"/></param>
        /// <returns>Jsonize object constructed from the html <see cref="string"/></returns>
        public static Jsonize FromHtmlString(string htmlString)
        {
            return new Jsonize(htmlString);
        }

        /// <summary>
        /// Construct a Jsonize object from an <see cref="HtmlDocument"/>.
        /// </summary>
        /// <param name="htmlDoc">The <see cref="HtmlDocument"/> to construct the <see cref="Jsonize"/> object with.</param>
        /// <returns>Jsonize object constructed from the html <see cref="string"/></returns>
        public static Jsonize FromHtmlDocument(IDocument htmlDoc)
        {
            return new Jsonize(htmlDoc);
        }

        /// <summary>
        /// Returns a <see cref="JObject"/> of the HTML document.
        /// </summary>
        /// <returns>The JSON representation of an HTML document as a <see cref="JObject"/>.</returns>
        public JObject ParseHtmlAsJson()
        {
            JsonizeNode parentJsonizeNode = ParseHtmlAsJsonizeNode();

            JsonSerializer jsonWriter = new JsonSerializer
            {
                NullValueHandling = (Newtonsoft.Json.NullValueHandling)_nullValueHandling
            };
            return JObject.FromObject(parentJsonizeNode, jsonWriter);
        }

        /// <summary>
        /// Returns a <see cref="JsonizeNode"/> of the HTML document.
        /// </summary>
        /// <returns>The JSON representation of an HTML document as a <see cref="JsonizeNode"/>.</returns>
        public JsonizeNode ParseHtmlAsJsonizeNode()
        {
            JsonizeNode parentJsonizeNode = new JsonizeNode();
            INode parentHtmlNode = _htmlDoc.DocumentElement;

            parentJsonizeNode.Node = parentHtmlNode.NodeType.ToString();
            GetChildren(parentJsonizeNode, parentHtmlNode);

            parentJsonizeNode = SetDocumentAndDoctypeNodes(parentJsonizeNode);

            return parentJsonizeNode;
        }

        /// <summary>
        /// Resturns a <see cref="JObject"/> of the HTML document.
        /// </summary>
        /// <returns>The JSON representation of an HTML document as a <see cref="JObject"/>.</returns>
        public JObject ParseHtmlAsJson(JsonizeConfiguration jsonizeConfiguration)
        {
            ApplyConfiguration(jsonizeConfiguration);

            return ParseHtmlAsJson();
        }

        /// <summary>
        /// Returns a JSON string representation of the HTML document.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the HTML document.</returns>
        public string ParseHtmlAsJsonString()
        {
            return ParseHtmlAsJson().ToString() ;
        }

        /// <summary>
        /// Returns a JSON string representation of the HTML document with the settings supplied in the <see cref="JsonizeConfiguration"/>.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the HTML document.</returns>
        public string ParseHtmlAsJsonString(JsonizeConfiguration jsonizeConfiguration)
        {
            ApplyConfiguration(jsonizeConfiguration);
            return ParseHtmlAsJson().ToString();
        }

        private void ApplyConfiguration(JsonizeConfiguration jsonizeConfiguration)
        {
            if (jsonizeConfiguration._emptyTextNodeHandling != null)
            {
                _emptyTextNodeHandling = jsonizeConfiguration.EmptyTextNodeHandling;
            }

            if (jsonizeConfiguration._nullValueHandling != null)
            {
                _nullValueHandling = jsonizeConfiguration.NullValueHandling;
            }

            if (jsonizeConfiguration._textTrimHandling != null)
            {
                _textTrimHandling = jsonizeConfiguration.TextTrimHandling;
            }

            if (jsonizeConfiguration._classAttributeHandling != null)
            {
                _classAttributeHandling = jsonizeConfiguration.ClassAttributeHandling;
            }
        }

        private void GetChildren(JsonizeNode parentJsonizeNode, INode parentHtmlNode)
        {
            foreach (INode node in parentHtmlNode.ChildNodes)
            {
                bool addToParent = false;
                JsonizeNode childJsonizeNode = new JsonizeNode();

                if (parentJsonizeNode.Children == null)
                {
                    parentJsonizeNode.Children = new List<JsonizeNode>();
                }

                switch (node.NodeType)
                {
                    case NodeType.Attribute:
                        AddAttribute(parentJsonizeNode, node);
                        break;
                    case NodeType.DocumentType:
                        childJsonizeNode.Node = GetNodeTypeName(node);
                        throw new Exception("in doctype");
                        //break;
                    case NodeType.Text:
                        addToParent = AddTextNode(node, addToParent, childJsonizeNode);
                        break;
                    case NodeType.Comment:
                        childJsonizeNode.Node = GetNodeTypeName(node);
                        childJsonizeNode.Text = node.TextContent;
                        break;
                    default:
                        childJsonizeNode.Node = GetNodeTypeName(node);
                        childJsonizeNode.Tag = node.NodeName.ToLowerInvariant();
                        addToParent = true;
                        break;

                }

                if (node.HasChildNodes)
                {
                    GetChildren(childJsonizeNode, node);
                    addToParent = true;
                }

                if (addToParent)
                {
                    parentJsonizeNode.Children.Add(childJsonizeNode);
                }


            }

            if (parentJsonizeNode.Children.Count == 0)
            {
                parentJsonizeNode.Children = null;
            }
        }

        private bool AddTextNode(INode node, bool addToParent, JsonizeNode childJsonizeNode)
        {
            childJsonizeNode.Node = GetNodeTypeName(node);
            string innerText = HtmlDecode(_textTrimHandling == TextTrimHandling.Trim ? node.TextContent.Trim() : node.TextContent);
            if (_emptyTextNodeHandling == EmptyTextNodeHandling.Include || !string.IsNullOrWhiteSpace(innerText))
            {
                if (!node.HasChildNodes)
                {
                    childJsonizeNode.Text = innerText.Equals("") ? null : innerText;
                }

                addToParent = true;
            }

            return addToParent;
        }

        private static string GetNodeTypeName(INode node)
        {
            return Enum.GetName(typeof(NodeType), node.NodeType);
        }

        private static void AddAttribute(JsonizeNode parentJsonizeNode, INode node)
        {
            if (parentJsonizeNode.Attributes == null)
            {
                parentJsonizeNode.Attributes = new List<JsonizeHtmlAttribute>();
            }

            JsonizeHtmlAttribute jsonizeHtmlAttribute = new JsonizeHtmlAttribute(node.NodeName, node.NodeValue);
            parentJsonizeNode.Attributes.Add(jsonizeHtmlAttribute);
        }

        private JsonizeNode SetDocumentAndDoctypeNodes(JsonizeNode jsonizeNode)
        {
            JsonizeNode documentNode = new JsonizeNode();

            // If has <!DOCTYPE> element
            if (_htmlDoc.FirstChild.NodeType == NodeType.DocumentType)
            {
                JsonizeNode doctypeNode = new JsonizeNode();
                doctypeNode.Node = GetNodeTypeName(_htmlDoc.Doctype);
                doctypeNode.Tag = _htmlDoc.Doctype.NodeName;
                jsonizeNode.Children.Insert(0, doctypeNode);
            }

            documentNode.Node = "Document";
            documentNode.Children = new List<JsonizeNode>();
            documentNode.Children.Add(jsonizeNode);
            return documentNode;
        }

        // Deprecated.
        // private void AddAttributes(INode htmlNode, JsonizeNode childJsonizeNode)
        // {
        //     // Gets previously added
        //     IDictionary<string, object> attributeDict = childJsonizeNode.Attributes;

        //     List<HtmlAttribute> attributes = htmlNode.Attributes.ToList();
        //     foreach (HtmlAttribute attribute in attributes)
        //     {
        //         if (attribute.Name.Equals("class") && _classAttributeHandling == ClassAttributeHandling.Array)
        //         {
        //             string[] classes = attribute.Value.Split(' ');
        //             List<string> classList = classes.ToList();
        //             attributeDict["class"] = classList;
        //         }
        //         else
        //         {
        //             attributeDict[attribute.Name] = attribute.Value;
        //         }
        //     }
        // }

    }
}
