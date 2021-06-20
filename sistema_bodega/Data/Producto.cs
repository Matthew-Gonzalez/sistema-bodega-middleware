using System.Collections.Generic;

namespace sistema_bodega.Data
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Umbral { get; set; }
        public List<ProductoBodega> ProductoBodegas { get; set; }
        public List<ProductoBodegaEmpleado> ProductoBodegaEmpleados { get; set; }
    }
}