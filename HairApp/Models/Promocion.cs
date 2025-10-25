namespace HairApp.Models
{
    public class Promocion
    {
        // Propiedades de la tabla Promociones

        public int Id_Promocion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        // ... otras propiedades como tipo_descuento, valor, etc.

        // Propiedad de navegación Many-to-Many
        public ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
    }
}
