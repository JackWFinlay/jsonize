using System;

namespace JackWFinlay.Jsonize.Attributes
{
    /// <summary>
    /// Enables a custom name for serialization.
    /// </summary>
    public class JsonizePropertyNameAttribute : Attribute
    {
        /// <summary>
        /// The custom name to use during serialization.
        /// </summary>
        internal string Name { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="JsonizePropertyNameAttribute"/> with the specified property name.
        /// </summary>
        /// <param name="name">The name of the property</param>
        public JsonizePropertyNameAttribute(string name)
        {
            Name = name;
        }
    }
}