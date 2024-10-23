using MediatR;

namespace ESys.Application.Features.BusinessForm.Queries.InitiateBusinessForm;
public class InitiateBusinessFormQuery : IRequest<string>
{
    public int BusinessId { init; get; }
}
