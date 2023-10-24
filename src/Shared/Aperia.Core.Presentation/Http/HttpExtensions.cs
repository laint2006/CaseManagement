using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aperia.Core.Presentation.Http
{
    /// <summary>
    /// The HTTP Extensions
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// The json serializer options
        /// </summary>
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        /// <summary>
        /// Initializes the <see cref="HttpExtensions"/> class.
        /// </summary>
        static HttpExtensions()
        {
            JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        /// <summary>
        /// Reads the content as.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Something went wrong calling the API: {response.ReasonPhrase}</exception>
        public static async Task<T?> ReadAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
            }

            var dataAsString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(dataAsString, JsonSerializerOptions);
        }

    }
}