using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JackWFinlay.Jsonize
{
    /// <summary>
    /// Specifies handling of empty text nodes.
    /// </summary>
    public enum EmptyTextNodeHandling
    {
        /// <summary>
        /// Include empty text nodes in the conversion output.
        /// </summary>
        Include = 0,

        /// <summary>
        /// Exclude empty text nodes from conversion output.
        /// </summary>
        Ignore = 1

    }
}
