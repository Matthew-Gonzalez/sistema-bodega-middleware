using Microsoft.EntityFrameworkCore;

namespace sistema_bodega.Data
{
    /// <summary>
    /// Establece conexion con la base de datos
    /// </summary>
    public class BaseDatos : DbContext
    {
        /// <summary>
        /// Define la conexion a la base de datos
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SistemaBodega;Integrated Security=true");
        }

        /// <summary>
        /// Configura las relaciones n:n y n:n:n
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Para la tabla Producto_Bodega
            modelBuilder.Entity<ProductoBodega>().HasKey(pd => new { pd.ProductoId, pd.BodegaId });
        }

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoBodega> ProductosBodegas { get; set; }
    }
}