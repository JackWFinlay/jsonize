using System.Threading.Tasks;
using Jsonize.Abstractions.Interfaces;
using Jsonize.Abstractions.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jsonize.Serializer.Json.Net
{
    public class JsonizeSerializer : IJsonizeSerializer
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        /// <summary>
        /// Initialize serializer with default settings.
        /// </summary>
        public JsonizeSerializer()
        {
            _jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        /// <summary>
        /// Initialize serializer with given <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="jsonSerializerSettings"><see cref="JsonSerializerSettings"/> to use for serialization.</param>
        public JsonizeSerializer(JsonSerializerSettings jsonSerializerSettings)
        {
            jsonSerializerSettings.ContractResolver ??= new CamelCasePropertyNamesContractResolver();
            _jsonSerializerSettings = jsonSerializerSettings;
        }
        
        public Task<string> Serialize(JsonizeNode jsonizeNode)
        {
            string result = JsonConvert.SerializeObject(jsonizeNode, _jsonSerializerSettings);

            return Task.FromResult(result);
        }
    }
}