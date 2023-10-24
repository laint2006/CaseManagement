namespace Aperia.ContactManagement.Api.Commands.AddContact;

/// <summary>
/// The Add Contact Command Validator
/// </summary>
/// <seealso cref="FluentValidation.AbstractValidator{AddContactCommand}" />
public class AddContactCommandValidator : AbstractValidator<AddContactCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddContactCommandValidator"/> class.
    /// </summary>
    public AddContactCommandValidator()
    {
        RuleFor(x => x.ObjectId).NotEmpty();
        RuleFor(x => x.ContactName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(8).MaximumLength(15);
    }
}