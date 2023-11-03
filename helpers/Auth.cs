using System.Security.Claims;
using biblioteca.Models.signin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace biblioteca.helpers;

public static class Auth{
    public async static void CreateCookie(HttpContext httpContext, User user){
        var claims = new List<Claim>
        {
            new Claim("UserId", user.UserId.ToString()!),
            new Claim(ClaimTypes.Name, user.Names),
            new Claim("Lastnames", user.Lastnames!),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("BirthDate", user.BirthDate.ToString()!),
            new Claim(ClaimTypes.Role, user.Rol!),
            new Claim("CreatedAt", user.CreatedAt.ToString()!)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
    }

    public async static void DeleteCookie(HttpContext httpContext){
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}