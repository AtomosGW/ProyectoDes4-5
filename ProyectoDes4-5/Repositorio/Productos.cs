using System;

namespace ProyectoDes4_5.Repositorio
{
    public class Productos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; } = true; // Por defecto, el producto está activo
    }

}
