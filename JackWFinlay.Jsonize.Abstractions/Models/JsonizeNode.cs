using System.Collections.Generic;
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

        [JsonizePropertyName("child")]
        public IEnumerable<JsonizeNode> Children { get; set; }
    }
}