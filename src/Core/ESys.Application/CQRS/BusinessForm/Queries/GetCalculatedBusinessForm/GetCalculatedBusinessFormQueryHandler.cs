using ESys.Application.Abstractions.Services.BusinessForm;
using MediatR;

namespace ESys.Application.CQRS.BusinessForm.Queries.GetCalculatedBusinessForm;

/// <summary>
/// Request handler for BusinessForm calculation. This class is used by mediator
/// </summary>
/// <param name="businessFormCalculator">Calculation provider gotten through dependency injection</param>
public class GetCalculatedBusinessFormQueryHandler(BusinessFormCalculator businessFormCalculator)
    : IRequestHandler<GetCalculatedBusinessFormQuery, GetCalculatedBusinessFormQueryResult>
{
    /// <summary>
    /// Base method of request handling which gets run automatically by mediator
    /// </summary>
    /// <param name="request">Request gotten from end point containing data needed for calculation service</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Corresponding response containing calculation data</returns>
    public Task<GetCalculatedBusinessFormQueryResult> Handle(GetCalculatedBusinessFormQuery request,
        CancellationToken cancellationToken)
    {
        var result = new GetCalculatedBusinessFormQueryResult()
        {
            Result = businessFormCalculator.GetCalculatedBusinessForm(request.Body).Result
        };
        return Task.FromResult(result);
    }

}