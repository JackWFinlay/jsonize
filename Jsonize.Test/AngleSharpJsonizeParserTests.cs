using System.Threading.Tasks;
using FluentAssertions;
using Jsonize.Abstractions.Models;
using Jsonize.Test.Fixtures;
using Xunit;

namespace Jsonize.Test
{
    public class AngleSharpJsonizeParserTests : IClassFixture<AngleSharpJsonizeParserTestFixture>
    {
        private AngleSharpJsonizeParserTestFixture _testFixture;
        
        public AngleSharpJsonizeParserTests(AngleSharpJsonizeParserTestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        [Fact]
        public async Task HtmlBodyPStringResource_DefaultConfiguration_ReturnsJsonizeNode()
        {
            JsonizeNode actual = await _testFixture.JsonizeParser.ParseAsync(StringResources.HtmlBodyP);

            actual
                .Should()
                .BeEquivalentTo(JsonizeNodeTestResources.HtmlBodyP);
        }
    }
}