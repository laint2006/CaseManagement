namespace Aperia.CaseManagement.Api.Commands.AsyncCreateInquiry;

/// <summary>
/// The Async Create Inquiry Command Validator
/// </summary>
/// <seealso cref="FluentValidation.AbstractValidator{CreateInquiryCommand}" />
public class AsyncCreateInquiryCommandValidator : AbstractValidator<AsyncCreateInquiryCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncCreateInquiryCommandValidator"/> class.
    /// </summary>
    public AsyncCreateInquiryCommandValidator()
    {
        RuleFor(x => x.ContactName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(8).MaximumLength(15);
    }
}