using Aperia.CaseManagement.Api.Entities;
using Aperia.CaseManagement.Api.Models;
using Aperia.CaseManagement.Api.Models.Ownership;
using Aperia.CaseManagement.Api.Repositories;
using Aperia.CaseManagement.Api.Services;
using Aperia.Core.Application.Repositories;
using Aperia.Core.Application.Services;
using Aperia.Core.Domain.Primitives;

namespace Aperia.CaseManagement.Api.Commands.AsyncCreateInquiry;

/// <summary>
/// The Async Create Inquiry Command Handler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{AsyncCreateInquiryCommand, InquiryDto}" />
public class AsyncCreateInquiryCommandHandler : IRequestHandler<AsyncCreateInquiryCommand, ErrorOr<InquiryDto>>
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
    /// The unit of work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// The inquiry repository
    /// </summary>
    private readonly IInquiryRepository _inquiryRepository;

    /// <summary>
    /// The ownership service
    /// </summary>
    private readonly IOwnershipService _ownershipService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncCreateInquiryCommandHandler" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="inquiryRepository">The contact repository.</param>
    /// <param name="ownershipService">The ownership service.</param>
    public AsyncCreateInquiryCommandHandler(IAppContext appContext, IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork, 
                                                IInquiryRepository inquiryRepository, IOwnershipService ownershipService)
    {
        this._appContext = appContext;
        this._dateTimeProvider = dateTimeProvider;
        this._unitOfWork = unitOfWork;
        this._inquiryRepository = inquiryRepository;
        this._ownershipService = ownershipService;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Response from the request
    /// </returns>
    public async Task<ErrorOr<InquiryDto>> Handle(AsyncCreateInquiryCommand request, CancellationToken cancellationToken)
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

        var inquiryDto = new InquiryDto
        {
            Id = inquiry.Id,
            EntityId = inquiry.EntityId,
            OwnerType = inquiry.OwnerType,
            OwnerId = inquiry.OwnerId,
            Assignee = inquiry.Assignee,
            Status = inquiry.Status,
            SecondaryStatus = inquiry.SecondaryStatus,
            StatusDate = inquiry.StatusDate,
            ContactName = request.ContactName,
            PhoneNumber = request.PhoneNumber,
            CreatedDate = inquiry.CreatedDate
        };
        inquiry.AddDomainEvent(DomainEvent.Create("Inquiry.Created", inquiry.Id.ToString(), inquiryDto));

        await this._inquiryRepository.AddAsync(inquiry);
        var count = await this._unitOfWork.SaveChangesAsync(cancellationToken);
        if (count == 0)
        {
            return Error.Failure("Inquiry.Error", "Failed to create inquiry information");
        }

        return inquiryDto;
    }
}