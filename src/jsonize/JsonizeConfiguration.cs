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

        internal EmptyTextNodeHandling? _emptyTextNodeHandling;
        internal NullValueHandling? _nullValueHandling;

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





    }
}
