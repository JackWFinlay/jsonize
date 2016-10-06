using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JackWFinlay.Jsonize
{
    /// <summary>
    /// Specifies handling of keys that have null values.
    /// </summary>
    public enum NullValueHandling
    {
        /// <summary>
        /// Include keys that have null values in the conversion output.
        /// </summary>
        Include = 0,

        /// <summary>
        /// Exclude keys that have null values from conversion output.
        /// </summary>
        Ignore = 1
    }
}
