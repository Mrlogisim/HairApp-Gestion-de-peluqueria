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


    }
}
