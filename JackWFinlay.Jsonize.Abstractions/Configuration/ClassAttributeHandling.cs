namespace JackWFinlay.Jsonize.Abstractions.Configuration
{
    /// <summary>
    /// Specifies handling of the class attributes on a Node during conversion.
    /// </summary>
    public enum ClassAttributeHandling
    {
        /// <summary>
        /// Output classes as an array.
        /// </summary>
        Array = 0,

        /// <summary>
        /// Output classes as a space delimited string.
        /// </summary>
        String = 1
    }
}