using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Serializer.NewtonsoftJson;

namespace JackWFinlay.Jsonize.Test.Fixtures
{
    public class NewtonsoftJsonJsonizeSerializerTestFixture
    {
        public readonly IJsonizeSerializer JsonizeSerializer;

        public NewtonsoftJsonJsonizeSerializerTestFixture()
        {
            JsonizeSerializer = new NewtonsoftJsonJsonizeSerializer();
        }
    }
}