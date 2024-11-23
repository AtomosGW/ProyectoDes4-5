using Microsoft.AspNetCore.Mvc;
using ProyectoDes4_5.Interfaz;
using ProyectoDes4_5.Models;  
using ProyectoDes4_5.Repositorio; 

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
                .Select(a => new AsignacionesModel  // Usar alias AsignacionesModel para el modelo de vista
                {
                    AssignmentId = a.AssignmentId,
                    TicketId = a.TicketId,
                    OperatorId = a.OperatorId,
                    AssignedAt = a.AssignedAt
                }).ToList();

            return View(asignaciones);
        }

        // Ver detalles de una asignaci�n
        public IActionResult Details(int id)
        {
            var asignacion = _asignacionesRepository.GetAsignacionById(id);
            if (asignacion == null)
                return NotFound();

            var model = new AsignacionesModel  // Usar alias AsignacionesModel para el modelo de vista
            {
                AssignmentId = asignacion.AssignmentId,
                TicketId = asignacion.TicketId,
                OperatorId = asignacion.OperatorId,
                AssignedAt = asignacion.AssignedAt
            };

            return View(model);
        }

        // Crear nueva asignaci�n
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AsignacionesModel model)  // Usar alias AsignacionesModel para el modelo de vista
        {
            if (ModelState.IsValid)
            {
                var asignacion = new Asignaciones  // Usar alias Asignaciones para el modelo de repositorio
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

        // Editar asignaci�n
        public IActionResult Edit(int id)
        {
            var asignacion = _asignacionesRepository.GetAsignacionById(id);
            if (asignacion == null)
                return NotFound();

            var model = new AsignacionesModel  // Usar alias AsignacionesModel para el modelo de vista
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
        public IActionResult Edit(int id, AsignacionesModel model)  // Usar alias AsignacionesModel para el modelo de vista
        {
            if (id != model.AssignmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var asignacion = new Asignaciones  // Usar alias Asignaciones para el modelo de repositorio
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

        // Eliminar asignaci�n
        public IActionResult Delete(int id)
        {
            var asignacion = _asignacionesRepository.GetAsignacionById(id);
            if (asignacion == null)
                return NotFound();

            var model = new AsignacionesModel  // Usar alias AsignacionesModel para el modelo de vista
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
