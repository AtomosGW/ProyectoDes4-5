

namespace ProyectoDes4_5.Repositorio

{
    public class MetodoPago
    {
        public int Id { get; set; }
        public string Metodo { get; set; }

        // Relación con Pagos
        public ICollection<Pago> Pagos { get; set; }
    }
}
