namespace Aperia.Core.Application.Services
{
    /// <summary>
    /// The IJsonSerializer interface
    /// </summary>
    public interface IJsonSerializer
    {
        /// <summary>
        /// Serializes the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        string? Serialize<T>(T? value);

        /// <summary>
        /// Deserializes the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        T? Deserialize<T>(string? value);
    }
}