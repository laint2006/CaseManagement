using Aperia.CaseManagement.Api.Entities;
using Aperia.CaseManagement.Api.Models;

namespace Aperia.CaseManagement.Api.Commands.CreateInquiry;

/// <summary>
/// The Add Inquiry Command
/// </summary>
public record CreateInquiryCommand(string? EntityId, InquiryStatus Status, string? SecondaryStatus, string ContactName, string PhoneNumber) : IRequest<ErrorOr<InquiryDto>>;