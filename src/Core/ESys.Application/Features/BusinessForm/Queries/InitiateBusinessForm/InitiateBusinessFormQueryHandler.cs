using ESys.Application.Abstractions.Services.BusinessForm;
using MediatR;

namespace ESys.Application.Features.BusinessForm.Queries.InitiateBusinessForm;

/// <summary>
/// Request handler for Business Form initialization. This class is used by mediator
/// </summary>
/// <param name="businessFormInitiator">initialization provider gotten through dependency injection</param>
public class InitiateBusinessFormQueryHandler(BusinessFormInitiator businessFormInitiator)
    : IRequestHandler<InitiateBusinessFormQuery, string>
{
    /// <summary>
    /// Base method of request handling which gets run automatically by mediator
    /// </summary>
    /// <param name="request">Request gotten from end point containing data needed for initialization service</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Corresponding response containing initialization data</returns>
    public Task<string> Handle(InitiateBusinessFormQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(businessFormInitiator.GetInitialBusinessForm(request.BusinessId).Result);
    }
}