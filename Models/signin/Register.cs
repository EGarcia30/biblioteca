using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models.signin{
    public class Register{
        public string Names { get; set; } = null!;
        public string? Lastnames { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set;} = null!;
        [Required,DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}