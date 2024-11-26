using System;

namespace ProyectoDes4_5.Repositorio
{
    public class Pago
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int MetodoId { get; set; }
        public MetodoPago Metodo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
