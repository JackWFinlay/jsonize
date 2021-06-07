using System;
using System.Runtime.Serialization;

namespace Jsonize.Abstractions.Exceptions
{
    public class JsonizeNullSerializerException : NullReferenceException
    {
        public JsonizeNullSerializerException()
        {
        }

        protected JsonizeNullSerializerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public JsonizeNullSerializerException(string message) : base(message)
        {
        }

        public JsonizeNullSerializerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}