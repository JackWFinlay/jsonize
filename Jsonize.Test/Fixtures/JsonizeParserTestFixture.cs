using Jsonize.Abstractions.Configuration;
using Jsonize.Abstractions.Interfaces;
using Jsonize.Parser;

namespace Jsonize.Test.Fixtures
{
    public class JsonizeParserTestFixture
    {
        public IJsonizeParser JsonizeParser { get; }
        public IJsonizeParser JsonizeParserClassic { get; }

        public JsonizeParserTestFixture()
        {
            JsonizeParser = new JsonizeParser();
            
            var configClassic = new JsonizeParserConfiguration()
            {
                ParagraphHandling = ParagraphHandling.Classic
            };
            
            JsonizeParserClassic = new JsonizeParser(configClassic);
        }
    }
}