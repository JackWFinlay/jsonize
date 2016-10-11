using Newtonsoft.Json;

namespace JackWFinlay.Jsonize
{
    public class JsonizeMeta
    {
        [JsonProperty(PropertyName = "domain")]
        public string Domain { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "document")]
        public JsonizeNode DocumentJsonizeNode { get; set; }

        public JsonizeMeta(JsonizeNode jsonizeNode, string url)
        {
            Url = url;
            Domain = url.Split('/')[2];
            DocumentJsonizeNode = jsonizeNode;
        }
    }
}
