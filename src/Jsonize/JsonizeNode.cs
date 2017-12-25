using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace JackWFinlay.Jsonize
{
    public class JsonizeNode
    {
        [JsonProperty(PropertyName = "node")]
        public string Node { get; set; }

        [JsonProperty(PropertyName = "tag")]
        public string Tag { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "attr")]
        public Dictionary<string,object> Attributes { get; set; }

        [JsonProperty(PropertyName = "child")]
        public List<JsonizeNode> Children { get; set; }
    }
}
