namespace sistema_bodega.Data
{
    public class ProductoBodega
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int BodegaId { get; set; }
        public Bodega Bodega { get; set; }
        public int Cantidad { get; set; }
    }
}