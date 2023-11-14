using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models.signin
{
    public partial class User
    {
        public int UserId { get; set; }
        [Required]
        public string Names { get; set; }
        public string Lastnames { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        [Required,DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [Required]
        [RegularExpression("^(Administrador|Cliente)$", ErrorMessage = "El campo solo puede contener los valores 'Administrador' o 'Cliente'.")]
        public string Rol { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
