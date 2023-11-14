using System.Reflection.Metadata.Ecma335;
using biblioteca.helpers;
using biblioteca.Models;
using biblioteca.Models.signin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace biblioteca.Controllers;

[Authorize(Roles = "Administrador")]
public class UsersController : Controller
{
    public readonly proyecto_cafeContext _context;

    public UsersController(proyecto_cafeContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        ViewBag.Color = "";
        ViewBag.Message = "";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Register user){
        try{
            if(ModelState.IsValid){
                var registeredUser = _context.Users.Any(u => u.Email == user.Email);

                if(registeredUser){
                    ViewBag.Color = "bg-red-600 text-red-700";
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
                    Rol = "Administrador",
                    Status = 1,
                    CreatedAt = DateTime.Now
                };

                _context.Add(newUser);
                await _context.SaveChangesAsync();
                ViewBag.Color = "bg-green-600 text-green-700";
                ViewBag.Message = "Usuario registrado.";            
                return View();
            }
            return View();
        }
        catch(Exception ex){
            ViewBag.Color = "bg-red-600 text-red-700";
            ViewBag.Message = $"Error {ex.Message}";
            return View();
        }
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if(id == null){
            return NotFound();
        }

        var userBD = await _context.Users.FindAsync(id);
        if(userBD == null)
        {
            return NotFound();
        }
        return View(userBD);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int? id, User user)
    {
        try{

            if(ModelState.IsValid)
            {
                if( id != user.UserId)
                {
                    ViewBag.Color = "bg-red-600 text-red-700";
                    ViewBag.Message = "No se pudo encontrar el ID, intentar de nuevo.";
                    return View();
                }

                var userBD = _context.Users.Any(ub => ub.UserId == user.UserId);
                if(!userBD)
                {
                    ViewBag.Color = "bg-red-600 text-red-700";
                    ViewBag.Message = "No se pudo encontrar el usuario a actualizar.";
                    return View();
                }

                user.UpdatedAt = DateTime.Now;
                ViewBag.Color ="bg-green-400 text-green-800";
                ViewBag.Message = "Usuario Actualizado.";
                return View();
            }

        }
        catch(Exception ex)
        {
            ViewBag.Color = "bg-red-600 text-red-700";
            ViewBag.Message = $"Error: {ex.Message}";
            return View();
        }
        return View();
    }

    public async Task<IActionResult> Delete(int? id){
        var user = await _context.Users.FindAsync(id);
        if(user == null){
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToAction("Users", "Admin");
    }
}