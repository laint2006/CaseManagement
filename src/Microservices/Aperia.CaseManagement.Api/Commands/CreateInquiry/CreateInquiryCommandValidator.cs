namespace Aperia.CaseManagement.Api.Commands.CreateInquiry;

/// <summary>
/// The Add Inquiry Command Validator
/// </summary>
/// <seealso cref="FluentValidation.AbstractValidator{CreateInquiryCommand}" />
public class CreateInquiryCommandValidator : AbstractValidator<CreateInquiryCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInquiryCommandValidator"/> class.
    /// </summary>
    public CreateInquiryCommandValidator()
    {
        RuleFor(x => x.ContactName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(8).MaximumLength(15);
    }
}