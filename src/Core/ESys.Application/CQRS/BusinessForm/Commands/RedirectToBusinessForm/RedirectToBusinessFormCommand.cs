using MediatR;

namespace ESys.Application.CQRS.BusinessForm.Commands.RedirectToBusinessForm;

public record RedirectToBusinessFormCommand() : IRequest<RedirectToBusinessFormResponse>
{
    public required string BusinessId { get; set; }
    public required string BusinessToken { get; set; }
    public required string IpAddress { get; set; }
    public required string ClientSessionId { get; set; }
}