using HairApp.Data;
using HairApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HairApp.Services
{
    public class InsumoService
    {
        private readonly ApplicationDbContext _context;

        public InsumoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Insumo>> ObtenerTodosAsync()
        {
            return await _context.Insumos.ToListAsync();
        }

        public async Task<Insumo?> ObtenerPorIdAsync(int id)
        {
            return await _context.Insumos.FirstOrDefaultAsync(i => i.IdInsumo == id);
        }

        public async Task CrearAsync(Insumo insumo)
        {
            _context.Add(insumo);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Insumo insumo)
        {
            _context.Update(insumo);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo != null)
            {
                _context.Insumos.Remove(insumo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Insumos.AnyAsync(i => i.IdInsumo == id);
        }
    }
}
