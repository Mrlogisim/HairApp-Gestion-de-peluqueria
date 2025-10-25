using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HairApp.Models
{
    public class Usuario : IdentityUser<int> // Usamos int como PK
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        public bool Estado { get; set; }

        // Relación uno a uno con Rol
        public int IdRol { get; set; } // Clave foránea
        public Rol Rol { get; set; } // Propiedad de navegación

        // Relaciones con otras entidades
        public ICollection<Cliente> Clientes { get; set; }
        public ICollection<Peluquero> Peluqueros { get; set; }
    }
}
