using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using biblioteca.Models;
using System.Security.Claims;

namespace biblioteca.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if(HttpContext.User.Identity.IsAuthenticated){
            var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            switch(role){
                case "Cliente":
                    return RedirectToAction("Index","Library");
                default:
                    return View();
            }
        }
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
