using Aperia.Acu.Api.Entities;
using Aperia.Acu.Api.Repositories;
using Aperia.Core.Application.Repositories;

namespace Aperia.Acu.Api.Commands.TriggerAction;

/// <summary>
/// The Trigger Action Command Handler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{TriggerActionCommand, TriggerRequest}" />
public class TriggerActionCommandHandler : IRequestHandler<TriggerActionCommand, ErrorOr<TriggerRequest>>
{
    /// <summary>
    /// The unit of work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// The trigger repository
    /// </summary>
    private readonly ITriggerRepository _triggerRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="TriggerActionCommandHandler" /> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="triggerRepository">The contact repository.</param>
    public TriggerActionCommandHandler(IUnitOfWork unitOfWork, ITriggerRepository triggerRepository)
    {
        this._unitOfWork = unitOfWork;
        this._triggerRepository = triggerRepository;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Response from the request
    /// </returns>
    public async Task<ErrorOr<TriggerRequest>> Handle(TriggerActionCommand request, CancellationToken cancellationToken)
    {
        string? responseData = null;
        var triggerRequest = TriggerRequest.Create(request.TriggerPointCode, request.EventType, request.RequestData, responseData);

        await this._triggerRepository.AddAsync(triggerRequest);
        await this._unitOfWork.SaveChangesAsync(cancellationToken);

        return triggerRequest;
    }
}