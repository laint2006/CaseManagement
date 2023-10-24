using Aperia.CaseManagement.Api.Models.SlaOla;
using Aperia.Core.Presentation.Http;

namespace Aperia.CaseManagement.Api.Services;

/// <summary>
/// The Sla Ola Service
/// </summary>
/// <seealso cref="Aperia.CaseManagement.Api.Services.ISlaOlaService" />
public class SlaOlaService : ISlaOlaService
{
    /// <summary>
    /// The client
    /// </summary>
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="SlaOlaService"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    public SlaOlaService(IHttpClientFactory httpClientFactory)
    {
        this._client = httpClientFactory.CreateClient("slaola");
    }

    /// <summary>
    /// Calculates the la asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public async Task<CalculateLaResponse?> CalculateLaAsync(CalculateLaRequest request)
    {
        var response = await this._client.PostAsJsonAsync("api/la/calculate", request);

        return await response.ReadAs<CalculateLaResponse>();
    }

}