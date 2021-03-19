using JackWFinlay.Jsonize.Parser.AngleSharp;
using JackWFinlay.Jsonize.Serializer;

namespace JackWFinlay.Jsonize.Test.Fixtures
{
    public class EndToEndAngleSharpJsonizeTestFixture
    {
        public Jsonizer Jsonizer { get; }

        public EndToEndAngleSharpJsonizeTestFixture()
        {
            var jsonizeParser = new AngleSharpJsonizeParser();
            var jsonizeSerializer = new JsonizeSerializer();
            
            Jsonizer = new Jsonizer(jsonizeParser, jsonizeSerializer);
        }
    }
}