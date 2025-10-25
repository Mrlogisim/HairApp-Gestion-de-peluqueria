using Microsoft.EntityFrameworkCore;
using HairApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HairApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Peluquero> Peluqueros { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<PeluqueroServicio> PeluqueroServicios { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Rol_Permiso> RolPermisos { get; set; }

        // Método para configurar el modelo de datos y las relaciones entre
        // las entidades de Entity Framework Core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación muchos a muchos Peluquero-Servicio
            modelBuilder.Entity<PeluqueroServicio>()
                .HasKey(ps => new { ps.PeluqueroId, ps.ServicioId });

            modelBuilder.Entity<PeluqueroServicio>() // Peluquero
                .HasOne(ps => ps.Peluquero)
                .WithMany(p => p.PeluqueroServicios)
                .HasForeignKey(ps => ps.PeluqueroId);

            modelBuilder.Entity<PeluqueroServicio>() // Servicio
                .HasOne(ps => ps.Servicio)
                .WithMany(s => s.PeluqueroServicios)
                .HasForeignKey(ps => ps.ServicioId);



            // Configuración de la relación Rol-Permiso
            modelBuilder.Entity<Rol_Permiso>()
                .HasKey(rp => new { rp.Id_Rol, rp.Id_Permiso });

            modelBuilder.Entity<Rol_Permiso>() // Rol
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.Rol_permisos)
                .HasForeignKey(rp => rp.Id_Rol);

            modelBuilder.Entity<Rol_Permiso>() // Permiso
                .HasOne(rp => rp.Permiso)
                .WithMany(p => p.Rol_permisos)
                .HasForeignKey(rp => rp.Id_Permiso);



            // Configuración de la relación uno a uno Usuario-Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithOne(r => r.Usuario)
                .HasForeignKey<Usuario>(u => u.IdRol);



            // Configuración de la relación muchos a muchos Turno-Clientes
            modelBuilder.Entity<TurnoClientes>()
                .HasKey(tc => new { tc.Id_cliente, tc.Id_turno}); // Clave compuesta

            modelBuilder.Entity<TurnoClientes>() // Cliente
                .HasOne(tc => tc.Cliente)
                .WithMany(c => c.TurnoClientes)
                .HasForeignKey(tc => tc.Id_cliente);

            modelBuilder.Entity<TurnoClientes>() // Turno
                .HasOne(tc => tc.Turno)
                .WithMany(t => t.TurnoClientes)
                .HasForeignKey(tc => tc.Id_turno);



            // Configuración de la relación muchos a muchos Servicio-insumo
            modelBuilder.Entity<ServicioInsumo>()
                .HasKey(tc => new { tc.Id_Servicio, tc.Id_Insumo }); // Clave compuesta

            modelBuilder.Entity<ServicioInsumo>() // Servicio
                .HasOne(tc => tc.Servicio)
                .WithMany(c => c.ServicioInsumo)
                .HasForeignKey(tc => tc.Id_Servicio);

            modelBuilder.Entity<ServicioInsumo>() // Insumo
                .HasOne(tc => tc.Insumo)
                .WithMany(t => t.ServicioInsumo)
                .HasForeignKey(tc => tc.Id_Insumo);



            // Configuración de la clave compuesta para Turno-Detalles
            modelBuilder.Entity<TurnoDetalles>()
                .HasKey(td => new { td.Id_Turno, td.Id_Cliente, td.Id_Peluquero });

            modelBuilder.Entity<TurnoDetalles>() // Turno
                .HasOne(td => td.Turno)
                .WithMany(t => t.Detalles)
                .HasForeignKey(td => td.Id_Turno);

            modelBuilder.Entity<TurnoDetalles>() // Cliente
                .HasOne(td => td.Cliente)
                .WithMany(c => c.TurnoDetalles)
                .HasForeignKey(td => td.Id_Cliente);

            modelBuilder.Entity<TurnoDetalles>() // Peluquero
                .HasOne(td => td.Peluquero)
                .WithMany(p => p.TurnoDetalles)
                .HasForeignKey(td => td.Id_Peluquero);
        }
    }
}
