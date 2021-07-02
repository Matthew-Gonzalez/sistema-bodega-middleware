using Microsoft.EntityFrameworkCore;

namespace sistema_bodega.Data
{
    /// <summary>
    /// Establece conexion con la base de datos
    /// </summary>
    public class BaseDatos : DbContext
    {
        public BaseDatos(DbContextOptions<BaseDatos> options) : base(options) { }

        /// <summary>
        /// Configura las relaciones n:n y n:n:n
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Para la tabla ProductosBodegas
            modelBuilder.Entity<ProductoBodega>().HasKey(pd => new { pd.ProductoId, pd.BodegaId });

            // Para la tabla ProductosBodegasEmpleados
            modelBuilder.Entity<ProductoBodegaEmpleado>().HasOne<Producto>(pbe => pbe.Producto)
                .WithMany(p => p.ProductoBodegaEmpleados)
                .HasForeignKey(pbe => pbe.ProductoId);

            modelBuilder.Entity<ProductoBodegaEmpleado>().HasOne<Bodega>(pbe => pbe.Bodega)
                .WithMany(b => b.ProductoBodegaEmpleados)
                .HasForeignKey(pbe => pbe.BodegaId);

            modelBuilder.Entity<ProductoBodegaEmpleado>().HasOne<Empleado>(pbe => pbe.Empleado)
                .WithMany(e => e.ProductoBodegaEmpleados)
                .HasForeignKey(pbe => pbe.EmpleadoId);
        }

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoBodega> ProductosBodegas { get; set; }
        public DbSet<ProductoBodegaEmpleado> ProductosBodegasEmpleados { get; set; }
    }
}