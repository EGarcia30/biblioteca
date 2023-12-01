using biblioteca.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Conexion a Base de datos
// pro "bibliotecaDB": "Server=sql.bsite.net\\MSSQL2016;Database=vastyscoffee_;User Id=vastyscoffee;password=Vastyscoffee01;Trusted_Connection=True;"
// pruebas "bibliotecaDB": "Server=DESKTOP-RD5B5QL\\SQLEXPRESS;Database=proyecto_cafe;Trusted_Connection=True;"
string connectionName = "bibliotecaDB";
var connectionString = builder.Configuration.GetConnectionString(connectionName);

builder.Services.AddDbContext<proyecto_cafeContext>(options => options.UseSqlServer(connectionString));

//Cookie persistencia de usuario
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options => {
    options.LoginPath = "/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.AccessDeniedPath = "/Login";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
