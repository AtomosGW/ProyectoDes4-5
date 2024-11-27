using System;
using System.Collections.Generic;

namespace ProyectoDes4_5.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Correo { get; set; }

    public string Password { get; set; } = null!;

    public string? Telefono { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Asignaciones> Asignaciones { get; set; } = new List<Asignaciones>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Role Role { get; set; } = null!;
}
