using biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace biblioteca.Controllers;
[Authorize]
public class LibraryController : Controller
{
    private readonly proyecto_cafeContext _context;

    public LibraryController(proyecto_cafeContext context){
        _context = context;
    }

    [Authorize(Roles = "Administrador,Cliente")]
    public async Task<IActionResult> Index(string search){
        if(!String.IsNullOrEmpty(search)){
            var searchBook = await _context.Books.Where( sb => sb.Title.Contains(search)).ToListAsync();
            return View(searchBook);
        }
        var Books = await _context.Books.ToListAsync();

        return View(Books); 
    }

    public IActionResult Menu(){
        return View();
    }

    public IActionResult SalonEvento(){
        return View();
    }
}
