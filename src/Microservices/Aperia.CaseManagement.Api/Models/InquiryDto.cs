using Aperia.CaseManagement.Api.Entities;

namespace Aperia.CaseManagement.Api.Models
{
    /// <summary>
    /// The Inquiry Dto
    /// </summary>
    public class InquiryDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public string? EntityId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public InquiryStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the secondary status.
        /// </summary>
        public string? SecondaryStatus { get; set; }

        /// <summary>
        /// Gets or sets the status date.
        /// </summary>
        public DateTime StatusDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the owner.
        /// </summary>
        public OwnerType? OwnerType { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        public Guid? OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the assignee.
        /// </summary>
        public Guid? Assignee { get; set; }

        /// <summary>
        /// Gets or sets the contact identifier.
        /// </summary>
        public Guid? ContactId { get; set; }

        /// <summary>
        /// Gets the acu trigger identifier.
        /// </summary>
        public Guid? AcuTriggerId { get; internal set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

    }
}