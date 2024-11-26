using Microsoft.AspNetCore.Mvc;
using ProyectoDes4_5.Services;
using ProyectoDes4_5.Repositorio;
using System.Threading.Tasks;

namespace ProyectoDes4_5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoProductoController : ControllerBase
    {
        private readonly PedidoProductoService _service;

        public PedidoProductoController(PedidoProductoService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProductoToPedido(int pedidoId, int productoId, int cantidad)
        {
            var result = await _service.AddProductoToPedidoAsync(pedidoId, productoId, cantidad);
            if (result == null) return BadRequest("Producto o pedido no encontrado.");
            return CreatedAtAction(nameof(AddProductoToPedido), result);
        }
    }
}
