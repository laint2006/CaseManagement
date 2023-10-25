using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aperia.Core.Application.Services
{
    /// <summary>
    /// The Json Serializer
    /// </summary>
    /// <seealso cref="Aperia.Core.Application.Services.IJsonSerializer" />
    public class JsonSerializer : IJsonSerializer
    {
        /// <summary>
        /// The default options
        /// </summary>
        private static readonly JsonSerializerOptions DefaultOptions = new()
        {
            WriteIndented = false,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        /// <summary>
        /// Initializes the <see cref="JsonSerializer"/> class.
        /// </summary>
        static JsonSerializer()
        {
            DefaultOptions.Converters.Add(new JsonStringEnumConverter());
        }

        /// <summary>
        /// Serializes the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string? Serialize<T>(T? value)
        {
            return value is null ? null : System.Text.Json.JsonSerializer.Serialize(value, DefaultOptions);
        }

        /// <summary>
        /// Deserializes the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T? Deserialize<T>(string? value)
        {
            return value is null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value, DefaultOptions);
        }

    }
}