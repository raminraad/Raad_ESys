using MediatR;

namespace ESys.Application.Features.BusinessForm.Commands.SubmitBusinessForm;

public record SubmitBusinessFormCommand : IRequest<bool>
{
    // Api request body as Json string
    public required string SubmissionInput { init; get; }
    public required Guid TempRoute { init; get; }
    public required string ClientToken { init; get; }
}