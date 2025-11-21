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

        // MÉTODO NUEVO: Paginación eficiente
        public async Task<(List<Servicio> Servicios, int TotalCount, int TotalPages)> ObtenerPaginadosAsync(
            int pagina = 1,
            int cantidadPorPagina = 10,
            string search = null)
        {
            // Validar parámetros
            if (pagina < 1) pagina = 1;
            if (cantidadPorPagina < 1) cantidadPorPagina = 10;

            // Construir query base
            var query = _context.Servicios.AsQueryable();

            // Aplicar búsqueda si existe
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s =>
                    s.Nombre.Contains(search) ||
                    (s.Descripcion != null && s.Descripcion.Contains(search))
                );
            }

            // Obtener conteo total (para calcular páginas)
            var totalCount = await query.CountAsync();

            // Calcular total de páginas
            var totalPages = (int)Math.Ceiling(totalCount / (double)cantidadPorPagina);

            // Aplicar paginación (EFICIENTE - se ejecuta en SQL)
            var servicios = await query
                .OrderBy(s => s.Nombre)
                .Skip((pagina - 1) * cantidadPorPagina)
                .Take(cantidadPorPagina)
                .ToListAsync();

            return (servicios, totalCount, totalPages);
        }

        // Métodos existentes (mantenidos)
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

        // MÉTODO NUEVO: Para validación de nombre único
        public async Task<bool> ExisteNombreAsync(string nombre, int idExcluir = 0)
        {
            return await _context.Servicios
                .AnyAsync(s =>
                    s.Nombre.ToLower() == nombre.ToLower() &&
                    s.Id_servicio != idExcluir);
        }
    }
}