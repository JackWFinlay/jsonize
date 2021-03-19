using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Serializer;

namespace JackWFinlay.Jsonize.Test.Fixtures
{
    public class JsonizeSerializerTestFixture
    {
        public readonly IJsonizeSerializer JsonizeSerializer;

        public JsonizeSerializerTestFixture()
        {
            JsonizeSerializer = new JsonizeSerializer();
        }
    }
}