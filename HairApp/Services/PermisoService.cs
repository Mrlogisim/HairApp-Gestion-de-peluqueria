using HairApp.Data;
using HairApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HairApp.Services
{
    public class PermisoService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<Rol> _roleManager;

        public PermisoService(ApplicationDbContext context, RoleManager<Rol> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // Sincroniza los permisos (claims) del sistema con los roles de Identity
        public async Task SincronizarPermisosAsync()
        {
            try
            {
                var roles = await _context.Roles
                    .Include(r => r.Rol_permisos)
                    .ThenInclude(rp => rp.Permiso)
                    .ToListAsync();

                foreach (var rol in roles)
                {
                    // Obtener los claims actuales del rol en Identity
                    var claimsExistentes = await _roleManager.GetClaimsAsync(rol);

                    // Crear una lista de claims nuevos basados en los permisos
                    var claimsNuevos = rol.Rol_permisos
                        .Select(rp => new Claim("Permiso", rp.Permiso.nombre))
                        .ToList();

                    // Eliminar claims que ya no están en los permisos
                    foreach (var claim in claimsExistentes)
                    {
                        if (!claimsNuevos.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                        {
                            await _roleManager.RemoveClaimAsync(rol, claim);
                        }
                    }

                    // Agregar claims nuevos que no están en los claims existentes
                    foreach (var claim in claimsNuevos)
                    {
                        if (!claimsExistentes.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                        {
                            await _roleManager.AddClaimAsync(rol, claim);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores: registrar el problema
                Console.WriteLine($"Error al sincronizar permisos: {ex.Message}");
                throw;
            }
        }
    }
}
