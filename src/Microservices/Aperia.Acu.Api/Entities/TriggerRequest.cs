using Aperia.Core.Domain.Primitives;

namespace Aperia.Acu.Api.Entities;

/// <summary>
/// The Trigger Request
/// </summary>
/// <seealso cref="Aperia.Core.Domain.Primitives.Entity{Guid}" />
/// <seealso cref="Aperia.Core.Domain.Primitives.IAuditableEntity" />
public class TriggerRequest : Entity<Guid>, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the trigger point code.
    /// </summary>
    public string TriggerPointCode { get; set; }

    /// <summary>
    /// Gets or sets the type of the event.
    /// </summary>
    public string EventType { get; set; }

    /// <summary>
    /// Gets or sets the request data.
    /// </summary>
    public string? RequestData { get; set; }

    /// <summary>
    /// Gets or sets the response data.
    /// </summary>
    public string? ResponseData { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TriggerRequest" /> class.
    /// </summary>
    /// <param name="triggerPointCode">The trigger point code.</param>
    /// <param name="eventType">Type of the event.</param>
    /// <param name="requestData">The request data.</param>
    /// <param name="responseData">The response data.</param>
    private TriggerRequest(string triggerPointCode, string eventType, string? requestData, string? responseData)
        : base(Guid.NewGuid())
    {
        this.TriggerPointCode = triggerPointCode;
        this.EventType = eventType;
        this.RequestData = requestData;
        this.ResponseData = responseData;
    }

    /// <summary>
    /// Creates the specified customer name.
    /// </summary>
    /// <param name="triggerPointCode">The trigger point code.</param>
    /// <param name="eventType">Type of the event.</param>
    /// <param name="requestData">The request data.</param>
    /// <param name="responseData">The response data.</param>
    /// <returns></returns>
    public static TriggerRequest Create(string triggerPointCode, string eventType, string? requestData, string? responseData)
    {
        var triggerRequest = new TriggerRequest(triggerPointCode, eventType, requestData, responseData);

        return triggerRequest;
    }
}