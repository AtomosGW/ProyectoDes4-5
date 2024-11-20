using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.Models;

namespace ProyectoDes4_5.BD
{
    public class ConexionDB : DbContext
    {
        // DbSet para las entidades
        public DbSet<Asignaciones> Asignaciones { get; set; }

        // Constructor que recibe DbContextOptions
        public ConexionDB(DbContextOptions<ConexionDB> options)
            : base(options)  // Llamada al constructor base de DbContext
        {
        }

        // Aquí puedes configurar la base de datos si lo necesitas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales, si es necesario
        }
    }
}
