using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models.signin{
    public class Register{
        [Required(ErrorMessage = "Nombres son requeridos.")]
        public string Names { get; set; } = null!;
        public string? Lastnames { get; set; }
        [Required(ErrorMessage = "Email es requerido."),EmailAddress]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Contraseña es requerido.")]
        public string Password { get; set;} = null!;
        [Required(ErrorMessage = "Fecha de nacimiento es requerido."),DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}