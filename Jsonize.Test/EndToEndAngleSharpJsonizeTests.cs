using System.Threading.Tasks;
using Jsonize.Test.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace Jsonize.Test
{
    public class EndToEndAngleSharpJsonizeTests : IClassFixture<EndToEndAngleSharpJsonizeTestFixture>
    {
        private readonly EndToEndAngleSharpJsonizeTestFixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public EndToEndAngleSharpJsonizeTests(EndToEndAngleSharpJsonizeTestFixture fixture,
            ITestOutputHelper testOutputHelper)
        {
            _fixture = fixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task DocoHtmlString_DefaultConfiguration_ProducesValidOutput()
        {
            var jsonize = await _fixture.Jsonizer.ParseToStringAsync(StringResources.DocoHtmlExample);

            Assert.Equal(StringResources.DocoHtmlExampleResult, jsonize);
            
            _testOutputHelper.WriteLine(jsonize);
        }

        [Fact]
        public async Task TestOutput()
        {
            const string html = StringResources.HtmlBodyP;

            var output = await _fixture.Jsonizer.ParseToStringAsync(html);

            Assert.Equal(StringResources.HtmlBodyPResult, output);
            
            _testOutputHelper.WriteLine(output);
        }
    }
}