using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.Repositorio;

namespace ProyectoDes4_5.BD
{
    public class ConexionDB : DbContext
    {
        public ConexionDB(DbContextOptions<ConexionDB> options) : base(options) { }

        // DbSet de la entidad Asignaciones
        public DbSet<Asignaciones> Asignaciones { get; set; }

        // Configurar la clave primaria de Asignaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asignaciones>()
                .HasKey(a => a.AssignmentId);  // Asegura que 'AssignmentId' sea la clave primaria
        }
    }
}
