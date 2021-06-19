using Microsoft.EntityFrameworkCore;

namespace sistema_bodega.Data
{
    /// <summary>
    /// Establece conexion con la base de datos
    /// </summary>
    public class BaseDatos : DbContext
    {
        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SistemaBodega;Integrated Security=true");
        }
    }
}