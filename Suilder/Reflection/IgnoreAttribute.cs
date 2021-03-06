namespace Suilder.Reflection
{
    /// <summary>
    /// Ignore a property.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public sealed class IgnoreAttribute : System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreAttribute"/> class.
        /// </summary>
        public IgnoreAttribute()
        {
        }
    }
}