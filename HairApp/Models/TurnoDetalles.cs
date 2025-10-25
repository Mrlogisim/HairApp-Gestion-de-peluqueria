using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairApp.Models
{
    public class TurnoDetalles
    {

        [Key, Column(Order = 0)]
        public int Id_Turno { get; set; }

        [Key, Column(Order = 1)]
        public int Id_Cliente { get; set; }

        [Key, Column(Order = 2)]
        public int Id_Peluquero { get; set; }



        [ForeignKey("Id_Turno")]
        public Turno Turno { get; set; }

        [ForeignKey("Id_Cliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("Id_Peluquero")]
        public Peluquero Peluquero { get; set; }



        // Otras propiedades de TurnoDetalles
        // public string Detalle { get; set; }
    }
}
