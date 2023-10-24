using Aperia.Ownership.Api.Models;
using Aperia.Ownership.Api.Repositories;

namespace Aperia.Ownership.Api.Queries.GetOwner;

/// <summary>
/// The Get Owner Query Handler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{AddContactCommand, Owner}" />
public class GetOwnerQueryHandler : IRequestHandler<GetOwnerQuery, ErrorOr<OwnerDto?>>
{
    /// <summary>
    /// The contact repository
    /// </summary>
    private readonly IOwnerRepository _ownerRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetOwnerQueryHandler" /> class.
    /// </summary>
    /// <param name="ownerRepository">The contact repository.</param>
    public GetOwnerQueryHandler(IOwnerRepository ownerRepository)
    {
        this._ownerRepository = ownerRepository;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Response from the request
    /// </returns>
    public async Task<ErrorOr<OwnerDto?>> Handle(GetOwnerQuery request, CancellationToken cancellationToken)
    {
        var owner = await this._ownerRepository.GetOwnerAsync(request);
        if (owner is null)
        {
            return new OwnerDto();
        }

        return new OwnerDto
        {
            OwnerType = owner.OwnerType,
            OwnerId = owner.OwnerId,
            Name = owner.Name
        };
    }
}