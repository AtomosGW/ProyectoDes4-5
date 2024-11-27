using System;
using System.Collections.Generic;

namespace ProyectoDes4_5.Models;

public partial class Asignaciones
{
    public int Id { get; set; }

    public int PedidoId { get; set; }

    public int EmpleadoId { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public virtual Usuario Empleado { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;
}
