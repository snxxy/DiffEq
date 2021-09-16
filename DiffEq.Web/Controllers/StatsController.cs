using Microsoft.AspNetCore.Mvc;

namespace DiffEq.Web.Controllers;
public class StatsController : Controller
{
    public IActionResult Stats()
    {
        return View();
    }
}
