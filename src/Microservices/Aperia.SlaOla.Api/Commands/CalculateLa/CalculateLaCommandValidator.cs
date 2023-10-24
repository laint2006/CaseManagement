namespace Aperia.SlaOla.Api.Commands.CalculateLa;

/// <summary>
/// The Calculate La Command Validator
/// </summary>
/// <seealso cref="FluentValidation.AbstractValidator{CalculateLaCommand}" />
public class CalculateLaCommandValidator : AbstractValidator<CalculateLaCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CalculateLaCommandValidator"/> class.
    /// </summary>
    public CalculateLaCommandValidator()
    {
        RuleFor(x => x.ObjectId).NotEmpty();
        RuleFor(x => x.Source).NotEmpty().MaximumLength(20);
    }
}