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

namespace sistema_bodega.Pages.Retiros
{
    /// <summary>
    /// Modelo de la vista Index de los retiros
    /// </summary>
    public class IndexModel : PageModel
    {
        // Lista de retiros a listar
        public List<ProductoBodegaEmpleado> ProductosBodegasEmpleados { get; set; }

        // ID de la bodega para filtrar
        [BindProperty(SupportsGet = true)]
        public int BodegaId { get; set; }
        // Lista de bodegas para filtrar
        public SelectList Bodegas { get; set; }
        // ID del producto para filtrar
        [BindProperty(SupportsGet = true)]
        public int ProductoId { get; set; }
        // Lista de productos para filtrar
        public SelectList Productos { get; set; }
        // ID del empleado para filtrar
        [BindProperty(SupportsGet = true)]
        public int EmpleadoId { get; set; }
        // Lista de empleados para filtrar
        public SelectList Empleados { get; set; }

        public void OnGet()
        {
            // Establecemos conexion con la base de datos
            BaseDatos baseDatos = new BaseDatos();

            // Obtenemos una primera lista de retiros incluyendo los objetos Bodega, Producto y Empleado
            ProductosBodegasEmpleados = baseDatos.ProductosBodegasEmpleados
                .Include(pbe => pbe.Bodega)
                .Include(pbe => pbe.Producto)
                .Include(pbe => pbe.Empleado)
                .OrderBy(pbe => pbe.Fecha).ToList();

            // Creamos la lista de bodegas para filtrar
            Bodegas = new SelectList(baseDatos.Bodegas, nameof(Bodega.Id), nameof(Bodega.Ciudad));

            // Creamos la lista de productos para filtrar
            Productos = new SelectList(baseDatos.Productos, nameof(Producto.Id), nameof(Producto.Nombre));

            // Se crea una lista con los empleados
            Empleados = new SelectList(baseDatos.Empleados
                .Select(e => new
                {
                    Id = e.Id,
                    RutNombre = e.Rut + " - " + e.Nombre
                })
                , "Id", "RutNombre");

            // Se filtra por bodega
            if (BodegaId > 0)
            {
                ProductosBodegasEmpleados = ProductosBodegasEmpleados.Where(pbe => pbe.BodegaId == BodegaId).ToList();
            }

            // Se filtra por producto
            if (ProductoId > 0)
            {
                ProductosBodegasEmpleados = ProductosBodegasEmpleados.Where(pbe => pbe.ProductoId == ProductoId).ToList();
            }

            // Se filtra por empleado
            if (EmpleadoId > 0)
            {
                ProductosBodegasEmpleados = ProductosBodegasEmpleados.Where(pbe => pbe.EmpleadoId == EmpleadoId).ToList();
            }
        }
    }
}