using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Models;
using JackWFinlay.Jsonize.Test.Fixtures;
using Xunit;

namespace JackWFinlay.Jsonize.Test
{
    public class EndToEndAngleSharpNewtonsoftTests : IClassFixture<EndToEndAngleSharpNewtonsoftTestFixture>
    {
        private readonly EndToEndAngleSharpNewtonsoftTestFixture _fixture;

        public EndToEndAngleSharpNewtonsoftTests(EndToEndAngleSharpNewtonsoftTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DocoHtmlString_DefaultConfiguration_ProducesValidOutput()
        {
            string jsonize = await _fixture.Jsonizer.ParseToStringAsync(StringResources.DocoHtmlExample);
        }
    }
}