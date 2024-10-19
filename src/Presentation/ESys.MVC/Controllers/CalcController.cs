using Microsoft.AspNetCore.Mvc;

namespace ESys.MVC.Controllers;

public class CalcController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}