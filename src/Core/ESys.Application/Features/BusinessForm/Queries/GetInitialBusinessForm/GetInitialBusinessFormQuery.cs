using MediatR;

namespace ESys.Application.Features.BusinessForm.Queries.GetInitialBusinessForm;
public class GetInitialBusinessFormQuery : IRequest<GetInitialBusinessFormResponse>
{
    public string BusinessId { set; get; } = string.Empty;
}
