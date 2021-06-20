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

namespace sistema_bodega.Pages.Bodegas
{
    public class AlmacenarModel : PageModel
    {
        // Bodega donde se almacenara el producto
        [BindProperty]
        public Bodega Bodega { get; set; }
        // ID del producto que se va a almacenar
        public int ProductoId { get; set; }
        // Lista con los productos a elegir
        public SelectList Productos { get; set; }

        /// <summary>
        /// Recibe la id de la bodega en la que se almacenara el producto
        /// </summary>
        /// <param name="id">El id de la bodega</param>
        public IActionResult OnGet(int? id)
        {
            // Si no se ha incluido el id de una bodega
            if (id == null)
            {
                return NotFound();
            }

            // Se establece conexion con la base de datos
            BaseDatos baseDatos = new BaseDatos();

            // Obtenemos la bodega
            Bodega = baseDatos.Bodegas.FirstOrDefault(b => b.Id == id);

            // Si la bodega con el id dado no existe
            if (Bodega == null)
            {
                return NotFound();
            }

            // Se crea la lista con los productos
            Productos = new SelectList(baseDatos.Productos, nameof(Producto.Id), nameof(Producto.Nombre));

            // Se carga la pagina
            return Page();
        }

        public IActionResult OnPost(int id_producto, int cantidad)
        {
            // Se establece conexion con la base de datos
            BaseDatos baseDatos = new BaseDatos();

            // Se busca una relacion ProductoBodega ya existente
            ProductoBodega productoBodega = baseDatos.ProductosBodegas
                .Where(pb => pb.ProductoId == id_producto && pb.BodegaId == Bodega.Id)
                .FirstOrDefault();

            // Si ya existe sus valores se actualizan
            if (productoBodega != null)
            {
                productoBodega.Cantidad += cantidad;
            }
            // Si no, se crea
            else
            {
                // Se crea una nueva relacion
                productoBodega = new ProductoBodega();

                productoBodega.ProductoId = id_producto;
                productoBodega.BodegaId = Bodega.Id;
                productoBodega.Cantidad = cantidad;

                // Se guarda la relacion en la base de datos
                baseDatos.ProductosBodegas.Add(productoBodega);
            }

            // Se guardan los cambios
            baseDatos.SaveChanges();

            // Se refresca la pagina con el id de la bodega
            return RedirectToPage("./Almacenar", new { id = Bodega.Id });
        }
    }
}