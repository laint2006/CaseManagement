namespace Aperia.SlaOla.Api.Models
{
    /// <summary>
    /// The Change History
    /// </summary>
    public class ChangeHistory
    {
        /// <summary>
        /// Gets or sets the attribute.
        /// </summary>
        public string Attribute { get; set; } = null!;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string? Value { get; set; }
    }
}