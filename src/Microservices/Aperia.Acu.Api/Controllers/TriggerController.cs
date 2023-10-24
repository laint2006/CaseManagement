using Aperia.Acu.Api.Commands.TriggerAction;
using Aperia.Acu.Api.Models;
using Microsoft.AspNetCore.RateLimiting;

namespace Aperia.Acu.Api.Controllers;

/// <summary>
/// The Trigger Controller
/// </summary>
/// <seealso cref="Aperia.Core.Presentation.ApiController" />
[Route("api/trigger")]
[EnableRateLimiting("api_rate_limit_policy")]
public class TriggerController : ApiController
{ 
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly ISender _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="TriggerController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public TriggerController(ISender mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Triggers the action asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    [HttpPost("trigger-action")]
    public async Task<IActionResult> TriggerActionAsync(TriggerActionRequest request)
    {
        var command = new TriggerActionCommand(request.TriggerPointCode, request.EventType, request.RequestData);
        var addResult = await _mediator.Send(command);

        return addResult.Match(result => Ok(new TriggerActionResponse
        {
            Id = result.Id
        }), Problem);
    }

}