using System;
using System.Collections.Generic;

namespace ProyectoDes4_5.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int PedidoId { get; set; }

    public int MetodoId { get; set; }

    public decimal Monto { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual MetodoPago Metodo { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;
}
