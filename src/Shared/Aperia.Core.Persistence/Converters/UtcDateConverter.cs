using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aperia.Core.Persistence.Converters;

/// <summary>
/// The UTC Date Converter
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter{DateTime, DateTime}" />
public class UtcDateConverter : ValueConverter<DateTime?, DateTime?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UtcDateConverter"/> class.
    /// </summary>
    public UtcDateConverter()
        : base(date => date, value => ConvertToUtc(value))
    {
    }

    /// <summary>
    /// Converts to enum.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    private static DateTime? ConvertToUtc(DateTime? value)
    {
        return value is null ? default : DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
    }
}