using ESys.Application.Abstractions.Services.BusinessForm;
using MediatR;

namespace ESys.Application.Features.BusinessForm.Queries.CalculateBusinessForm;

/// <summary>
/// Request handler for BusinessForm calculation. This class is used by mediator
/// </summary>
/// <param name="businessFormCalculator">Calculation provider gotten through dependency injection</param>
public class CalculateBusinessFormQueryHandler(BusinessFormCalculator businessFormCalculator)
    : IRequestHandler<CalculateBusinessFormQuery, string>
{
    /// <summary>
    /// Base method of request handling which gets run automatically by mediator
    /// </summary>
    /// <param name="request">Request gotten from end point containing data needed for calculation service</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Corresponding response containing calculation data</returns>
    public Task<string> Handle(CalculateBusinessFormQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(businessFormCalculator.GetCalculatedBusinessForm(request.Body).Result);
    }

}