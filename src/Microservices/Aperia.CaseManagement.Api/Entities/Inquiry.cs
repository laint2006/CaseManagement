using Aperia.Core.Domain.Primitives;

namespace Aperia.CaseManagement.Api.Entities;

/// <summary>
/// The Inquiry
/// </summary>
/// <seealso cref="Aperia.Core.Domain.Primitives.Entity{Guid}" />
/// <seealso cref="Aperia.Core.Domain.Primitives.IAuditableEntity" />
public class Inquiry : Entity<Guid>, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the source.
    /// </summary>
    public string Source { get; set; }

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
    /// Gets or sets the created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Inquiry" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="status">The status.</param>
    /// <param name="secondaryStatus">The secondary status.</param>
    /// <param name="statusDate">The status date.</param>
    private Inquiry(string source, string? entityId, InquiryStatus status, string? secondaryStatus, DateTime statusDate)
        : base(Guid.NewGuid())
    {
        this.Source = source;
        this.EntityId = entityId;
        this.Status = status;
        this.SecondaryStatus = secondaryStatus;
        this.StatusDate = statusDate;
    }

    /// <summary>
    /// Creates the specified customer name.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="status">The status.</param>
    /// <param name="secondaryStatus">The secondary status.</param>
    /// <param name="statusDate">The status date.</param>
    /// <returns></returns>
    public static Inquiry Create(string source, string? entityId, InquiryStatus status, string? secondaryStatus, DateTime statusDate)
    {
        var inquiry = new Inquiry(source, entityId, status, secondaryStatus, statusDate);

        return inquiry;
    }
}