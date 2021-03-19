using System;
using System.Text.Json;
using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Serializer
{
    public class JsonizeSerializer: IJsonizeSerializer
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        
        /// <summary>
        /// Initialize serializer with default settings.
        /// </summary>
        public JsonizeSerializer()
        {
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new JsonizeNamingPolicy()
            };
        }

        /// <summary>
        /// Initialize serializer with given <see cref="JsonSerializerOptions"/>
        /// </summary>
        /// <param name="jsonSerializerOptions"><see cref="JsonSerializerOptions"/> to use for serialization.</param>
        public JsonizeSerializer(JsonSerializerOptions jsonSerializerOptions)
        {
            jsonSerializerOptions.PropertyNamingPolicy = new JsonizeNamingPolicy();
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public Task<string> Serialize(JsonizeNode jsonizeNode)
        {
            var result = JsonSerializer.Serialize(jsonizeNode, _jsonSerializerOptions);
            
            return Task.FromResult(result);
        }
    }
}