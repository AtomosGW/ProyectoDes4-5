
using System;
using System.Collections.Generic;

namespace ProyectoDes4_5.Repositorio
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Usuarios Cliente { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }

        // Relación con DetallePedido
        public ICollection<PedidoProductos> PedidoProductos { get; set; }
    }
}
