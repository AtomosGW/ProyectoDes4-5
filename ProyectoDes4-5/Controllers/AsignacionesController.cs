using Microsoft.AspNetCore.Mvc;
using ProyectoDes4_5.Interfaz;
using ProyectoDes4_5.Models;
using ProyectoDes4_5.Repositorio;
using System.Linq;

namespace ProyectoDes4_5.Controllers
{
    public class AsignacionesController : Controller
    {
        private readonly IAsignacionesRepository _asignacionesRepository;

        public AsignacionesController(IAsignacionesRepository asignacionesRepository)
        {
            _asignacionesRepository = asignacionesRepository;
        }

        // Mostrar todas las asignaciones
        public IActionResult Index()
        {
            var asignaciones = _asignacionesRepository.GetAllAsignaciones()
                .Select(a => new AsignacionesModels
                {
                    AssignmentId = a.AssignmentId,
                    TicketId = a.TicketId,
                    OperatorId = a.OperatorId,
                    AssignedAt = a.AssignedAt
                }).ToList();

            return View(asignaciones);
        }

        // Ver detalles de una asignación
        public IActionResult Details(int id)
        {
            var asignacion = _asignacionesRepository.GetAsignacionById(id);
            if (asignacion == null)
                return NotFound();

            var model = new AsignacionesModels
            {
                AssignmentId = asignacion.AssignmentId,
                TicketId = asignacion.TicketId,
                OperatorId = asignacion.OperatorId,
                AssignedAt = asignacion.AssignedAt
            };

            return View(model);
        }

        // Crear nueva asignación
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AsignacionesModels model)
        {
            if (ModelState.IsValid)
            {
                var asignacion = new Asignaciones
                {
                    TicketId = model.TicketId,
                    OperatorId = model.OperatorId,
                    AssignedAt = model.AssignedAt
                };

                _asignacionesRepository.InsertAsignacion(asignacion);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Editar asignación
        public IActionResult Edit(int id)
        {
            var asignacion = _asignacionesRepository.GetAsignacionById(id);
            if (asignacion == null)
                return NotFound();

            var model = new AsignacionesModels
            {
                AssignmentId = asignacion.AssignmentId,
                TicketId = asignacion.TicketId,
                OperatorId = asignacion.OperatorId,
                AssignedAt = asignacion.AssignedAt
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AsignacionesModels model)
        {
            if (id != model.AssignmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var asignacion = new Asignaciones
                {
                    AssignmentId = model.AssignmentId,
                    TicketId = model.TicketId,
                    OperatorId = model.OperatorId,
                    AssignedAt = model.AssignedAt
                };

                _asignacionesRepository.UpdateAsignacion(asignacion);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Eliminar asignación
        public IActionResult Delete(int id)
        {
            var asignacion = _asignacionesRepository.GetAsignacionById(id);
            if (asignacion == null)
                return NotFound();

            var model = new AsignacionesModels
            {
                AssignmentId = asignacion.AssignmentId,
                TicketId = asignacion.TicketId,
                OperatorId = asignacion.OperatorId,
                AssignedAt = asignacion.AssignedAt
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _asignacionesRepository.DeleteAsignacion(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
