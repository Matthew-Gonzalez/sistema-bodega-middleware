using System;

namespace sistema_bodega.Data
{
    public class ProductoBodegaEmpleado
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int BodegaId { get; set; }
        public Bodega Bodega { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
    }
}