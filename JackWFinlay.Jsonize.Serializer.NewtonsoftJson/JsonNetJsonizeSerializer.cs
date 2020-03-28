using System;
using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Abstractions.Models;
using Newtonsoft.Json;

namespace JackWFinlay.Jsonize.Serializer.NewtonsoftJson
{
    public class NewtonsoftJsonJsonizeSerializer : IJsonizeSerializer
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public NewtonsoftJsonJsonizeSerializer()
        {
            _jsonSerializerSettings = new JsonSerializerSettings();
            _jsonSerializerSettings.
        }

        public NewtonsoftJsonJsonizeSerializer(JsonSerializerSettings jsonSerializerSettings) : this()
        {
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public Task<string> Serialize(JsonizeNode jsonizeNode)
        {
            string result = JsonConvert.SerializeObject(jsonizeNode, _jsonSerializerSettings);

            return Task.FromResult(result);
        }
    }
}