using ESys.API.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ESys.MVC.Controllers;

public class CalcController : Controller
{
    [NoDirectAccess]
    public IActionResult Index()
    {
        return View();
    }
}