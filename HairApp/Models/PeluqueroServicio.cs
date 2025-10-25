using System.ComponentModel.DataAnnotations.Schema;

namespace HairApp.Models
{
    public class PeluqueroServicio
    {
        // Clave foránea a Peluquero
        public int PeluqueroId { get; set; }
        public Peluquero Peluquero { get; set; }

        // Clave foránea a Servicio
        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; }
    }
}
