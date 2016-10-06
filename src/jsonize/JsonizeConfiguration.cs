using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JackWFinlay.Jsonize
{
    public class JsonizeConfiguration
    {
        internal const EmptyTextNodeHandling DefaultEmptyTextNodeHandling = EmptyTextNodeHandling.Ignore;
        internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Ignore;
        internal const TextTrimHandling DefaultTextTrimHandling = TextTrimHandling.Trim;

        internal EmptyTextNodeHandling? _emptyTextNodeHandling;
        internal NullValueHandling? _nullValueHandling;
        internal TextTrimHandling? _textTrimHandling;

        /// <summary>
        /// Gets or sets how empty text nodes are handled during conversion.
        /// </summary>
        /// <value>Empty text node handling.</value>
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
        /// Gets or sets how white-space in inner-text of nodes is handled during conversion.
        /// </summary>
        /// <value>White-space trim handling.</value>
        public TextTrimHandling TextTrimHandling
        {
            get { return _textTrimHandling ?? DefaultTextTrimHandling; }
            set { _textTrimHandling = value; }
        }




    }
}
