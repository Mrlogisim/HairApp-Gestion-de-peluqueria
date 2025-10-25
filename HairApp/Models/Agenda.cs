using System.ComponentModel.DataAnnotations;

namespace HairApp.Models
{
    public class Agenda
    {
        [Key]
        public int Id_Agenda { get; set; }
        public int Id_Peluquero { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora_Inicio { get; set; }
        public TimeSpan Hora_Fin { get; set; }

        public Peluquero Peluquero { get; set; } // propiedad de navegación, permite la relación entre Peluquero y Agenda
    }

}
