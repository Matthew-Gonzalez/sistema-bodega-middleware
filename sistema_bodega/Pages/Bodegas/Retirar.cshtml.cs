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
    /// <summary>
    /// Modelo de la vista Retirar de las bodegas
    /// </summary>
    public class RetirarModel : PageModel
    {
        // Conexion a la base de datos
        private readonly BaseDatos _baseDatos;

        // Bodega desde la cual se va a retirar el producto
        [BindProperty]
        public Bodega Bodega { get; set; }
        // Lista de productos a elegir
        public SelectList Productos { get; set; }
        // Lista de empleados a elegir
        public SelectList Empleados { get; set; }

        public RetirarModel(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public IActionResult OnGet(int? id)
        {
            // Si no se ha incluido el id de una bodega
            if (id == null)
            {
                return NotFound();
            }

            // Se obtiene la bodega
            Bodega = _baseDatos.Bodegas
                .Where(b => b.Id == id)
                .FirstOrDefault();

            // Si la bodega con el id dado no existe
            if (Bodega == null)
            {
                return NotFound();
            }

            // Se crea una lista con los productos que tengan existencias en la bodega
            Productos = new SelectList(_baseDatos.ProductosBodegas
                .Include(pb => pb.Producto)
                .Where(pb => pb.BodegaId == id && pb.Cantidad > 0)
                .Select(p => new
                {
                    Id = p.Producto.Id,
                    Nombre = p.Producto.Nombre
                })
                , "Id", "Nombre");

            // Se crea una lista con los empleados
            Empleados = new SelectList(_baseDatos.Empleados
                .Select(e => new
                {
                    Id = e.Id,
                    RutNombre = e.Rut + " - " + e.Nombre
                })
                , "Id", "RutNombre");

            // Se carga la pagina
            return Page();
        }

        public IActionResult OnPost(DateTime fecha, int id_empleado, int id_producto, int cantidad)
        {
            // Se busca una relacion ProductoBodega ya existente
            ProductoBodega productoBodega = _baseDatos.ProductosBodegas
                .Where(pb => pb.ProductoId == id_producto && pb.BodegaId == Bodega.Id)
                .FirstOrDefault();

            // Aqui se debe validar si hay stock del producto en la bodega, esta validacion es temporal
            if (productoBodega == null)
            {
                return NotFound();
            }
            else if (productoBodega.Cantidad < cantidad)
            {
                return NotFound();
            }

            // Se actualiza la cantidad de stock que hay en la bodega
            productoBodega.Cantidad -= cantidad;

            // Se crea la entidad ProductoBodegaEmpleado
            ProductoBodegaEmpleado productoBodegaEmpleado = new ProductoBodegaEmpleado();

            productoBodegaEmpleado.ProductoId = id_producto;
            productoBodegaEmpleado.BodegaId = Bodega.Id;
            productoBodegaEmpleado.EmpleadoId = id_empleado;
            productoBodegaEmpleado.Cantidad = cantidad;
            productoBodegaEmpleado.Fecha = fecha;

            // Se agrega la entidad a la base de datos y se guardan los cambios
            _baseDatos.ProductosBodegasEmpleados.Add(productoBodegaEmpleado);
            _baseDatos.SaveChanges();

            // Se refresca la pagina con el id de la bodega
            return RedirectToPage("./Retirar", new { id = Bodega.Id });
        }
    }
}