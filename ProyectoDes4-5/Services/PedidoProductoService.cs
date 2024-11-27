using System.Threading.Tasks;
using ProyectoDes4_5.Models;
using ProyectoDes4_5.Repositorio;

namespace ProyectoDes4_5.Services
{
    public class PedidoProductoService : BaseService<PedidoProducto>
    {
        public PedidoProductoService(WebPedidosContext context) : base(context) { }

        public async Task<PedidoProducto> AddProductoToPedidoAsync(int pedidoId, int productoId, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null) return null;

            var pedidoProducto = new PedidoProducto
            {
                PedidoId = pedidoId,
                ProductoId = productoId,
                Cantidad = cantidad,
                Subtotal = producto.Precio * cantidad
            };

            await CreateAsync(pedidoProducto);

            return pedidoProducto;
        }
    }
}
