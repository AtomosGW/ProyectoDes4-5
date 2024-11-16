using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoDes4_5.Models;

namespace ProyectoDes4_5.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AsignacionesController : ControllerBase
    {
        private readonly IAsignacionesRepository _repository;

        public AsignacionesController(IAsignacionesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAllAsignaciones());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var asignacion = _repository.GetAsignacionById(id);
            if (asignacion == null)
                return NotFound();

            return Ok(asignacion);
        }

        [HttpPost]
        public IActionResult Create(Asignaciones asignacion)
        {
            _repository.InsertAsignacion(asignacion);
            return CreatedAtAction(nameof(GetById), new { id = asignacion.AssignmentId }, asignacion);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Asignaciones asignacion)
        {
            if (id != asignacion.AssignmentId)
                return BadRequest();

            _repository.UpdateAsignacion(asignacion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteAsignacion(id);
            return NoContent();
        }
    }

}
