using Aperia.CaseManagement.Api.Models.Acu;
using Aperia.Core.Presentation.Http;

namespace Aperia.CaseManagement.Api.Services;

/// <summary>
/// The Acu Service
/// </summary>
/// <seealso cref="Aperia.CaseManagement.Api.Services.IAcuService" />
public class AcuService : IAcuService
{
    /// <summary>
    /// The client
    /// </summary>
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcuService"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    public AcuService(IHttpClientFactory httpClientFactory)
    {
        this._client = httpClientFactory.CreateClient("acu");
    }

    /// <summary>
    /// Triggers the action asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<TriggerActionResponse?> TriggerActionAsync(TriggerActionRequest request)
    {
        var response = await this._client.PostAsJsonAsync("api/trigger/trigger-action", request);

        return await response.ReadAs<TriggerActionResponse>();
    }

}