using Aperia.SlaOla.Api.Entities;
using Aperia.SlaOla.Api.Models;

namespace Aperia.SlaOla.Api.Commands.CalculateLa;

/// <summary>
/// The Calculate La Command
/// </summary>
public record CalculateLaCommand(string Source, Guid ObjectId, List<ChangeHistory>? ChangeHistories) : IRequest<ErrorOr<LaObject>>;