using MediatR;

namespace ESys.Application.Features.BusinessForm.Queries.CalculateBusinessForm;
public class CalculateBusinessFormQuery : IRequest<string>
{
    // Api request body as Json string
    public string Body { set; get; } = string.Empty;
}
