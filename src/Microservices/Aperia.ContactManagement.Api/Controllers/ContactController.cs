using Aperia.ContactManagement.Api.Commands.AddContact;
using Aperia.ContactManagement.Api.Models;
using Microsoft.AspNetCore.RateLimiting;

namespace Aperia.ContactManagement.Api.Controllers;

/// <summary>
/// The Contact Controller
/// </summary>
/// <seealso cref="Aperia.Core.Presentation.ApiController" />
[Route("api/contact")]
[EnableRateLimiting("api_rate_limit_policy")]
public class ContactController : ApiController
{
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly ISender _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public ContactController(ISender mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Adds the contact asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task<IActionResult> AddContactAsync(AddContactRequest request)
    {
        var command = new AddContactCommand(request.ObjectId, request.ContactName, request.PhoneNumber);
        var addResult = await _mediator.Send(command);

        return addResult.Match(result => Ok(new AddContactResponse
        {
            Id = result.Id,
            ObjectId = result.ObjectId,
            ContactName = result.ContactName,
            PhoneNumber = result.PhoneNumber
        }), Problem);
    }

}