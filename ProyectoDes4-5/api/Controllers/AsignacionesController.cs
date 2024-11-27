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

        // Acci�n API para obtener todas las asignaciones
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result); // Respuesta est�ndar con c�digo 200
        }

        // Acci�n API para obtener asignaci�n por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        // Acci�n API para actualizar una asignaci�n
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Asignaciones entity)
        {
            if (entity == null) return BadRequest("Datos inv�lidos.");
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.UpdateAsync(entity);
            return NoContent();
        }

        // Acci�n API para eliminar una asignaci�n
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] Asignaciones entity)
        {
            if (entity == null)
            {
                return BadRequest("Datos inv�lidos.");
            }

            // Log para depuraci�n
            Console.WriteLine($"EmpleadoId: {entity.EmpleadoId}, PedidoId: {entity.PedidoId}");

            try
            {
                await _service.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
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
