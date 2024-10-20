using MediatR;

namespace ESys.Application.Features.BusinessForm.Queries.GetCalculatedBusinessForm;
public class GetCalculatedBusinessFormQuery : IRequest<GetCalculatedBusinessFormResponse>
{
    // Api request body as Json string
    public string Body { set; get; } = string.Empty;
}
