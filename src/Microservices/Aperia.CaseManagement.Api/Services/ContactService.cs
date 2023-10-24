using Aperia.CaseManagement.Api.Models.ContactManagement;
using Aperia.Core.Presentation.Http;

namespace Aperia.CaseManagement.Api.Services;

/// <summary>
/// The Contact Service
/// </summary>
/// <seealso cref="Aperia.CaseManagement.Api.Services.IContactService" />
public class ContactService : IContactService
{
    /// <summary>
    /// The client
    /// </summary>
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactService"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory.</param>
    public ContactService(IHttpClientFactory httpClientFactory)
    {
        this._client = httpClientFactory.CreateClient("contactManagement");
    }

    /// <summary>
    /// Adds the contact asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<AddContactResponse?> AddContactAsync(AddContactRequest request)
    {
        var response = await this._client.PostAsJsonAsync("api/contact/add", request);

        return await response.ReadAs<AddContactResponse>();
    }

}