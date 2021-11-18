using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Data
    {
        public int id { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string edad { get; set; }
        [Required]
        public string telefono { get; set; }
        
        public int id_usuario { get; set; }

    }
}
