using MediatR;

namespace ESys.Application.CQRS.BusinessForm.Queries.GetCalculatedBusinessForm;
public class GetCalculatedBusinessFormQuery : IRequest<GetCalculatedBusinessFormResponse>
{
    // Api request body as Json string
    public string Body { set; get; } = string.Empty;
}
