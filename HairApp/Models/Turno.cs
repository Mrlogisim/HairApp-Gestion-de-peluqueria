using System.ComponentModel.DataAnnotations;

namespace HairApp.Models
{
    public class Turno
    {
        [Key]
        public int Id_Turno { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora_Inicio { get; set; }
        public TimeSpan Hora_Fin { get; set; }
        public string Estado { get; set; }

        // Relación mucho a muchos con Peluquero
        public ICollection<TurnoDetalles> Detalles { get; set; }
        // Relación muchos a muchos con Cliente
        public ICollection<TurnoClientes> TurnoClientes { get; set; }
    }

}
