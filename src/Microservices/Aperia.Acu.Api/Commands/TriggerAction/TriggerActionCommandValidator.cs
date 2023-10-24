namespace Aperia.Acu.Api.Commands.TriggerAction;

/// <summary>
/// The Trigger Action Command Validator
/// </summary>
/// <seealso cref="FluentValidation.AbstractValidator{TriggerActionCommand}" />
public class TriggerActionCommandValidator : AbstractValidator<TriggerActionCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TriggerActionCommandValidator"/> class.
    /// </summary>
    public TriggerActionCommandValidator()
    {
        RuleFor(x => x.TriggerPointCode).NotEmpty().MaximumLength(20);
        RuleFor(x => x.EventType).NotEmpty().MaximumLength(150);
    }
}