using System.ComponentModel.DataAnnotations;

namespace HairApp.Models
{
    public class TurnoClientes
    {
        // Clave foránea a Cliente
        public int Id_cliente{ get; set; }
        public Cliente Cliente { get; set; }
             

        // Clave foránea a Turno
        public int Id_turno { get; set; }
        public Turno Turno { get; set; }
    }
}
