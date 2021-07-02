using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using sistema_bodega.Data;

namespace sistema_bodega.Pages.Bodegas
{
    /// <summary>
    /// Modelo de la vista Index de las bodegas
    /// </summary>
    public class IndexModel : PageModel
    {
        // Conexion a la base de datos
        private readonly BaseDatos _baseDatos;
        // Bodegas a listar
        public List<Bodega> Bodegas { get; set; }

        public IndexModel(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public void OnGet()
        {
            // Obtenemos de la base de datos la lista de bodegas
            Bodegas = _baseDatos.Bodegas.ToList();
        }
    }
}