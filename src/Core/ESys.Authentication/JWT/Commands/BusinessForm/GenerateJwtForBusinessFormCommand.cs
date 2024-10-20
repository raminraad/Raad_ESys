using ESys.Application.Abstractions.CQRS;
using MediatR;

namespace ESys.Authentication.JWT.Commands.BusinessForm;

public record GenerateJwtForBusinessFormCommand() : IRequest<string>
{
    public required string BusinessId { get; set; }
    public required string BusinessToken { get; set; }
    public required string IpAddress { get; set; }
    public required string ClientSessionId { get; set; }
}