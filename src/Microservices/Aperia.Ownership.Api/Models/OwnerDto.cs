using Aperia.Ownership.Api.Entities;

namespace Aperia.Ownership.Api.Models
{
    /// <summary>
    /// The Owner Dto
    /// </summary>
    public class OwnerDto
    {
        /// <summary>
        /// Gets or sets the type of the owner.
        /// </summary>
        public OwnerType? OwnerType { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        public Guid? OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }
    }
}