using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models.signin{
    public class Login{
        [Required(ErrorMessage = "El campo de Email es requerido."),EmailAddress]
        public string Email {get; set;} = null!;

        [Required(ErrorMessage = "El campo de contraseña es requerido."), StringLength(20)]
        public string Password {get; set;} = null!;
    }
 }