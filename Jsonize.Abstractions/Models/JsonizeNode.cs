using System.Collections.Generic;
using System.Linq;

namespace Jsonize.Abstractions.Models
{
    public class JsonizeNode
    {
        /// <summary>
        /// The type of the current node. e.g. Document, Comment, Element, etc.
        /// </summary>
        public string NodeType { get; set; }
        
        /// <summary>
        /// The tag for the current node. e.g. body, head, div, p, etc.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The node's text content, if any present.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The attributes for this node. e.g. class, id, etc.
        /// </summary>
        public IDictionary<string,object> Attr { get; set; }

        /// <summary>
        /// The child nodes of this node, if any present.
        /// </summary>
        public IEnumerable<JsonizeNode> Children { get; set; } = Enumerable.Empty<JsonizeNode>();
    }
}