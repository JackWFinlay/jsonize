using Jsonize.Abstractions.Interfaces;
using Jsonize.Serializer;

namespace Jsonize.Test.Fixtures
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