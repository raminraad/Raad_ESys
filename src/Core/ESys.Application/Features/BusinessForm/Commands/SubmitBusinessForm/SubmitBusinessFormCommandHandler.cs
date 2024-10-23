using AutoMapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Application.Abstractions.Services.BusinessForm;
using ESys.Application.Abstractions.Services.JWT;
using ESys.Application.Features.BusinessForm.Commands.SubmitBusinessForm;
using ESys.Domain.Entities;
using MediatR;

namespace ESys.Application.Features.BusinessForm.Queries.CalculateBusinessForm;

/// <summary>
/// Request handler for BusinessForm submission. This class is used by mediator
/// </summary>
/// <param name="businessFormCalculator">Calculation provider gotten through dependency injection(Being used for reevaluation)</param>
public class SubmitBusinessFormCommandHandler : IRequestHandler<SubmitBusinessFormCommand, bool>
{
    private readonly IClientSessionHistoryRepository _clientSessionHistoryRepository;
    private readonly IClientSessionCacheRepository _clientSessionCacheRepository;
    private readonly BusinessFormCalculator _businessFormCalculator;

    public SubmitBusinessFormCommandHandler(IClientSessionHistoryRepository clientSessionHistoryRepository,
        IClientSessionCacheRepository clientSessionCacheRepository, BusinessFormCalculator businessFormCalculator)
    {
        _clientSessionHistoryRepository = clientSessionHistoryRepository;
        _clientSessionCacheRepository = clientSessionCacheRepository;
        _businessFormCalculator = businessFormCalculator;
    }


    /// <summary>
    /// Base method of request handling which gets run automatically by mediator
    /// </summary>
    /// <param name="request">Request gotten from end point containing data needed for submission service</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Corresponding response containing data to be submitted</returns>
    public async Task<bool> Handle(SubmitBusinessFormCommand request,
        CancellationToken cancellationToken)
    {
        var recalculationTask = _businessFormCalculator.GetCalculatedBusinessForm(request.SubmissionInput);
        var clientSessionCacheTask = _clientSessionCacheRepository.GetByTempRoute(request.TempRoute);

        var recalculationResult = await recalculationTask;
        var clientSessionCache = await clientSessionCacheTask;

        if (clientSessionCache is not null)
        {
            var clientSessionHistory = new ClientSessionHistory
            {
                ClientToken = request.ClientToken,
                BusinessId = clientSessionCache.BusinessId,
                TempRoute = request.TempRoute,
                SubmissionInput = request.SubmissionInput,
                SubmissionOutput = recalculationResult
            };

            await _clientSessionHistoryRepository.Add(clientSessionHistory);
            await _clientSessionCacheRepository.Delete(clientSessionCache);
            return true;
        }

        return false;
    }
}