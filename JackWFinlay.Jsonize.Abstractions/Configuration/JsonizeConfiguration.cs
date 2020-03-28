using System;
using JackWFinlay.Jsonize.Abstractions.Exceptions;
using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Configuration;

namespace JackWFinlay.Jsonize.Abstractions.Configuration
{
    public class JsonizeConfiguration
    {
        internal const EmptyTextNodeHandling DefaultEmptyTextNodeHandling = EmptyTextNodeHandling.Ignore;
        internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Ignore;
        internal const TextTrimHandling DefaultTextTrimHandling = TextTrimHandling.Trim;
        internal const ClassAttributeHandling DefaultClassAttributeHandling = ClassAttributeHandling.Array;

        private EmptyTextNodeHandling? _emptyTextNodeHandling;
        private NullValueHandling? _nullValueHandling;
        private TextTrimHandling? _textTrimHandling;
        private ClassAttributeHandling? _classAttributeHandling;
        private IJsonizeSerializer _jsonizeSerializer;
        private IJsonizeParser _jsonizeParser;

        /// <summary>
        /// Gets or sets how empty Text nodes are handled during conversion.
        /// </summary>
        /// <value>Empty Text Node handling.</value>
        public EmptyTextNodeHandling EmptyTextNodeHandling
        {
            get => _emptyTextNodeHandling ?? DefaultEmptyTextNodeHandling;
            set => _emptyTextNodeHandling = value;
        }

        /// <summary>
        /// Gets or sets how null values are handled during conversion.
        /// </summary>
        /// <value>Null value handling.</value>
        public NullValueHandling NullValueHandling
        {
            get => _nullValueHandling ?? DefaultNullValueHandling;
            set => _nullValueHandling = value;
        }

        /// <summary>
        /// Gets or sets how white-space in inner-Text of nodes is handled during conversion.
        /// </summary>
        /// <value>White-space trim handling.</value>
        public TextTrimHandling TextTrimHandling
        {
            get => _textTrimHandling ?? DefaultTextTrimHandling;
            set => _textTrimHandling = value;
        }

        /// <summary>
        /// Gets or sets how the class attributes of nodes is handled during conversion.
        /// </summary>
        /// <value>Class attribute handling.</value>
        public ClassAttributeHandling ClassAttributeHandling
        {
            get => _classAttributeHandling ?? DefaultClassAttributeHandling;
            set => _classAttributeHandling = value;
        }

        /// <summary>
        /// Gets or sets the serializer for serializing the result to JSON.
        /// </summary>
        public IJsonizeSerializer Serializer
        {
            get => _jsonizeSerializer ?? throw new JsonizeNullSerializerException();
            set => _jsonizeSerializer = value;
        }

        /// <summary>
        /// Gets or sets the parser for parsing the HTML document.
        /// </summary>
        public IJsonizeParser Parser
        {
            get => _jsonizeParser ?? throw new JsonizeNullParserException();
            set => _jsonizeParser = value;
        }
    }
}