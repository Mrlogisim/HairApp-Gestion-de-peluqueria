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
    [Authorize]
    public class ServiciosController : Controller
    {
        private readonly ServicioService _servicioService;

        public ServiciosController(ServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        // GET: Servicios (ACTUALIZADO con paginación)
        public async Task<IActionResult> Index(string search, int pagina = 1, int cantidadPorPagina = 10)
        {
            try
            {
                // Obtener datos paginados
                var (servicios, totalCount, totalPages) = await _servicioService.ObtenerPaginadosAsync(
                    pagina, cantidadPorPagina, search);

                // Pasar datos a la vista
                ViewBag.PaginaActual = pagina;
                ViewBag.TotalPaginas = totalPages;
                ViewBag.TotalRegistros = totalCount;
                ViewBag.CantidadPorPagina = cantidadPorPagina;
                ViewBag.Search = search;

                // Calcular rangos para mostrar
                ViewBag.RegistroInicio = totalCount == 0 ? 0 : ((pagina - 1) * cantidadPorPagina) + 1;
                ViewBag.RegistroFin = Math.Min(pagina * cantidadPorPagina, totalCount);

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
                    // Validar nombre único
                    if (await _servicioService.ExisteNombreAsync(servicio.Nombre))
                    {
                        TempData["Error"] = $"Ya existe un servicio con el nombre '{servicio.Nombre}'";
                        return RedirectToAction(nameof(Index));
                    }

                    await _servicioService.CrearAsync(servicio);
                    TempData["Success"] = "Servicio creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }

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
                        // Validar nombre único (excluyendo el actual)
                        if (await _servicioService.ExisteNombreAsync(servicio.Nombre, servicio.Id_servicio))
                        {
                            TempData["Error"] = $"Ya existe un servicio con el nombre '{servicio.Nombre}'";
                            return RedirectToAction(nameof(Index));
                        }

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

        // GET: Servicios/VerificarNombre (OPTIMIZADO)
        public async Task<JsonResult> VerificarNombre(string nombre, int id = 0)
        {
            try
            {
                var existe = await _servicioService.ExisteNombreAsync(nombre, id);
                return Json(new { existe = existe });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}