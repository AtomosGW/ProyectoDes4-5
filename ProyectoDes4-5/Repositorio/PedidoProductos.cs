namespace ProyectoDes4_5.Repositorio
{
    public class PedidoProductos
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int ProductoId { get; set; }
        public Productos Productos { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }

}
