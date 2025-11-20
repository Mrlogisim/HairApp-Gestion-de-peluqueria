using HairApp.Data;
using HairApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HairApp.Services
{
    public class ServicioService
    {

        private readonly ApplicationDbContext _context;

        public ServicioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Servicio>> ObtenerTodosAsync()
        {
            return await _context.Servicios.ToListAsync();
        }

        public async Task<Servicio?> ObtenerPorIdAsync(int id)
        {
            return await _context.Servicios.FirstOrDefaultAsync(i => i.Id_servicio == id);
        }

        public async Task CrearAsync(Servicio servicio)
        {
            _context.Add(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Servicio servicio)
        {
            _context.Update(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Servicios.AnyAsync(i => i.Id_servicio == id);
        }
    }

}

