namespace HairApp.Models
{
    public class Rol_Permiso
    {
        public int Id_Rol { get; set; }
        public Rol Rol { get; set; }

        public int Id_Permiso { get; set; }
        public Permisos Permiso { get; set; }
    }
}

