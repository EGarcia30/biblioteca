using biblioteca.Models;
using biblioteca.Models.product;
using Microsoft.AspNetCore.Mvc;

namespace biblioteca.Controllers;

public class BooksController : Controller
{
    private readonly IWebHostEnvironment _enviroment;
    private readonly proyecto_cafeContext _context;
    public BooksController(IWebHostEnvironment enviroment, proyecto_cafeContext context)
    {
        _enviroment = enviroment;
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
    public async Task<IActionResult> Create(BookRegister newBook)
    {
        try
        {
            if(ModelState.IsValid)
            {
                //guardar archivo de imagen en nuestra pagina
                var fileNameImgAbs = Path.Combine(_enviroment.ContentRootPath,"wwwroot/archive/img",newBook.UrlImagen.FileName);
                var fileNameImgRel = Path.Combine("/archive/img/",newBook.UrlImagen.FileName);
                await newBook.UrlImagen.CopyToAsync( new FileStream(fileNameImgAbs, FileMode.Create));

                //guardar nuestro archivo pdf en nuestra pagina
                var fileNamePdfAbs = Path.Combine(_enviroment.ContentRootPath,"wwwroot/archive/pdf",newBook.UrlPdf.FileName);
                var fileNamePdfRel = Path.Combine("/archive/pdf/",newBook.UrlPdf.FileName);
                await newBook.UrlPdf.CopyToAsync( new FileStream(fileNamePdfAbs, FileMode.Create));

                var bookBD = new Book
                {
                    UrlImagen = fileNameImgRel,
                    Title = newBook.Title,
                    Description = newBook.Description,
                    Status = 1,
                    UrlPdf = fileNamePdfRel,
                    CreatedAt = DateTime.Now
                };

                _context.Add(bookBD);
                await _context.SaveChangesAsync();
                ViewBag.Color = "bg-green-600 text-green-700";
                ViewBag.Message = "Libro registrado.";            

                return View();
            }
            return View();
        }
        catch(Exception ex)
        {
            ViewBag.Color = "bg-red-600 text-red-700";
            ViewBag.Message = $"Error {ex.Message}";
            return View();
        }
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var bookBD = await _context.Books.FindAsync(id);
        if(bookBD == null){
            return NotFound();
        }
        return View(bookBD);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Book bookBD, IFormFile newImagen, IFormFile newPdf)
    {
        try
        {
            if(ModelState.IsValid)
            {
                var newBookImg = newImagen?.FileName;
                var newBookPdf = newPdf?.FileName;
                var bookImg = bookBD?.UrlImagen;
                var bookPdf = bookBD?.UrlPdf;

                if(newBookImg != null)
                {
                    //eliminar archivo de imagen en nuestra carpeta
                    var fileNameImgAbsExists = $"{_enviroment.ContentRootPath}wwwroot{bookImg}";
                    System.IO.File.Delete(fileNameImgAbsExists);

                    //agregar nueva imagen a nuestra pag
                    var fnImgAbs = Path.Combine(_enviroment.ContentRootPath,"wwwroot/archive/img",newBookImg);
                    var fnImgRel = Path.Combine("/archive/img", newBookImg);
                    await newImagen.CopyToAsync(new FileStream(fnImgAbs, FileMode.Create));

                    //nuevo path relativo Imagen
                    bookBD.UrlImagen = fnImgRel;
                }

                if(newBookPdf != null)
                {
                    //eliminar archivo de pdf en nuestra carpeta
                    var fileNamePdfAbsExists = $"{_enviroment.ContentRootPath}wwwroot{bookPdf}";
                    System.IO.File.Delete(fileNamePdfAbsExists);

                    //agregar nueva imagen a nuestra pag
                    var fnPdfAbs = Path.Combine(_enviroment.ContentRootPath,"wwwroot/archive/pdf",newBookPdf);
                    var fnPdfRel = Path.Combine("/archive/pdf", newBookPdf);
                    await newPdf.CopyToAsync(new FileStream(fnPdfAbs, FileMode.Create));

                    //nuevo path relativo del pdf
                    bookBD.UrlPdf = fnPdfRel;
                }
                bookBD.UpdatedAt = DateTime.Now;
                _context.Update(bookBD);
                await _context.SaveChangesAsync();
                ViewBag.Color = "bg-green-600 text-green-700";
                ViewBag.Message = "Libro actualizado.";  

                return View(bookBD);
            }
            return View(bookBD);
        }
        catch(Exception ex)
        {
            ViewBag.Color = "bg-red-600 text-red-700";
            ViewBag.Message = $"Error {ex.Message}";
            return View(bookBD);
        }
    }

    public async Task<IActionResult> Delete(int? id)
    {
        try
        {
            var bookBD = await _context.Books.FindAsync(id);
            if(bookBD == null)
            {
                return NotFound();
            }

            //eliminar archivo de imagen en nuestra carpeta
            var fileNameImgAbsExists = $"{_enviroment.ContentRootPath}wwwroot{bookBD.UrlImagen}";
            System.IO.File.Delete(fileNameImgAbsExists);
            //eliminar archivo de pdf en nuestra carpeta
            var fileNamePdfAbsExists = $"{_enviroment.ContentRootPath}wwwroot{bookBD.UrlPdf}";
            System.IO.File.Delete(fileNamePdfAbsExists);

            _context.Books.Remove(bookBD);
            await _context.SaveChangesAsync();
            return RedirectToAction("Books", "Admin");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return RedirectToAction("Books","Admin");
        }
    }

    public IActionResult Download(string uri)
    {
        string url = Path.Combine(_enviroment.ContentRootPath,"wwwroot",uri);

        // Tipo MIME del archivo PDF
        string tipoMIME = "application/pdf";

        string nombrePdf = Path.GetFileName(uri);

        return File(url, tipoMIME, nombrePdf);
    }
}