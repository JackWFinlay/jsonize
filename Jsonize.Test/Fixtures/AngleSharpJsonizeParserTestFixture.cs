using Jsonize.Abstractions.Interfaces;
using Jsonize.Parser;

namespace Jsonize.Test.Fixtures
{
    public class AngleSharpJsonizeParserTestFixture
    {
        public IJsonizeParser JsonizeParser { get; }

        public AngleSharpJsonizeParserTestFixture()
        {
            JsonizeParser = new JsonizeParser();
        }
    }
}