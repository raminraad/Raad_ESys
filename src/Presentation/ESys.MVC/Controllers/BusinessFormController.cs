using ESys.API.Attributes;
using ESys.Application.Abstractions.Persistence;
using ESys.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ESys.MVC.Controllers;

public class BusinessFormController : Controller
{
    private readonly IClientSessionCacheRepository _clientSessionCacheRepository;

    public BusinessFormController(IClientSessionCacheRepository clientSessionCacheRepository)
    {
        _clientSessionCacheRepository = clientSessionCacheRepository;
    }
    
    [Route("epaas/businessform/client/{clientSession}")]
    public IActionResult Business([FromRoute]Guid clientSession)
    {
        try
        {
            if (_clientSessionCacheRepository.GetByTempRoute(clientSession) != null)
                return View();
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
}