using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HairApp.Models
{
    public class Rol : IdentityRole<int> // Usamos int como PK
    {
        [MaxLength(100)]
        public string Descripcion { get; set; }

        // Relación uno a uno con Usuario
        public Usuario Usuario { get; set; } // Propiedad de navegación

        // Relación con permisos
        public ICollection<Rol_Permiso> Rol_permisos { get; set; }
    }
}
