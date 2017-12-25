using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebUtility;

namespace JackWFinlay.Jsonize
{
    public class Jsonize
    {
        private HtmlDocument _htmlDoc;
        private EmptyTextNodeHandling _emptyTextNodeHandling;
        private NullValueHandling _nullValueHandling;
        private TextTrimHandling _textTrimHandling;
        private ClassAttributeHandling _classAttributeHandling;

        /// <summary>
        /// Gets or sets the <see cref="HtmlDocument"/> for the <see cref="Jsonize"/> object.
        /// </summary>
        /// <value><see cref="HtmlDocument"/></value>
        public HtmlDocument HtmlDoc
        {
            get { return _htmlDoc ?? new HtmlDocument(); }
            set { _htmlDoc = value; }
        }

        /// <summary>
        /// Gets or sets the Html <see cref="string"/> for the <see cref="Jsonize"/> object.
        /// </summary>
        /// <value>Html <see cref="string"/></value>
        public string HtmlString
        {
            get { return _htmlDoc.ToString() ?? ""; }
            set { _htmlDoc.LoadHtml(value); }
        }

        /// <summary>
        /// Creates a new <see cref="Jsonize"/> object.
        /// </summary>
        public Jsonize()
        {
            // Fix #26: Form tag parsed as a text node.
            HtmlNode.ElementsFlags.Remove("form");
            _htmlDoc = _htmlDoc ?? new HtmlDocument();
            _emptyTextNodeHandling = JsonizeConfiguration.DefaultEmptyTextNodeHandling;
            _nullValueHandling = JsonizeConfiguration.DefaultNullValueHandling;
            _textTrimHandling = JsonizeConfiguration.DefaultTextTrimHandling;
            _classAttributeHandling = JsonizeConfiguration.DefaultClassAttributeHandling;
        }

        /// <summary>
        /// Constructs a <see cref="Jsonize"/> object with the supplied <see cref="HtmlDocument"/> .
        /// </summary>
        /// <param name="htmlDoc">HtmlDocument loaded with the html string</param>
        public Jsonize(HtmlDocument htmlDoc) : this()
        {
            _htmlDoc = htmlDoc;
        }

        /// <summary>
        /// Constructs a <see cref="Jsonize"/> object with the supplied HTML <see cref="string"/>.
        /// </summary>
        /// <param name="html">Html string</param>
        public Jsonize(string html) : this()
        {
            _htmlDoc = new HtmlDocument();
            _htmlDoc.LoadHtml(html);
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
        public static Jsonize FromHtmlDocument(HtmlDocument htmlDoc)
        {
            return new Jsonize(htmlDoc);
        }

        /// <summary>
        /// Returns a <see cref="JObject"/> of the HTML document.
        /// </summary>
        /// <returns>The JSON representation of an HTML document as a <see cref="JObject"/>.</returns>
        public JObject ParseHtmlAsJson()
        {
            JsonizeNode parentJsonizeNode = new JsonizeNode();
            HtmlNode parentHtmlNode = _htmlDoc.DocumentNode;

            parentJsonizeNode.Node = parentHtmlNode.NodeType.ToString();
            GetChildren(parentJsonizeNode, parentHtmlNode);

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
            HtmlNode parentHtmlNode = _htmlDoc.DocumentNode;

            parentJsonizeNode.Node = parentHtmlNode.NodeType.ToString();
            GetChildren(parentJsonizeNode, parentHtmlNode);

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

        private void GetChildren(JsonizeNode parentJsonizeNode, HtmlNode parentHtmlNode)
        {
            foreach (HtmlNode htmlNode in parentHtmlNode.ChildNodes)
            {
                bool addToParent = false;
                JsonizeNode childJsonizeNode = new JsonizeNode();

                if (parentJsonizeNode.Children == null)
                {
                    parentJsonizeNode.Children = new List<JsonizeNode>();
                }

                if (!htmlNode.Name.StartsWith("#"))
                {
                    childJsonizeNode.Tag = htmlNode.Name;
                    addToParent = true;
                }

                string innerText = HtmlDecode(_textTrimHandling == TextTrimHandling.Trim ? htmlNode.InnerText.Trim() : htmlNode.InnerText);
                if (_emptyTextNodeHandling == EmptyTextNodeHandling.Include || !string.IsNullOrWhiteSpace(innerText))
                {
                    if (!htmlNode.HasChildNodes)
                    {
                        childJsonizeNode.Text = innerText.Equals("") ? null : innerText;
                    }

                    addToParent = true;
                }

                childJsonizeNode.Node = htmlNode.NodeType.ToString();

                if (htmlNode.HasAttributes)
                {
                    if (childJsonizeNode.Attributes == null)
                    {
                        childJsonizeNode.Attributes = new Dictionary<string,object>();
                    }

                    AddAttributes(htmlNode, childJsonizeNode);
                    addToParent = true;
                }

                if (htmlNode.HasChildNodes)
                {
                    GetChildren(childJsonizeNode, htmlNode);
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

        private void AddAttributes(HtmlNode htmlNode, JsonizeNode childJsonizeNode)
        {
            Dictionary<string, object> attributeDict = childJsonizeNode.Attributes;

            List<HtmlAttribute> attributes = htmlNode.Attributes.ToList();
            foreach (HtmlAttribute attribute in attributes)
            {
                if (attribute.Name.Equals("class") && _classAttributeHandling == ClassAttributeHandling.Array)
                {
                    string[] classes = attribute.Value.Split(' ');
                    List<string> classList = classes.ToList();
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
