using HairApp.Data;
using HairApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HairApp.Services
{
    public class ClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los clientes
        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        // Obtener un cliente por ID
        public async Task<Cliente?> ObtenerPorIdAsync(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Crear un nuevo cliente
        public async Task CrearAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        // Actualizar cliente existente
        public async Task ActualizarAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        // Eliminar un cliente
        public async Task EliminarAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        // Verificar existencia de cliente
        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Clientes.AnyAsync(e => e.Id == id);
        }
    }
}
