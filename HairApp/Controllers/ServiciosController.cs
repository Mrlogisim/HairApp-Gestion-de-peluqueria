using HairApp.Models;
using HairApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HairApp.Controllers
{
    [Authorize] // Requiere login para acceder y cargar las vistas
    public class ServiciosController : Controller
    {
        private readonly ServicioService _servicioService;

        public ServiciosController(ServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        // GET: Servicios
        public async Task<IActionResult> Index(string search)
        {
            try
            {
                var servicios = await _servicioService.ObtenerTodosAsync();

                // Aplicar búsqueda si existe
                if (!string.IsNullOrEmpty(search))
                {
                    servicios = servicios.Where(s =>
                        s.Nombre.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        (s.Descripcion != null && s.Descripcion.Contains(search, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                return View(servicios);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los servicios: " + ex.Message;
                return View(new List<Servicio>());
            }
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID de servicio no especificado";
                return RedirectToAction(nameof(Index));
            }

            var servicio = await _servicioService.ObtenerPorIdAsync(id.Value);
            if (servicio == null)
            {
                TempData["Error"] = "Servicio no encontrado";
                return RedirectToAction(nameof(Index));
            }

            return View(servicio);
        }

        // POST: Servicios/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Servicio servicio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicioService.CrearAsync(servicio);
                    TempData["Success"] = "Servicio creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }

                // Si el modelo no es válido, regresar a la vista con errores
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Console.WriteLine("--- FALLÓ LA VALIDACIÓN DEL MODELO ---");
                TempData["Error"] = "Por favor, complete todos los campos requeridos correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al crear el servicio: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Servicios/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int Id_servicio, Servicio servicio)
        {
            try
            {
                if (Id_servicio != servicio.Id_servicio)
                {
                    TempData["Error"] = "ID de servicio no coincide";
                    return RedirectToAction(nameof(Index));
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _servicioService.ActualizarAsync(servicio);
                        TempData["Success"] = "Servicio actualizado exitosamente";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _servicioService.ExisteAsync(servicio.Id_servicio))
                        {
                            TempData["Error"] = "El servicio ya no existe";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }

                TempData["Error"] = "Por favor, complete todos los campos requeridos correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar el servicio: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Servicios/Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var servicio = await _servicioService.ObtenerPorIdAsync(id);
                if (servicio == null)
                {
                    TempData["Error"] = "Servicio no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                await _servicioService.EliminarAsync(id);
                TempData["Success"] = "Servicio eliminado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar el servicio: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Servicios/VerificarNombre
        public async Task<JsonResult> VerificarNombre(string nombre, int id = 0)
        {
            try
            {
                var servicios = await _servicioService.ObtenerTodosAsync();
                var existe = servicios.Any(s =>
                    s.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                    s.Id_servicio != id
                );

                return Json(new { existe = existe });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}