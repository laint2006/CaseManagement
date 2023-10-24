using Aperia.ContactManagement.Api.Entities;
using Aperia.ContactManagement.Api.Repositories;
using Aperia.Core.Application.Repositories;

namespace Aperia.ContactManagement.Api.Commands.AddContact;

/// <summary>
/// The Add Contact Command Handler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{AddContactCommand, Contact}" />
public class AddContactCommandHandler : IRequestHandler<AddContactCommand, ErrorOr<Contact>>
{
    /// <summary>
    /// The unit of work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// The contact repository
    /// </summary>
    private readonly IContactRepository _contactRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddContactCommandHandler" /> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="contactRepository">The contact repository.</param>
    public AddContactCommandHandler(IUnitOfWork unitOfWork, IContactRepository contactRepository)
    {
        this._unitOfWork = unitOfWork;
        this._contactRepository = contactRepository;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Response from the request
    /// </returns>
    public async Task<ErrorOr<Contact>> Handle(AddContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await this._contactRepository.GetByContactNameAsync(request.ObjectId, request.ContactName, request.PhoneNumber);
        if (contact is not null)
        {
            return contact;
        }

        contact = Contact.Create(request.ObjectId, request.ContactName, request.PhoneNumber);

        await this._contactRepository.AddAsync(contact);
        await this._unitOfWork.SaveChangesAsync(cancellationToken);

        return contact;
    }
}