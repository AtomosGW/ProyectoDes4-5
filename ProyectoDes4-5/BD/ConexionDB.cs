using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.Repositorio;
using System.Collections.Generic;

namespace ProyectoDes4_5.BD
{
    public class ConexionDB(DbContextOptions<ConexionDB> options) : DbContext(options)
    {

        // DbSet de la entidad Asignaciones
        public DbSet<Asignaciones> Asignaciones { get; set; }
    }
}
