using System;
using System.Collections.Generic;

namespace biblioteca.Models.product
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string UrlImagen { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public string UrlPdf { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
