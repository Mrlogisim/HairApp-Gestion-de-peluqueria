using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairApp.Models
{
    public class Peluquero
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string? Email { get; set; }

        [Display(Name = "Especialidad")]
        [StringLength(100)]
        public string? Especialidad { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        // Relación muchos a muchos con Servicio
        public ICollection<PeluqueroServicio> PeluqueroServicios { get; set; }

        // Relación muchos a muchos con TurnoDetalles
        public ICollection<TurnoDetalles> TurnoDetalles { get; set; }
    }
}
