using Jsonize.Abstractions.Interfaces;
using Jsonize.Parser;

namespace Jsonize.Test.Fixtures
{
    public class JsonizeParserTestFixture
    {
        public IJsonizeParser JsonizeParser { get; }

        public JsonizeParserTestFixture()
        {
            JsonizeParser = new JsonizeParser();
        }
    }
}