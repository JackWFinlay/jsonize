using System.Threading.Tasks;
using FluentAssertions;
using JackWFinlay.Jsonize.Test.Fixtures;
using Xunit;

namespace JackWFinlay.Jsonize.Test
{
    public class JsonizeSerializerTests : IClassFixture<JsonizeSerializerTestFixture>
    {
        private readonly JsonizeSerializerTestFixture _testFixture;

        public JsonizeSerializerTests(JsonizeSerializerTestFixture testFixture)
        {
            _testFixture = testFixture;
        }
        
        [Fact]
        public async Task Serialize_DefaultSettings_SerializesCorrectly()
        {
            string actual = await _testFixture.JsonizeSerializer.Serialize(JsonizeNodeTestResources.HtmlBodyP);

            actual
                .Should()
                .Be(StringResources.HtmlBodyPResult);
        }
    }
}