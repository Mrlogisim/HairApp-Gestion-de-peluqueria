using System.ComponentModel.DataAnnotations;

namespace HairApp.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }

        [Phone]
        public string Telefono { get; set; }

        public string? Email { get; set; }
    }
}
