using Aperia.CaseManagement.Api.Models.Ownership;
using Aperia.Core.Presentation.Http;

namespace Aperia.CaseManagement.Api.Services;

/// <summary>
/// The Ownership Service
/// </summary>
/// <seealso cref="Aperia.CaseManagement.Api.Services.IOwnershipService" />
public class OwnershipService : IOwnershipService
{
    /// <summary>
    /// The client
    /// </summary>
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="OwnershipService"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    public OwnershipService(IHttpClientFactory httpClientFactory)
    {
        this._client = httpClientFactory.CreateClient("ownership");
    }

    /// <summary>
    /// Gets the owner asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<GetOwnerResponse?> GetOwnerAsync(GetOwnerRequest request)
    {
        var response = await this._client.PostAsJsonAsync("api/owner/get-owner", request);

        return await response.ReadAs<GetOwnerResponse>();
    }

}