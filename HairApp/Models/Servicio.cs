using System.ComponentModel.DataAnnotations;

namespace HairApp.Models
{
    public class Servicio
    {
        [Key]
        public int Id_servicio { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La duración debe ser al menos 1 minuto.")]
        public int Duracion { get; set; }

        // Relación muchos a muchos con Peluquero
        public ICollection<PeluqueroServicio> PeluqueroServicios { get; set; }

        // Relación muchos a muchos con Insumo
        public ICollection<ServicioInsumo> ServicioInsumo { get; set; }
    }
}
