using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Parser.AngleSharp;

namespace JackWFinlay.Jsonize.Test.Fixtures
{
    public class AngleSharpJsonizeParserTestFixture
    {
        public IJsonizeParser JsonizeParser { get; }

        public AngleSharpJsonizeParserTestFixture()
        {
            JsonizeParser = new AngleSharpJsonizeParser();
        }
    }
}