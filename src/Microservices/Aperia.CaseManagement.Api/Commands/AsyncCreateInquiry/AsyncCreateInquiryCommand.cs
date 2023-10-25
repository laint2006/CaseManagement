using Aperia.CaseManagement.Api.Entities;
using Aperia.CaseManagement.Api.Models;

namespace Aperia.CaseManagement.Api.Commands.AsyncCreateInquiry;

/// <summary>
/// The Async Create Inquiry Command
/// </summary>
public record AsyncCreateInquiryCommand(string? EntityId, InquiryStatus Status, string? SecondaryStatus, string ContactName, string PhoneNumber) : IRequest<ErrorOr<InquiryDto>>;