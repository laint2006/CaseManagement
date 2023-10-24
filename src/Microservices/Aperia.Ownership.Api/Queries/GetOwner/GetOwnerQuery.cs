using Aperia.Ownership.Api.Entities;
using Aperia.Ownership.Api.Models;

namespace Aperia.Ownership.Api.Queries.GetOwner;

/// <summary>
/// The Get Owner Query
/// </summary>
public record GetOwnerQuery(string? Source, string? ObjectType, string? Status, string? SecondaryStatus) : IRequest<ErrorOr<OwnerDto?>>;