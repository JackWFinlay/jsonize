using System.Collections.Generic;
using System.Linq;
using JackWFinlay.Jsonize.Abstractions.Attributes;

namespace JackWFinlay.Jsonize.Abstractions.Models
{
    public class JsonizeNode
    {
        /// <summary>
        /// The type of the current node. e.g. Document, Comment, Element, etc.
        /// </summary>
        [JsonizePropertyName("nodeType")]
        public string NodeType { get; set; }
        
        /// <summary>
        /// The tag for the current node. e.g. body, head, div, p, etc.
        /// </summary>
        [JsonizePropertyName("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// The node's text content, if any present.
        /// </summary>
        [JsonizePropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// The attributes for this node. e.g. class, id, etc.
        /// </summary>
        [JsonizePropertyName("attr")]
        public IDictionary<string,object> Attributes { get; set; }

        /// <summary>
        /// The child nodes of this node, if any present.
        /// </summary>
        [JsonizePropertyName("children")]
        public IEnumerable<JsonizeNode> Children { get; set; } = Enumerable.Empty<JsonizeNode>();
    }
}