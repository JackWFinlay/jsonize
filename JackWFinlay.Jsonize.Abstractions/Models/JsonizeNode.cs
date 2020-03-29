using System.Collections.Generic;
using System.Linq;
using JackWFinlay.Jsonize.Attributes;

namespace JackWFinlay.Jsonize.Abstractions.Models
{
    public class JsonizeNode
    {
        [JsonizePropertyName("node")]
        public string Node { get; set; }
        
        [JsonizePropertyName("tag")]
        public string Tag { get; set; }

        [JsonizePropertyName("text")]
        public string Text { get; set; }

        [JsonizePropertyName("attr")]
        public IDictionary<string,object> Attributes { get; set; }

        [JsonizePropertyName("children")]
        public IEnumerable<JsonizeNode> Children { get; set; } = Enumerable.Empty<JsonizeNode>();
    }
}