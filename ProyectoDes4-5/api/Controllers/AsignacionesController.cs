using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.Repositorio;
using ProyectoDes4_5.Services;
using ProyectoDes4_5.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoDes4_5.api.Controllers { 

    [ApiController]
    [Route("api/[controller]")]
    public class AsignacionesController : Controller
    {
        private readonly BaseService<Asignaciones> _service;

        public AsignacionesController(BaseService<Asignaciones> service)
        {
            _service = service;
        }

        // Acción API para obtener todas las asignaciones
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result); // Respuesta estándar con código 200
        }

        // Acción API para obtener asignación por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Acción API para crear una nueva asignación
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Asignaciones entity)
        {
            if (entity == null) return BadRequest("Datos inválidos.");
            await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        // Acción API para actualizar una asignación
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Asignaciones entity)
        {
            if (entity == null) return BadRequest("Datos inválidos.");
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.UpdateAsync(entity);
            return NoContent();
        }

        // Acción API para eliminar una asignación
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var asignaciones = await _service.GetAllAsync(query => query
                .Include(a => a.Pedido)  // Incluye el Pedido relacionado
                .ThenInclude(p => p.Cliente)  // Incluye el Cliente relacionado con Pedido
            );

            return View(asignaciones);  // Pasa el modelo con los datos relacionados a la vista
        }



    }
}
