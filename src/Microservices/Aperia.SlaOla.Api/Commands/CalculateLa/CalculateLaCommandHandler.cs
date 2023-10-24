using Aperia.Core.Application.Repositories;
using Aperia.SlaOla.Api.Entities;
using Aperia.SlaOla.Api.Repositories;

namespace Aperia.SlaOla.Api.Commands.CalculateLa;

/// <summary>
/// The Calculate La Command Handler
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{CalculateLaCommand, LaObject}" />
public class CalculateLaCommandHandler : IRequestHandler<CalculateLaCommand, ErrorOr<LaObject>>
{
    /// <summary>
    /// The unit of work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// The la repository
    /// </summary>
    private readonly ILaRepository _laRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculateLaCommandHandler" /> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="laRepository">The contact repository.</param>
    public CalculateLaCommandHandler(IUnitOfWork unitOfWork, ILaRepository laRepository)
    {
        this._unitOfWork = unitOfWork;
        this._laRepository = laRepository;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Response from the request
    /// </returns>
    public async Task<ErrorOr<LaObject>> Handle(CalculateLaCommand request, CancellationToken cancellationToken)
    {
        var laObject = await this._laRepository.GetByObjectIdAsync(request.Source, request.ObjectId);
        if (laObject is null)
        {
            return await this.AddLaObjectAsync(request, cancellationToken);
        }

        return await this.UpdateLaObjectAsync(request, laObject, cancellationToken);
    }

    /// <summary>
    /// Adds the la object asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    private async Task<ErrorOr<LaObject>> AddLaObjectAsync(CalculateLaCommand request, CancellationToken cancellationToken)
    {
        var createResult = LaObject.Create(request.Source, request.ObjectId);
        if (createResult.IsError)
        {
            return createResult;
        }

        var laObject = createResult.Value;

        var addHistoryResult = this.AddChangeHistories(request, laObject);
        if (addHistoryResult.IsError)
        {
            return addHistoryResult;
        }

        await this._laRepository.AddAsync(laObject);
        await this._unitOfWork.SaveChangesAsync(cancellationToken);

        return laObject;
    }

    /// <summary>
    /// Updates the la object asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="laObject">The la object.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    private async Task<ErrorOr<LaObject>> UpdateLaObjectAsync(CalculateLaCommand request, LaObject laObject, CancellationToken cancellationToken)
    {
        var addHistoryResult = this.AddChangeHistories(request, laObject);
        if (addHistoryResult.IsError)
        {
            return addHistoryResult;
        }

        this._laRepository.Update(laObject);
        await this._unitOfWork.SaveChangesAsync(cancellationToken);

        return laObject;
    }

    /// <summary>
    /// Adds the change histories.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="laObject">The la object.</param>
    /// <returns></returns>
    private ErrorOr<LaObject> AddChangeHistories(CalculateLaCommand request, LaObject laObject)
    {
        if (request.ChangeHistories is not { Count: > 0 })
        {
            return laObject;
        }

        var changeId = Guid.NewGuid();
        foreach (var changeHistory in request.ChangeHistories)
        {
            var addResult = laObject.AddChangeHistory(changeId, changeHistory.Attribute, changeHistory.Value);
            if (addResult.IsError)
            {
                return addResult.Errors;
            }
        }

        return laObject;
    }
}