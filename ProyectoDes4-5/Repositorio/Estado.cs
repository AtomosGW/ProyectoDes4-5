namespace ProyectoDes4_5.Repositorio
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Relación con Pedidos
        public ICollection<Pedido> Pedidos { get; set; }
    }
}
