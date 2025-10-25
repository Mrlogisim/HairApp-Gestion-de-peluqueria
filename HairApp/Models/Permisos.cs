using System.ComponentModel.DataAnnotations;

namespace HairApp.Models
{
    public class Permisos
    {
        [Key]
        public int Id_permiso { get; set; }

        [MaxLength(100)]
        public string nombre { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        // Relación con rol
        public ICollection<Rol_Permiso> Rol_permisos { get; set; }
    }
}
