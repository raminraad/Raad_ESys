using MediatR;

namespace ESys.Application.CQRS.BusinessForm.Queries.GetInitialBusinessForm;
public class GetInitialBusinessFormQuery : IRequest<GetInitialBusinessFormQueryResult>
{
    public string BusinessId { set; get; } = string.Empty;
}
