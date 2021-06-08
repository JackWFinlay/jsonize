using Jsonize.Abstractions.Exceptions;
using Jsonize.Abstractions.Interfaces;

namespace Jsonize.Abstractions.Configuration
{
    public class JsonizeConfiguration
    {
        private IJsonizeSerializer _jsonizeSerializer;
        private IJsonizeParser _jsonizeParser;

        /// <summary>
        /// Gets or sets the serializer for serializing the result to JSON.
        /// </summary>
        public IJsonizeSerializer Serializer
        {
            get => _jsonizeSerializer ?? throw new JsonizeNullSerializerException();
            set => _jsonizeSerializer = value;
        }

        /// <summary>
        /// Gets or sets the parser for parsing the HTML document.
        /// </summary>
        public IJsonizeParser Parser
        {
            get => _jsonizeParser ?? throw new JsonizeNullParserException();
            set => _jsonizeParser = value;
        }
    }
}