using ESys.API.Attributes;
using ESys.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ESys.MVC.Controllers;

public class BusinessFormController : Controller
{
    [Route("businessform/{clientSession}")]
    public IActionResult Business([FromRoute]string clientSession)
    {
        var opensessions = JsonHandler.SimpleRead<List<ClientSession>>("D:\\f.j");
        try
        {
            var clientSessionAsGuid = Guid.Parse(clientSession);
            if (opensessions.Select(o=>o.SessionId).Contains(clientSessionAsGuid))
            {
                ViewBag.SessionId = clientSession;
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
}