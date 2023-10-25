using Aperia.ContactManagement.Api.Entities;

namespace Aperia.ContactManagement.Api.Commands.AddContact;

/// <summary>
/// The Add Contact Command
/// </summary>
public record AddContactCommand(Guid ObjectId, string? ContactName, string? PhoneNumber) : IRequest<ErrorOr<Contact>>;