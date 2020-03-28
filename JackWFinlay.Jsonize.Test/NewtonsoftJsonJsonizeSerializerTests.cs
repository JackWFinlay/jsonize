using System.Threading.Tasks;
using JackWFinlay.Jsonize.Test.Fixtures;
using Xunit;

namespace JackWFinlay.Jsonize.Test
{
    public class NewtonsoftJsonJsonizeSerializerTests : IClassFixture<NewtonsoftJsonJsonizeSerializerTestFixture>
    {
        private readonly NewtonsoftJsonJsonizeSerializerTestFixture _testFixture;

        public NewtonsoftJsonJsonizeSerializerTests(NewtonsoftJsonJsonizeSerializerTestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        [Fact]
        public async Task Serialize_DefaultSettings_SerializesCorrectly()
        {
            string actual = await _testFixture.JsonizeSerializer.Serialize(JsonizeNodeTestResources.HtmlBodyP);
        }
    }
}