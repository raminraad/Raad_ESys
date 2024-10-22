using MediatR;

namespace ESys.Application.Features.BusinessForm.Commands.GenerateJwtForBusinessForm;

public record GenerateJwtForBusinessFormCommand() : IRequest<string>
{
    public required string BusinessId { get; set; }
    public required string BusinessToken { get; set; }
    public required string IpAddress { get; set; }
    public required string ClientSessionId { get; set; }
}