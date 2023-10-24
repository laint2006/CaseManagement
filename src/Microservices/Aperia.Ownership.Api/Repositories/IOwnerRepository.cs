using Aperia.Core.Application.Repositories;
using Aperia.Ownership.Api.Entities;
using Aperia.Ownership.Api.Queries.GetOwner;

namespace Aperia.Ownership.Api.Repositories;

/// <summary>
/// The IOwnerRepository interface
/// </summary>
public interface IOwnerRepository : IRepository<Owner, long>
{
    /// <summary>
    /// Gets the owner asynchronous.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    Task<Owner?> GetOwnerAsync(GetOwnerQuery query);
}