using Aperia.SlaOla.Api.Commands.CalculateLa;
using Aperia.SlaOla.Api.Models;
using Microsoft.AspNetCore.RateLimiting;

namespace Aperia.SlaOla.Api.Controllers;

/// <summary>
/// The La Controller
/// </summary>
/// <seealso cref="Aperia.Core.Presentation.ApiController" />
[Route("api/la")]
[EnableRateLimiting("api_rate_limit_policy")]
public class LaController : ApiController
{
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly ISender _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="LaController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public LaController(ISender mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Calculates the la asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    [HttpPost("calculate")]
    public async Task<IActionResult> CalculateLaAsync(CalculateLaRequest request)
    {
        var command = new CalculateLaCommand(request.Source, request.ObjectId, request.ChangeHistories);
        var addResult = await _mediator.Send(command);

        return addResult.Match(result => Ok(new CalculateLaResponse
        {
            Id = result.Id,
        }), Problem);
    }

}