using System;

namespace ProyectoDes4_5.Repositorio
{
    public class Asignaciones
    {
            public int Id { get; set; }
            public int PedidoId { get; set; }
            public Pedido Pedido { get; set; }
            public int EmpleadoId { get; set; }
            public Usuarios Empleado { get; set; }
    }
}