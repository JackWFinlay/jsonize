using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Parser.AngleSharp;
using JackWFinlay.Jsonize.Serializer.NewtonsoftJson;

namespace JackWFinlay.Jsonize.Test.Fixtures
{
    public class EndToEndAngleSharpNewtonsoftTestFixture
    {
        public Jsonizer Jsonizer { get; }

        public EndToEndAngleSharpNewtonsoftTestFixture()
        {
            var jsonizeParser = new AngleSharpJsonizeParser();
            var jsonizeSerializer = new NewtonsoftJsonJsonizeSerializer();
            
            Jsonizer = new Jsonizer(jsonizeParser, jsonizeSerializer);
        }
    }
}