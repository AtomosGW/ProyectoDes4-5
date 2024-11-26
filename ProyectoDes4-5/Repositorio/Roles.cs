using System.Collections.Generic;

namespace ProyectoDes4_5.Repositorio
{
        public class Roles
        {
            public int Id { get; set; }
            public string Nombre { get; set; }

            public ICollection<Usuarios> Usuarios { get; set; }
        }
    }

