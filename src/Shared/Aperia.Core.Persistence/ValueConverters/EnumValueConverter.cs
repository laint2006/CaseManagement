using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aperia.Core.Persistence.ValueConverters;

/// <summary>
/// The Enum Value Converter
/// </summary>
/// <typeparam name="TEnum">The type of the enum.</typeparam>
/// <seealso cref="Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter{TEnum, String}" />
public class EnumValueConverter<TEnum> : ValueConverter<TEnum?, string?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnumValueConverter{TEnum}"/> class.
    /// </summary>
    public EnumValueConverter()
        : base(@enum => ConvertToString(@enum), value => ConvertToEnum(value))
    {
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <param name="enum">The enum.</param>
    /// <returns></returns>
    private static string? ConvertToString(TEnum? @enum)
    {
        return @enum is null ? null : @enum.ToString();
    }

    /// <summary>
    /// Converts to enum.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    private static TEnum? ConvertToEnum(string? value)
    {
        return value is null ? default : (TEnum)Enum.Parse(typeof(TEnum), value);
    }
}