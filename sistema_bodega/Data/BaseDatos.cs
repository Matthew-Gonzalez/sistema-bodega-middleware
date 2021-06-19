using Microsoft.EntityFrameworkCore;

namespace sistema_bodega.Data
{
    /// <summary>
    /// Establece conexion con la base de datos
    /// </summary>
    public class BaseDatos : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SistemaBodega;Integrated Security=true");
        }
    }
}