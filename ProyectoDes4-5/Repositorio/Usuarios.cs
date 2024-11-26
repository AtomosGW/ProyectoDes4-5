
using System.Collections.Generic;

namespace ProyectoDes4_5.Repositorio

{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }

        // Foreign Key
        public int RoleId { get; set; }
        public Roles Rol { get; set; }

        // Relación con otras entidades
        public ICollection<Pedido> Pedidos { get; set; }
        public ICollection<Asignaciones> Asignaciones { get; set; }
    }
}
