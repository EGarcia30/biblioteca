using biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace biblioteca.Controllers;
[Authorize]
public class LibraryController : Controller
{
    private readonly proyecto_cafeContext _context;

    public LibraryController(proyecto_cafeContext context){
        _context = context;
    }

    [Authorize(Roles = "Administrador,Cliente")]
    public IActionResult Index(){
        return View();
    }
}
