using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models.signin{
    public class Login{
        [Required(ErrorMessage = "El campo de Email es requerido."),EmailAddress]
        public string Email {get; set;} = null!;

        [Required(ErrorMessage = "El campo de contrasea es requerido."), StringLength(20)]
        public string password {get; set;} = null!;
    }
 }