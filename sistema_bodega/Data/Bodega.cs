using System.Collections.Generic;

namespace sistema_bodega.Data
{
    public class Bodega
    {
        public int Id { get; set; }
        public string Ciudad { get; set; }
        public List<ProductoBodega> ProductoBodegas { get; set; }
    }
}