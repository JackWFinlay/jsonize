namespace JackWFinlay.Jsonize.Configuration
{
    /// <summary>
    /// Specifies handling of empty Text nodes.
    /// </summary>
    public enum EmptyTextNodeHandling
    {
        /// <summary>
        /// Include empty Text nodes in the conversion output.
        /// </summary>
        Include = 0,

        /// <summary>
        /// Exclude empty Text nodes from conversion output.
        /// </summary>
        Ignore = 1
    }
}