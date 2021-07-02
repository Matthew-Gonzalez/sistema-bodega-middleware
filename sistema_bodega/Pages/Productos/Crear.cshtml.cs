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
    /// Modelo de la vista Crear de los productos
    /// </summary>
    public class CrearModel : PageModel
    {
        // Conexion a la base de datos
        private readonly BaseDatos _baseDatos;

        public CrearModel(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        /// <summary>
        /// Procesa la solicitud de crear un producto en la base de datos
        /// </summary>
        /// <param name="nombre">El nombre del producto</param>
        /// <param name="umbral">El umbral de stock critico del producto</param>
        public void OnPost(string nombre, int umbral)
        {
            // Se crea un producto nuevo
            Producto producto = new Producto();

            // Se da un valor a los atributos del producto
            producto.Nombre = nombre;
            producto.Umbral = umbral;

            // Se agrega el producto a la base de datos y se guardan los cambios
            _baseDatos.Productos.Add(producto);
            _baseDatos.SaveChanges();
        }
    }
}