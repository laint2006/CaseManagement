namespace Aperia.Core.Application.Services;

/// <summary>
/// The IDateTimeProvider interface
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the now.
    /// </summary>
    DateTime Now { get; }

    /// <summary>
    /// Gets the UTC now.
    /// </summary>
    DateTime UtcNow { get; }
}