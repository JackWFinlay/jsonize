namespace JackWFinlay.Jsonize
{
    public class JsonizeConfiguration
    {
        private const EmptyTextNodeHandling DefaultEmptyTextNodeHandling = EmptyTextNodeHandling.Ignore;
        private const NullValueHandling DefaultNullValueHandling = NullValueHandling.Ignore;
        private const TextTrimHandling DefaultTextTrimHandling = TextTrimHandling.Trim;
        private const ClassAttributeHandling DefaultClassAttributeHandling = ClassAttributeHandling.Array;

        private EmptyTextNodeHandling? _emptyTextNodeHandling;
        private NullValueHandling? _nullValueHandling;
        private TextTrimHandling? _textTrimHandling;
        private ClassAttributeHandling? _classAttributeHandling;

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


    }
}
