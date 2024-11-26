using Microsoft.AspNetCore.Mvc;
using ProyectoDes4_5.Repositorio;
using ProyectoDes4_5.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoDes4_5.Controllers
{
    [ApiController]  // Indica que este es un controlador de API
    [Route("api/[controller]")]  // Ruta base para este controlador
    public class AsignacionesController : ControllerBase
    {
        private readonly BaseService<Asignaciones> _service;

        public AsignacionesController(BaseService<Asignaciones> service)
        {
            _service = service;
        }

        // GET api/asignaciones
        [HttpGet]
        public async Task<IEnumerable<Asignaciones>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        // GET api/asignaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignaciones>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;  // Esto retornará un JSON
        }

        // POST api/asignaciones
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Asignaciones entity)
        {
            if (entity == null)
                return BadRequest("El objeto Asignaciones no puede ser nulo.");

            await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);  // Responde con JSON
        }

        // PUT api/asignaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Asignaciones entity)
        {
            if (entity == null)
                return BadRequest("El objeto Asignaciones no puede ser nulo.");

            var existingEntity = await _service.GetByIdAsync(id);
            if (existingEntity == null) return NotFound();

            await _service.UpdateAsync(entity);
            return NoContent();  // Responde con HTTP 204
        }

        // DELETE api/asignaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();  // Responde con HTTP 204
        }
    }
}
