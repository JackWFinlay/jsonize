using System.Text.Json;
using Jsonize.Parser;
using Jsonize.Serializer;

namespace Jsonize.Test.Fixtures
{
    public class EndToEndAngleSharpJsonizeTestFixture
    {
        public Jsonizer Jsonizer { get; }

        public EndToEndAngleSharpJsonizeTestFixture()
        {
            var jsonizeParser = new JsonizeParser();
            var jsonizeSerializer = new JsonizeSerializer(new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            Jsonizer = new Jsonizer(jsonizeParser, jsonizeSerializer);
        }
    }
}