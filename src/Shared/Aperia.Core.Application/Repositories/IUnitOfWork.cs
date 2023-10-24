namespace Aperia.Core.Application.Repositories;

/// <summary>
/// The IUnitOfWork interface
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves the changes.
    /// </summary>
    /// <returns></returns>
    int SaveChanges();

    /// <summary>
    /// Saves the changes asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}