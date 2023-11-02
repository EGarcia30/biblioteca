using biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace biblioteca.Controllers;
public class LibraryController : Controller
{
    public readonly proyecto_cafeContext _context;

    public LibraryController(proyecto_cafeContext context){
        _context = context;
    }

    public IActionResult Index(){
        return View();
    }
}
