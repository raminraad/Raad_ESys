using ESys.Application.Contracts.Persistence;
using ESys.Application.Exceptions;
using ESys.Domain.Entities;

namespace ESys.Application.Features.BusinessForm;

public class BusinessFormInitiator
{
    private readonly IBusinessInitialUiRepository _businessInitialUiRepository;

    public BusinessFormInitiator(IBusinessInitialUiRepository businessInitialUiRepository)
    {
        _businessInitialUiRepository = businessInitialUiRepository;
    }

    /// <summary>
    /// Gets Business Form initial UI from database and returns it for initial rendering
    /// </summary>
    /// <param name="BusinessId">Business Id which corresponding UI must be returned</param>
    /// <returns>A Json string containing all the data needed for Business Form initialization</returns>
    /// <exception cref="NotFoundException">Occurs when there is no UI for received Business Id in database</exception>
    public async Task<string> GetInitialBusinessForm(string BusinessId)
    {
            var initialBusinessUI = await _businessInitialUiRepository.GetByIdAsync(BusinessId);
            if (initialBusinessUI is null)
                throw new NotFoundException(nameof(BusinessInitialUI), BusinessId);
            return initialBusinessUI.UiJson;
    }
}