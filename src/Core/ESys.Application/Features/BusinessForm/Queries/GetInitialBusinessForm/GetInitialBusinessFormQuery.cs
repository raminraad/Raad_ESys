using MediatR;

namespace ESys.Application.Features.BusinessForm.Queries.GetInitialBusinessForm;
public class GetInitialBusinessFormQuery : IRequest<GetInitialBusinessFormQueryResult>
{
    public int BusinessId { init; get; }
}
