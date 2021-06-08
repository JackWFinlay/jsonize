using Jsonize.Abstractions.Interfaces;
using Jsonize.Serializer.Json.Net;

namespace Jsonize.Test.Fixtures
{
    public class NewtonsoftJsonJsonizeSerializerTestFixture
    {
        public readonly IJsonizeSerializer JsonizeSerializer;

        public NewtonsoftJsonJsonizeSerializerTestFixture()
        {
            JsonizeSerializer = new JsonizeSerializer();
        }
    }
}