namespace Aperia.Core.Application.Services;

/// <summary>
/// The Date Time Provider
/// </summary>
/// <seealso cref="Aperia.Core.Application.Services.IDateTimeProvider" />
public class DateTimeProvider : IDateTimeProvider
{
    /// <summary>
    /// Gets the now.
    /// </summary>
    public DateTime Now => DateTime.Now;

    /// <summary>
    /// The UTC now
    /// </summary>
    public DateTime UtcNow => DateTime.UtcNow;

}