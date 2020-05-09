using System;
using System.Runtime.Serialization;

namespace JackWFinlay.Jsonize.Abstractions.Exceptions
{
    public class JsonizeNullParserException : NullReferenceException
    {
        public JsonizeNullParserException()
        {
        }

        protected JsonizeNullParserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public JsonizeNullParserException(string message) : base(message)
        {
        }

        public JsonizeNullParserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}