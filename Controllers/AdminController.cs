using biblioteca.Models;
using biblioteca.Models.signin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace biblioteca.Controllers;
[Authorize(Roles = "Administrador")]
public class AdminController : Controller
{
    private readonly proyecto_cafeContext _context;

    public AdminController(proyecto_cafeContext context){
        _context = context;
    }

    public async Task<IActionResult> Index(string search){
        if(!String.IsNullOrEmpty(search)){
            var searchBook = await _context.Books.Where( sb => sb.Title.Contains(search)).ToListAsync();
            return View(searchBook);
        }
        var Books = await _context.Books.ToListAsync();

        return View(Books);                   
    }

    public async Task<IActionResult> Users(string search){
        if(!String.IsNullOrEmpty(search)){
            var searchUser = await _context.Users.Where( u => u.Names.Contains(search) || 
            u.Lastnames.Contains(search) || u.Email.Contains(search) || u.Rol.Contains(search)).ToListAsync();

            return View(searchUser);
        }
        var Users = await _context.Users.ToListAsync(); 
        return View(Users);
    }

    public async Task<IActionResult> Books(string search){
        if(!String.IsNullOrEmpty(search)){
            var searchBook = await _context.Books.Where( sb => sb.Title.Contains(search)).ToListAsync();
            return View(searchBook);
        }
        var Books = await _context.Books.ToListAsync();

        return View(Books);
    } 
}