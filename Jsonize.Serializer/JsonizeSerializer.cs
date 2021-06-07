using System;
using System.Text.Json;
using System.Threading.Tasks;
using Jsonize.Abstractions.Interfaces;
using Jsonize.Abstractions.Models;

namespace Jsonize.Serializer
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
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Initialize serializer with given <see cref="JsonSerializerOptions"/>
        /// </summary>
        /// <param name="jsonSerializerOptions"><see cref="JsonSerializerOptions"/> to use for serialization.</param>
        public JsonizeSerializer(JsonSerializerOptions jsonSerializerOptions)
        {
            // Only override PropertyNamingPolicy if not set.
            jsonSerializerOptions.PropertyNamingPolicy ??= JsonNamingPolicy.CamelCase;
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public Task<string> Serialize(JsonizeNode jsonizeNode)
        {
            var result = JsonSerializer.Serialize(jsonizeNode, _jsonSerializerOptions);
            
            return Task.FromResult(result);
        }
    }
}