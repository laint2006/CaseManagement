using Aperia.Acu.Api.Entities;

namespace Aperia.Acu.Api.Commands.TriggerAction;

/// <summary>
/// The Trigger Action Command
/// </summary>
public record TriggerActionCommand(string TriggerPointCode, string EventType, string? RequestData) : IRequest<ErrorOr<TriggerRequest>>;