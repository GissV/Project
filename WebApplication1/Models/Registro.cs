
using WebApplication1.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Registro
    {
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string email { get; set; }

        [Required(ErrorMessage = "Password obligatorio")]
   
        public string contrasena { get; set; }

        
    }
}