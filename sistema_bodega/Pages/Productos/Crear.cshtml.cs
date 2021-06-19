using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using sistema_bodega.Data;

namespace sistema_bodega.Pages.Productos
{
    /// <summary>
    /// Permite crear un producto en la base de datos
    /// </summary>
    public class CrearModel : PageModel
    {
        /// <summary>
        /// Procesa la solicitud de crear un producto en la base de datos
        /// </summary>
        /// <param name="nombre">El nombre del producto</param>
        /// <param name="umbral">El umbral de stock critico del producto</param>
        public void OnPost(string nombre, int umbral)
        {
            BaseDatos baseDatos = new BaseDatos();
            Producto producto = new Producto();

            producto.Nombre = nombre;
            producto.Umbral = umbral;

            baseDatos.Productos.Add(producto);
            baseDatos.SaveChanges();
        }
    }
}