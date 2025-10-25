namespace HairApp.Models
{
    public class ServicioInsumo
    {
        public int Id_Servicio { get; set; }
        public Servicio Servicio { get; set; }

        public int Id_Insumo { get; set; }
        public Insumo Insumo { get; set; }
    }

}
