using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using biblioteca.Models;
using biblioteca.Models.signin;
using biblioteca.helpers;

namespace biblioteca.Controllers;

public class SigninController : Controller
{
    public readonly proyecto_cafeContext _context;
    private readonly ILogger<SigninController> _logger;

    public SigninController(proyecto_cafeContext context,ILogger<SigninController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [Route("register")]
    public IActionResult Register()
    {
        ViewBag.Message = "";
        return View();
    }

    [HttpPost]
    [Route("register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Register user){
        try{
            if(ModelState.IsValid){
                var registeredUser = _context.Users.Any(u => u.Email == user.Email);

                if(registeredUser){
                    ViewBag.Message = "Email ya registrado. Inicie sesión o regístrese con otra cuenta.";
                    return View();
                }

                Hash.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt );

                User newUser = new User
                {
                    Names = user.Names,
                    Lastnames = user.Lastnames,
                    Email = user.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    BirthDate = user.BirthDate,
                    Rol = "Cliente",
                    Status = 1,
                    CreatedAt = DateTime.Now
                };

                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Library");
            }
            return View();
        }
        catch(Exception ex){
            ViewBag.Message = $"Error: {ex.Message}";
            return View();
        }
        
    }

    [Route("login")]
    public IActionResult Login(){
        return View();
    }
}
