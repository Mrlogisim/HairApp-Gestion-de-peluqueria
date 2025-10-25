using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairApp.Models
{
    public class Cliente
    {
        [Key]
        [Column("Id_cliente")] // Mapea la propiedad a la columna "Id_cliente"
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }

        public string DNI { get; set; }

        [Phone]
        public string Telefono { get; set; }

        public string? Email { get; set; }

        // Relación muchos a muchos con Turno
        public ICollection<TurnoClientes> TurnoClientes { get; set; }

        // Relación muchos a muchos con TurnoDetalles
        public ICollection<TurnoDetalles> TurnoDetalles { get; set; }

    }
}
