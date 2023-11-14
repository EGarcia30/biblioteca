using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using biblioteca.Models;
using biblioteca.Models.signin;
using biblioteca.helpers;
using Microsoft.EntityFrameworkCore;

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
                    Username = user.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    BirthDate = user.BirthDate,
                    Rol = "Cliente",
                    Status = 1,
                    CreatedAt = DateTime.Now
                };

                _context.Add(newUser);
                await _context.SaveChangesAsync();

                var userClaim = _context.Users.FirstOrDefault(uc => uc.Email.Equals(newUser.Email));
                Auth.CreateCookie(HttpContext,userClaim!);                
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
        ViewBag.Message = "";
        return View();
    }

    [HttpPost]
    [Route("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Login login){
        try{
            if(ModelState.IsValid){
                var verifyUser = _context.Users.Any( vu => vu.Email.Equals(login.Email));
                if(!verifyUser){
                    ViewBag.Message = "Usuario no encontrado.";
                    return View();
                }

                var userBd = await _context.Users.FirstOrDefaultAsync(ub => ub.Email.Equals(login.Email));
                if(!Hash.VerifyPasswordHash(login.Password, userBd!.PasswordHash!, userBd.PasswordSalt!)){
                    ViewBag.Message = "Constraseña incorrecta.";
                    return View();
                }

                Auth.CreateCookie(HttpContext,userBd);
                switch(userBd.Rol){
                    case "Administrador":
                        return RedirectToAction("Index","Admin");
                    case "Cliente":
                        return RedirectToAction("Index","Library");
                    default:
                        return View();
                }
            }
            return View();
        }
        catch(Exception ex){
            ViewBag.Message = $"Error: {ex.Message}";
            return View();
        }
    }

    public IActionResult Logout(){
        Auth.DeleteCookie(HttpContext);
        return RedirectToAction("Index", "Home");
    }
}
