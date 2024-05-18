using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalRLearn.Controllers;

[Authorize]
public class SupportController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}