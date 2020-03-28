using System;
using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Abstractions.Models;
using JackWFinlay.Jsonize.Configuration;

namespace JackWFinlay.Jsonize
{
    public class Jsonizer
    {
        private JsonizeConfiguration _jsonizeConfiguration;
        public JsonizeConfiguration JsonizeConfiguration 
        { 
            get => _jsonizeConfiguration;
            set => _jsonizeConfiguration = value;
        }

        public Jsonizer(JsonizeConfiguration jsonizeJsonizeConfiguration)
        {
            _jsonizeConfiguration = jsonizeJsonizeConfiguration;
        }

        public Jsonizer(IJsonizeSerializer serializer)
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration()
            {
                Serializer = serializer
            };

            _jsonizeConfiguration = jsonizeConfiguration;
        }
        
        public Jsonizer(IJsonizeParser jsonizeParser)
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration()
            {
                Parser = jsonizeParser
            };

            _jsonizeConfiguration = jsonizeConfiguration;
        }
        
        public Jsonizer(IJsonizeParser jsonizeParser, IJsonizeSerializer jsonizeSerializer)
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration()
            {
                Parser = jsonizeParser,
                Serializer = jsonizeSerializer
            };

            _jsonizeConfiguration = jsonizeConfiguration;
        }

        public async Task<JsonizeNode> ParseToJsonizeNodeAsync(string htmlString)
        {
            JsonizeNode jsonizeNode = await _jsonizeConfiguration.Parser.ParseAsync(htmlString);

            return jsonizeNode;
        }
        
        public async Task<string> ParseToStringAsync(string htmlString)
        {
            JsonizeNode jsonizeNode = await _jsonizeConfiguration.Parser.ParseAsync(htmlString);
            string jsonString = await _jsonizeConfiguration.Serializer.Serialize(jsonizeNode);
            
            return jsonString;
        }
    }
}