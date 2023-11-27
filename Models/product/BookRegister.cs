namespace biblioteca.Models.product;

public class BookRegister
{
    public int BookId { get; set; }
    public IFormFile UrlImagen { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int? Status { get; set; }
    public IFormFile UrlPdf { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
}