using Aperia.CaseManagement.Api.Commands.AsyncCreateInquiry;
using Aperia.CaseManagement.Api.Commands.CreateInquiry;
using Aperia.CaseManagement.Api.Models;
using Microsoft.AspNetCore.RateLimiting;

namespace Aperia.CaseManagement.Api.Controllers;

/// <summary>
/// The Inquiry Controller
/// </summary>
/// <seealso cref="Aperia.Core.Presentation.ApiController" />
[Route("api/inquiry")]
[EnableRateLimiting("api_rate_limit_policy")]
public class InquiryController : ApiController
{
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly ISender _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="InquiryController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public InquiryController(ISender mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Adds the inquiry asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> CreateInquiryAsync(CreateInquiryRequest request)
    {
        var command = new CreateInquiryCommand(request.EntityId, request.Status,  request.SecondaryStatus, request.ContactName, request.PhoneNumber);
        var addResult = await _mediator.Send(command);

        return addResult.Match(inquiry => Ok(new CreateInquiryResponse
        {
            Id = inquiry.Id,
            EntityId = inquiry.EntityId,
            OwnerType = inquiry.OwnerType,
            OwnerId = inquiry.OwnerId,
            Assignee = inquiry.Assignee,
            Status = inquiry.Status,
            SecondaryStatus = inquiry.SecondaryStatus,
            AcuTriggerId = inquiry.AcuTriggerId,
            ContactId = inquiry.ContactId,
            StatusDate = inquiry.StatusDate,
            CreatedDate = inquiry.CreatedDate
        }), Problem);
    }

    /// <summary>
    /// Adds the inquiry asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    [HttpPost("async-create")]
    public async Task<IActionResult> AsyncCreateInquiryAsync(CreateInquiryRequest request)
    {
        var command = new AsyncCreateInquiryCommand(request.EntityId, request.Status, request.SecondaryStatus, request.ContactName, request.PhoneNumber);
        var addResult = await _mediator.Send(command);

        return addResult.Match(inquiry => Ok(new CreateInquiryResponse
        {
            Id = inquiry.Id,
            EntityId = inquiry.EntityId,
            OwnerType = inquiry.OwnerType,
            OwnerId = inquiry.OwnerId,
            Assignee = inquiry.Assignee,
            Status = inquiry.Status,
            SecondaryStatus = inquiry.SecondaryStatus,
            StatusDate = inquiry.StatusDate,
            CreatedDate = inquiry.CreatedDate
        }), Problem);
    }

}