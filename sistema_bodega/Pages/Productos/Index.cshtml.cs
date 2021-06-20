using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using sistema_bodega.Data;

namespace sistema_bodega.Pages.Productos
{
    /// <summary>
    /// Modelo de la vista Index para los productos
    /// </summary>
    public class IndexModel : PageModel
    {
        // Productos a listar
        public List<Producto> Productos { get; set; }
        // Para filtrar los productos y obtener su stock total
        public List<ProductoBodega> ProductosBodegas { get; set; }
        // Lista con objetos temporales que relacionan producto y su stock total
        public List<TempProducto> TempProductos { get; set; }

        // ID de la bodega para filtrar
        [BindProperty(SupportsGet = true)]
        public int BodegaId { get; set; }
        // Lista con todas las bodegas
        public SelectList Bodegas { get; set; }


        public void OnGet()
        {
            // Establecemos conexion con la base de datos
            BaseDatos baseDatos = new BaseDatos();

            // Obtenemos de la base de datos las colecciones con las que vamos a trabajar
            Productos = baseDatos.Productos.ToList();
            Bodegas = new SelectList(baseDatos.Bodegas, nameof(Bodega.Id), nameof(Bodega.Ciudad));

            // Verificamos si debemos filtrar por bodega
            if (BodegaId > 0)
            {
                // Obtenemos los productos de la base de datos a traves de la relacion ProductoBodega para filtrarlos por bodega
                ProductosBodegas = baseDatos.ProductosBodegas
                    .Where(pb => pb.BodegaId == BodegaId)
                    .Include(pb => pb.Bodega).ToList();
            }
            else
            {
                // Obtenemos los productos de la base de datos
                List<Producto> productos = baseDatos.Productos.ToList();
                // Obtenemos las relaciones ProductoBodega para calcular el stock total
                List<ProductoBodega> productosBodegas = baseDatos.ProductosBodegas.ToList();

                // Creamos y poblamos una lista que una al producto y su stock
                TempProductos = new List<TempProducto>();

                foreach (Producto producto in Productos)
                {
                    TempProducto temp = new TempProducto();
                    temp.Producto = producto;

                    // Por cada coincidencia aumentamos el stock
                    foreach (ProductoBodega productoBodega in productosBodegas)
                    {
                        if (productoBodega.ProductoId == producto.Id)
                        {
                            temp.Stock += productoBodega.Cantidad;
                        }
                    }

                    TempProductos.Add(temp);
                }
            }
        }
    }

    public class TempProducto
    {
        public Producto Producto { get; set; }
        public int Stock { get; set; }
    }
}