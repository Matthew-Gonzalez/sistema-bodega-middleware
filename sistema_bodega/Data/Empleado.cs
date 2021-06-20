using System.Collections.Generic;

namespace sistema_bodega.Data
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public List<ProductoBodegaEmpleado> ProductoBodegaEmpleados { get; set; }
    }
}