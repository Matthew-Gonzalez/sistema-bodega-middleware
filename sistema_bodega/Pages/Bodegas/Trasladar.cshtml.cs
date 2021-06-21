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
    public class TrasladarModel : PageModel
    {
        // Bodega desde la cual trasladaremos los productos
        [BindProperty]
        public Bodega Bodega { get; set; }
        // Lista de bodegas destino para escoger
        public SelectList Bodegas { get; set; }
        // Lista de productos a escoger
        public SelectList Productos { get; set; }

        public IActionResult OnGet(int? id)
        {
            // Si no se ha incluido el id de una bodega
            if (id == null)
            {
                return NotFound();
            }

            // Se establece conexion con la base de datos
            BaseDatos baseDatos = new BaseDatos();

            // Se obtiene la bodega
            Bodega = baseDatos.Bodegas
                .Where(b => b.Id == id)
                .FirstOrDefault();

            // Si la bodega con el id dado no existe
            if (Bodega == null)
            {
                return NotFound();
            }

            // Se crea una lista con las bodegas
            Bodegas = new SelectList(baseDatos.Bodegas
                .Where(b => b.Id != id),
                 nameof(Bodega.Id), nameof(Bodega.Ciudad));

            // Se crea una lista con los productos que tengan existencias en la bodega
            Productos = new SelectList(baseDatos.ProductosBodegas
                .Include(pb => pb.Producto)
                .Where(pb => pb.BodegaId == id && pb.Cantidad > 0)
                .Select(p => new
                {
                    Id = p.Producto.Id,
                    Nombre = p.Producto.Nombre
                })
                , "Id", "Nombre");

            // Se carga la pagina
            return Page();
        }

        public IActionResult OnPost(int id_bodega, int id_producto, int cantidad)
        {
            // Se establece conexion con la base de datos
            BaseDatos baseDatos = new BaseDatos();

            // Se obtiene la relacion ProductoBodega de la bodega de origen
            ProductoBodega productoBodegaOrigen = baseDatos.ProductosBodegas
                .Where(pb => pb.ProductoId == id_producto && pb.BodegaId == Bodega.Id)
                .FirstOrDefault();

            // Lo primero es verificar si la bodega de origen tiene suficientes existencias para trasladar
            if (productoBodegaOrigen.Cantidad < cantidad)
            {
                return NotFound();
            }

            // Se reduce el stock de la bodega de origen
            productoBodegaOrigen.Cantidad -= cantidad;

            // Se guardan los cambios y se da espacio a la siguiente entidad
            baseDatos.SaveChanges();
            baseDatos.Entry(productoBodegaOrigen).State = EntityState.Detached;

            // Se obtiene la relacion ProductoBodega de la bodega de destino
            ProductoBodega productoBodegaDestino = baseDatos.ProductosBodegas
                .Where(pb => pb.ProductoId == id_producto && pb.BodegaId == id_bodega)
                .FirstOrDefault();

            // Luego verificamos si la bodega de destino ya posee existencias del producto
            if (productoBodegaDestino != null)
            {
                // Actualizamos las existencias
                productoBodegaDestino.Cantidad += cantidad;
            }
            else
            {
                // Se crea la relacion
                productoBodegaDestino = new ProductoBodega();

                productoBodegaDestino.ProductoId = id_producto;
                productoBodegaDestino.BodegaId = id_bodega;
                productoBodegaDestino.Cantidad = cantidad;

                // Se guarda la relacion en la base de datos
                baseDatos.ProductosBodegas.Add(productoBodegaDestino);
            }

            // Se guardan los cambios en la base de datos
            baseDatos.SaveChanges();

            // Se refresca la pagina con el id de la bodega
            return RedirectToPage("./Trasladar", new
            {
                id = Bodega.Id
            });
        }
    }
}