using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.Repositorio;
using System.Data;

namespace ProyectoDes4_5.BD
{
    public class ConexionDB : DbContext
    {
        // DbSet para cada entidad
        public DbSet<Asignaciones> Asignaciones { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoProductos> PedidoProductos { get; set; }
        public DbSet<MetodoPago> MetodoPago { get; set; }
        public DbSet<Pago> Pago { get; set; }

        // Constructor que recibe DbContextOptions
        public ConexionDB(DbContextOptions<ConexionDB> options)
            : base(options)
        {
        }

        // Configurar relaciones y restricciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla PedidoProducto
            modelBuilder.Entity<PedidoProductos>()
                .Property(pp => pp.Subtotal)
                .HasPrecision(10, 2); // Configuración de precisión para el decimal

            modelBuilder.Entity<PedidoProductos>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidoProductos)
                .HasForeignKey(pp => pp.PedidoId);

            modelBuilder.Entity<PedidoProductos>()
                .HasOne(pp => pp.Productos)
                .WithMany()
                .HasForeignKey(pp => pp.ProductoId);

            // Configuración para otras tablas
            modelBuilder.Entity<Usuarios>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<Roles>()
                .Property(r => r.Nombre)
                .IsRequired();

            modelBuilder.Entity<Productos>()
                .Property(p => p.Precio)
                .HasPrecision(10, 2);

            
        }
    }
}
