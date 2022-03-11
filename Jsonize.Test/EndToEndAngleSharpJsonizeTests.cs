using System.IO;
using System.Text;
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
        public async Task LargeExampleString_DefaultConfiguration_ProducesValidOutput()
        {
            var jsonize = await _fixture.Jsonizer.ParseToStringAsync(StringResources.LargeExample);

            //Assert.Equal(StringResources.DocoHtmlExampleResult, jsonize);
            
            _testOutputHelper.WriteLine(jsonize);
        }

        [Fact]
        public async Task DocoHtmlStream_DefaultConfiguration_ProducesValidOutput()
        {
            await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(StringResources.DocoHtmlExample));
            var jsonize = await _fixture.Jsonizer.ParseToStringAsync(stream);

            Assert.Equal(StringResources.DocoHtmlExampleResult, jsonize);
            
            _testOutputHelper.WriteLine(jsonize);
        }
        
        [Fact]
        public async Task LargeExampleStream_DefaultConfiguration_ProducesValidOutput()
        {
            await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(StringResources.LargeExample));
            var jsonize = await _fixture.Jsonizer.ParseToStringAsync(stream);

            //Assert.Equal(StringResources.DocoHtmlExampleResult, jsonize);
            
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