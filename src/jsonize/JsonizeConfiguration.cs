// ReSharper disable InconsistentNaming
namespace JackWFinlay.Jsonize
{
    public class JsonizeConfiguration
    {
        internal const EmptyTextNodeHandling DefaultEmptyTextNodeHandling = EmptyTextNodeHandling.Ignore;
        internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Ignore;
        internal const TextTrimHandling DefaultTextTrimHandling = TextTrimHandling.Trim;
        internal const ClassAttributeHandling DefaultClassAttributeHandling = ClassAttributeHandling.Array;

        internal EmptyTextNodeHandling? _emptyTextNodeHandling;
        internal NullValueHandling? _nullValueHandling;
        internal TextTrimHandling? _textTrimHandling;
        internal ClassAttributeHandling? _classAttributeHandling;

        /// <summary>
        /// Gets or sets how empty Text nodes are handled during conversion.
        /// </summary>
        /// <value>Empty Text Node handling.</value>
        public EmptyTextNodeHandling EmptyTextNodeHandling
        {
            get { return _emptyTextNodeHandling ?? DefaultEmptyTextNodeHandling; }
            set { _emptyTextNodeHandling = value; }
        }

        /// <summary>
        /// Gets or sets how null values are handled during conversion.
        /// </summary>
        /// <value>Null value handling.</value>
        public NullValueHandling NullValueHandling
        {
            get { return _nullValueHandling ?? DefaultNullValueHandling; }
            set { _nullValueHandling = value; }
        }

        /// <summary>
        /// Gets or sets how white-space in inner-Text of nodes is handled during conversion.
        /// </summary>
        /// <value>White-space trim handling.</value>
        public TextTrimHandling TextTrimHandling
        {
            get { return _textTrimHandling ?? DefaultTextTrimHandling; }
            set { _textTrimHandling = value; }
        }

        /// <summary>
        /// Gets or sets how the class attributes of nodes is handled during conversion.
        /// </summary>
        /// <value>Class attribute handling.</value>
        public ClassAttributeHandling ClassAttributeHandling
        {
            get { return _classAttributeHandling ?? DefaultClassAttributeHandling; }
            set { _classAttributeHandling = value; }
        }


    }
}
