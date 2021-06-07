using Jsonize.Abstractions.Interfaces;
using Jsonize.Parser;
using Jsonize.Serializer.Json.Net;

namespace Jsonize.Test.Fixtures
{
    public class EndToEndAngleSharpNewtonsoftTestFixture
    {
        public Jsonizer Jsonizer { get; }

        public EndToEndAngleSharpNewtonsoftTestFixture()
        {
            var jsonizeParser = new JsonizeParser();
            var jsonizeSerializer = new JsonizeSerializer();
            
            Jsonizer = new Jsonizer(jsonizeParser, jsonizeSerializer);
        }
    }
}