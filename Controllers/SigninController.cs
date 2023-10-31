using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using biblioteca.Models;

namespace biblioteca.Controllers;

public class SigninController : Controller
{
    private readonly ILogger<SigninController> _logger;

    public SigninController(ILogger<SigninController> logger)
    {
        _logger = logger;
    }

    public IActionResult Registrar()
    {
        return View();
    }

    public IActionResult Iniciar(){
        return View();
    }
}
