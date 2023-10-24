using Aperia.Ownership.Api.Models;
using Aperia.Ownership.Api.Queries.GetOwner;
using Microsoft.AspNetCore.RateLimiting;

namespace Aperia.Ownership.Api.Controllers;

/// <summary>
/// The Owner Controller
/// </summary>
/// <seealso cref="Aperia.Core.Presentation.ApiController" />
[Route("api/owner")]
[EnableRateLimiting("api_rate_limit_policy")]
public class OwnerController : ApiController
{ 
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly ISender _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="OwnerController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public OwnerController(ISender mediator)
    {
        this._mediator = mediator;
    }

    /// <summary>
    /// Gets the owner asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    [HttpPost("get-owner")]
    public async Task<IActionResult> GetOwnerAsync(GetOwnerRequest request)
    {
        var query = new GetOwnerQuery(request.Source, request.ObjectType, request.Status, request.SecondaryStatus);
        var getResult = await _mediator.Send(query);

        return getResult.Match(result => Ok(new GetOwnerResponse
        {
            OwnerType = result.OwnerType,
            OwnerId = result.OwnerId,
            Name = result.Name
        }), Problem);
    }

}