using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.Repositorio;

namespace ProyectoDes4_5.BD
{
    public class DBContext : DbContext
    {
        // DbSet para cada entidad
        public required DbSet<Asignaciones> Asignaciones { get; set; }
        public required DbSet<Roles> Roles { get; set; }
        public required DbSet<Usuarios> Usuarios { get; set; }
        public required DbSet<Productos> Productos { get; set; }
        public required DbSet<Estado> Estado { get; set; }
        public required DbSet<Pedido> Pedido { get; set; }
        public required DbSet<PedidoProductos> PedidoProductos { get; set; }
        public required DbSet<MetodoPago> MetodoPago { get; set; }
        public required DbSet<Pago> Pago { get; set; }

        // Constructor que recibe DbContextOptionsdoned
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        // Configurar relaciones, restricciones y precisiones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para PedidoProductos
            modelBuilder.Entity<PedidoProductos>()
                .Property(pp => pp.Subtotal)
                .HasPrecision(10, 2); // Precisión para decimal

            modelBuilder.Entity<PedidoProductos>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidoProductos)
                .HasForeignKey(pp => pp.PedidoId)
                .OnDelete(DeleteBehavior.Cascade); // Eliminación en cascada

            modelBuilder.Entity<PedidoProductos>()
                .HasOne(pp => pp.Productos)
                .WithMany()
                .HasForeignKey(pp => pp.ProductoId)
                .OnDelete(DeleteBehavior.Restrict); // No eliminación en cascada

            // Configuración para Usuarios
            modelBuilder.Entity<Usuarios>()
                .HasIndex(u => u.Correo)
                .IsUnique(); // Índice único en correo

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Correo)
                .IsRequired(); // Correo es obligatorio

            // Configuración para Roles
            modelBuilder.Entity<Roles>()
                .Property(r => r.Nombre)
                .IsRequired(); // Nombre del rol es obligatorio

            // Configuración para Productos
            modelBuilder.Entity<Productos>()
                .Property(p => p.Precio)
                .HasPrecision(10, 2) // Precisión para decimal
                .IsRequired(); // Precio es obligatorio

            // Configuración para Pedido
            modelBuilder.Entity<Pedido>()
                .Property(p => p.Total)
                .HasPrecision(10, 2) // Precisión para decimal
                .IsRequired(); // Total es obligatorio

            // Configuración para Pago
            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .HasPrecision(10, 2) // Precisión para decimal
                .IsRequired(); // Monto es obligatorio

            // Configuración para Estado (opcional: excluir de migraciones si ya existe)
            modelBuilder.Entity<Estado>()
                .ToTable("Estado", t => t.ExcludeFromMigrations()); // No incluir en migraciones si ya existe

            // Agregar restricciones adicionales aquí si es necesario
            base.OnModelCreating(modelBuilder);
        }
    }
}
