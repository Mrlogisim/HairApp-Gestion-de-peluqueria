using HairApp.Models;
using HairApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HairApp.Controllers
{
    [Authorize] // Requiere login para acceder y cargar las vistas 
    public class InsumosController : Controller
    {
        private readonly InsumoService _insumoService;

        // Inyección del servicio "InsumoService"•	Esto permite que el controlador acceda
        // a los métodos de InsumoService para realizar operaciones relacionadas con la entidad Insumo.
        public InsumosController(InsumoService insumoService)
        {
            _insumoService = insumoService;
        }

        // Muestra la vista principal
        public async Task<IActionResult> Index()
        {
            var insumos = await _insumoService.ObtenerTodosAsync();
            return View(insumos); // se cambio View(insumos) por View()
        }

        // Crea un nuevo insumo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Insumo insumo)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Verifique los datos ingresados.";
                return RedirectToAction("Index");
            }

            await _insumoService.CrearAsync(insumo);
            TempData["Success"] = "Insumo registrado correctamente.";
            return RedirectToAction("Index");
        }

        // Editar un insumo existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Insumo insumo)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Verifique los datos ingresados.";
                return RedirectToAction("Index");
            }

            await _insumoService.ActualizarAsync(insumo);
            TempData["Success"] = "Insumo actualizado correctamente.";
            return RedirectToAction("Index");
        }

        // Eliminar un insumo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _insumoService.EliminarAsync(id);
            TempData["Success"] = "Insumo eliminado correctamente.";
            return RedirectToAction("Index");
        }
    }
}

