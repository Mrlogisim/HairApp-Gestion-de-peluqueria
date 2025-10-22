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

        public ICollection<TurnoDetalles> Detalles { get; set; }
        public ICollection<TurnoClientes> TurnoClientes { get; set; }
    }

}
