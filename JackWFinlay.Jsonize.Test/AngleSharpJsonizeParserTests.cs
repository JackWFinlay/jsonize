using System;
using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Models;
using JackWFinlay.Jsonize.Test.Fixtures;
using Xunit;

namespace JackWFinlay.Jsonize.Test
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
        }
    }
}