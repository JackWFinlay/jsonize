using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Jsonize.Abstractions.Configuration;
using Jsonize.Abstractions.Interfaces;
using Jsonize.Abstractions.Models;

namespace Jsonize
{
    public class Jsonizer : IJsonizer
    {
        private readonly JsonizeConfiguration _jsonizeConfiguration;

        /// <summary>
        /// Create an instance of <see cref="Jsonizer"/> with the given <see cref="JsonizeConfiguration"/>
        /// </summary>
        /// <param name="jsonizeConfiguration">The <see cref="JsonizeConfiguration"/> to use.</param>
        public Jsonizer(JsonizeConfiguration jsonizeConfiguration)
        {
            _jsonizeConfiguration = jsonizeConfiguration;
        }

        /// <summary>
        /// Create an instance of <see cref="Jsonizer"/> with the given <see cref="IJsonizeSerializer"/>
        /// The default configuration will be used.
        /// </summary>
        /// <param name="serializer">The <see cref="IJsonizeSerializer"/> to use for serialization of results.</param>
        public Jsonizer(IJsonizeSerializer serializer)
        {
            var jsonizeConfiguration = new JsonizeConfiguration()
            {
                Serializer = serializer
            };

            _jsonizeConfiguration = jsonizeConfiguration;
        }
        
        /// <summary>
        /// Create an instance of <see cref="Jsonizer"/> with the given <see cref="IJsonizeParser"/>
        /// The default configuration will be used.
        /// </summary>
        /// <param name="jsonizeParser">The <see cref="IJsonizeParser"/> to use for parsing.</param>
        public Jsonizer(IJsonizeParser jsonizeParser)
        {
            var jsonizeConfiguration = new JsonizeConfiguration()
            {
                Parser = jsonizeParser
            };

            _jsonizeConfiguration = jsonizeConfiguration;
        }
        
        /// <summary>
        /// Create an instance of <see cref="Jsonizer"/> with the given <see cref="IJsonizeParser"/> and <see cref="IJsonizeSerializer"/>
        /// The default configuration will be used.
        /// </summary>
        /// <param name="jsonizeParser">The <see cref="IJsonizeParser"/> to use for parsing.</param>
        /// <param name="jsonizeSerializer">The <see cref="IJsonizeSerializer"/> to use for serialization of results.</param>
        public Jsonizer(IJsonizeParser jsonizeParser, IJsonizeSerializer jsonizeSerializer)
        {
            var jsonizeConfiguration = new JsonizeConfiguration()
            {
                Parser = jsonizeParser,
                Serializer = jsonizeSerializer
            };

            _jsonizeConfiguration = jsonizeConfiguration;
        }

        /// <summary>
        /// Parse the given HTML <see cref="string"/> into Jsonize's <see cref="JsonizeNode"/> format.
        /// </summary>
        /// <param name="htmlString">The HTML to parse.</param>
        /// <param name="cancellationToken">Cancellation token to request cancellation.</param>
        /// <returns>The parent <see cref="JsonizeNode"/> representing the HTML.</returns>
        public async Task<JsonizeNode> ParseToJsonizeNodeAsync(string htmlString, CancellationToken cancellationToken = default)
        {
            var jsonizeNode = await _jsonizeConfiguration.Parser.ParseAsync(htmlString, cancellationToken);

            return jsonizeNode;
        }

        /// <summary>
        /// Parse the given HTML <see cref="string"/> into Jsonize's <see cref="JsonizeNode"/> format.
        /// </summary>
        /// <param name="htmlStream">The HTML to parse.</param>
        /// <param name="cancellationToken">Cancellation token to request cancellation.</param>
        /// <returns>The parent <see cref="JsonizeNode"/> representing the HTML.</returns>
        public async Task<JsonizeNode> ParseToJsonizeNodeAsync(Stream htmlStream, CancellationToken cancellationToken = default)
        {
            var jsonizeNode = await _jsonizeConfiguration.Parser.ParseAsync(htmlStream, cancellationToken);

            return jsonizeNode;
        }

        /// <summary>
        /// Parse the given HTML <see cref="string"/> into a JSON <see cref="string"/>.
        /// </summary>
        /// <param name="htmlString">The HTML to parse.</param>
        /// <param name="cancellationToken">Cancellation token to request cancellation.</param>
        /// <returns>The JSON <see cref="string"/> representation of the HTML.</returns>
        public async Task<string> ParseToStringAsync(string htmlString, CancellationToken cancellationToken = default)
        {
            var jsonizeNode = await ParseToJsonizeNodeAsync(htmlString, cancellationToken);
            var jsonString = await _jsonizeConfiguration.Serializer.Serialize(jsonizeNode);
            
            return jsonString;
        }

        /// <summary>
        /// Parse the given HTML <see cref="string"/> into a JSON <see cref="string"/>.
        /// </summary>
        /// <param name="htmlStream">The HTML to parse.</param>
        /// <param name="cancellationToken">Cancellation token to request cancellation.</param>
        /// <returns>The JSON <see cref="string"/> representation of the HTML.</returns>
        public async Task<string> ParseToStringAsync(Stream htmlStream, CancellationToken cancellationToken = default)
        {
            var jsonizeNode = await ParseToJsonizeNodeAsync(htmlStream, cancellationToken);
            var jsonString = await _jsonizeConfiguration.Serializer.Serialize(jsonizeNode);
            
            return jsonString;
        }
    }
}