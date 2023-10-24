using Aperia.Core.Application.Repositories;
using Aperia.SlaOla.Api.Entities;

namespace Aperia.SlaOla.Api.Repositories;

/// <summary>
/// The ILaRepository interface
/// </summary>
public interface ILaRepository : IRepository<LaObject>
{
    /// <summary>
    /// Gets the by object identifier asynchronous.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="objectId">The object identifier.</param>
    /// <returns></returns>
    Task<LaObject?> GetByObjectIdAsync(string source, Guid objectId);
}