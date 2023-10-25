namespace Aperia.ContactManagement.Api.Messaging.EventData
{
    /// <summary>
    /// The Inquiry
    /// </summary>
    public class Inquiry
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
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the secondary status.
        /// </summary>
        public string? SecondaryStatus { get; set; }

        /// <summary>
        /// Gets or sets the status date.
        /// </summary>
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the owner.
        /// </summary>
        public string? OwnerType { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        public Guid? OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the assignee.
        /// </summary>
        public Guid? Assignee { get; set; }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        public string? ContactName { get; set; }

        /// <summary>
        /// Gets or sets the contact phone.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}