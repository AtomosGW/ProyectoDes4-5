using System;
using System.Collections.Generic;

namespace ProyectoDes4_5.Models;

public partial class MetodoPago
{
    public int Id { get; set; }

    public string Metodo { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
