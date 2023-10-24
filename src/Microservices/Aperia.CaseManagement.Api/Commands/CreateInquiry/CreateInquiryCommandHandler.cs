using Aperia.CaseManagement.Api.Entities;
using Aperia.CaseManagement.Api.Models;
using Aperia.CaseManagement.Api.Models.Acu;
using Aperia.CaseManagement.Api.Models.ContactManagement;
using Aperia.CaseManagement.Api.Models.Ownership;
using Aperia.CaseManagement.Api.Models.SlaOla;
using Aperia.CaseManagement.Api.Repositories;
using Aperia.CaseManagement.Api.Services;
using Aperia.Core.Application.Repositories;
using Aperia.Core.Application.Services;

namespace Aperia.CaseManagement.Api.Commands.CreateInquiry;

/// <summary>
/// The Add Inquiry Command Handler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{CreateInquiryCommand, InquiryDto}" />
public class CreateInquiryCommandHandler : IRequestHandler<CreateInquiryCommand, ErrorOr<InquiryDto>>
{
    /// <summary>
    /// The application context
    /// </summary>
    private readonly IAppContext _appContext;

    /// <summary>
    /// The date time provider
    /// </summary>
    private readonly IDateTimeProvider _dateTimeProvider;

    /// <summary>
    /// The json serializer
    /// </summary>
    private readonly IJsonSerializer _jsonSerializer;

    /// <summary>
    /// The unit of work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// The inquiry repository
    /// </summary>
    private readonly IInquiryRepository _inquiryRepository;

    /// <summary>
    /// The acu service
    /// </summary>
    private readonly IAcuService _acuService;

    /// <summary>
    /// The contact service
    /// </summary>
    private readonly IContactService _contactService;

    /// <summary>
    /// The ownership service
    /// </summary>
    private readonly IOwnershipService _ownershipService;

    /// <summary>
    /// The ola service
    /// </summary>
    private readonly ISlaOlaService _olaService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInquiryCommandHandler" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="jsonSerializer">The json serializer.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="inquiryRepository">The contact repository.</param>
    /// <param name="acuService">The acu service.</param>
    /// <param name="contactService">The contact service.</param>
    /// <param name="ownershipService">The ownership service.</param>
    /// <param name="olaService">The ola service.</param>
    public CreateInquiryCommandHandler(IAppContext appContext, IDateTimeProvider dateTimeProvider, IJsonSerializer jsonSerializer, IUnitOfWork unitOfWork, IInquiryRepository inquiryRepository,
                                    IAcuService acuService, IContactService contactService, IOwnershipService ownershipService, ISlaOlaService olaService)
    {
        this._appContext = appContext;
        this._dateTimeProvider = dateTimeProvider;
        this._jsonSerializer = jsonSerializer;
        this._unitOfWork = unitOfWork;
        this._inquiryRepository = inquiryRepository;
        this._acuService = acuService;
        this._contactService = contactService;
        this._ownershipService = ownershipService;
        this._olaService = olaService;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Response from the request
    /// </returns>
    public async Task<ErrorOr<InquiryDto>> Handle(CreateInquiryCommand request, CancellationToken cancellationToken)
    {
        var ownerResponse = await this._ownershipService.GetOwnerAsync(new GetOwnerRequest
        {
            Source = this._appContext.Name,
            ObjectType = nameof(Inquiry),
            Status = request.Status.ToString(),
            SecondaryStatus = request.SecondaryStatus
        });

        if (ownerResponse?.OwnerId == null || ownerResponse.OwnerId == Guid.Empty)
        {
            return Error.Failure("Owner.Error", "Failed to get owner information");
        }

        var currentDate = this._dateTimeProvider.Now;
        var inquiry = Inquiry.Create(this._appContext.Name, request.EntityId, request.Status, request.SecondaryStatus, currentDate);
        inquiry.OwnerType = ownerResponse.OwnerType;
        inquiry.OwnerId = ownerResponse.OwnerId;

        if (ownerResponse.OwnerType == OwnerType.User)
        {
            inquiry.Assignee = ownerResponse.OwnerId;
        }

        await this._inquiryRepository.AddAsync(inquiry);
        var count = await this._unitOfWork.SaveChangesAsync(cancellationToken);
        if (count == 0)
        {
            return Error.Failure("Inquiry.Error", "Failed to create inquiry information");
        }

        var addContactResponse = await this._contactService.AddContactAsync(new AddContactRequest
        {
            ObjectId = inquiry.Id,
            ContactName = request.ContactName,
            PhoneNumber = request.PhoneNumber
        });

        if (addContactResponse == null || addContactResponse.Id == Guid.Empty)
        {
            return Error.Failure("Contact.Error", "Failed to add contact information");
        }

        var calculateLaResponse = await this._olaService.CalculateLaAsync(new CalculateLaRequest
        {
            ObjectId = inquiry.Id,
            Source = this._appContext.Name,
            ChangeHistories = new List<ChangeHistory>
            {
                new()
                {
                    Attribute = "OwnerType",
                    Value = inquiry.OwnerType.ToString()
                },
                new()
                {
                    Attribute = "OwnerId",
                    Value = inquiry.OwnerId.ToString()
                },
                new()
                {
                    Attribute = "Status",
                    Value = inquiry.Status.ToString()
                },
                new()
                {
                    Attribute = "SecondaryStatus",
                    Value = inquiry.SecondaryStatus
                },
                new()
                {
                    Attribute = "CreatedDate",
                    Value = inquiry.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss")
                }
            }
        });

        if (calculateLaResponse == null || calculateLaResponse.Id == Guid.Empty)
        {
            return Error.Failure("La.Error", "Failed to calculate LA information");
        }

        var triggerActionResponse = await this._acuService.TriggerActionAsync(new TriggerActionRequest
        {
            TriggerPointCode = this._appContext.Name,
            EventType = "Inquiry.Created",
            RequestData = this._jsonSerializer.Serialize(inquiry)
        });

        if (triggerActionResponse == null)
        {
            return Error.Failure("Acu.Error", "Failed to trigger ACU request");
        }

        return new InquiryDto
        {
            Id = inquiry.Id,
            EntityId = inquiry.EntityId,
            OwnerType = inquiry.OwnerType,
            OwnerId = inquiry.OwnerId,
            Assignee = inquiry.Assignee,
            Status = inquiry.Status,
            SecondaryStatus = inquiry.SecondaryStatus,
            AcuTriggerId = triggerActionResponse.Id,
            ContactId = addContactResponse.Id,
            StatusDate = inquiry.StatusDate,
            CreatedDate = inquiry.CreatedDate
        };
    }
}