using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JackWFinlay.Jsonize
{
    /// <summary>
    /// Specifies handling of white-space trimming.
    /// </summary>
    public enum TextTrimHandling
    {
        /// <summary>
        /// Include white-space in the conversion output.
        /// </summary>
        Include = 0,

        /// <summary>
        /// Trim white-space in conversion output.
        /// </summary>
        Trim = 1
    }
}
