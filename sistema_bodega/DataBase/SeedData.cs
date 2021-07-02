using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using sistema_bodega.Data;

namespace sistema_bodega.DataBase
{
    /// <summary>
    /// Pobla la base de datos si no hay datos ya en ella
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Pobla la base de datos para aquellas tablas que no tengan datos
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialization(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetService<BaseDatos>())
            {
                // Se llama a los metodos que poblan la base de datos en orden
                BodegasSeeder(context);
                EmpleadosSeeder(context);
                ProductosSeeder(context);
            }
        }

        /// <summary>
        /// Si no hay bodegas en la base de datos se añaden
        /// </summary>
        /// <param name="baseDatos"></param>
        private static void BodegasSeeder(BaseDatos baseDatos)
        {
            // Nos aseguramos de que no existan ya bodegas en la base de datos
            if (baseDatos.Bodegas.Any() == true)
            {
                return;
            }

            // Poblamos la base de datos
            baseDatos.Bodegas.AddRange(
                new Bodega
                {
                    Ciudad = "Antofagasta"
                },
                new Bodega
                {
                    Ciudad = "Calama"
                },
                new Bodega
                {
                    Ciudad = "Mejillones"
                },
                new Bodega
                {
                    Ciudad = "Tocopilla"
                }
            );

            // Se guardan los cambios
            baseDatos.SaveChanges();
        }

        /// <summary>
        /// Si no hay empleados en la base de datos se añaden
        /// </summary>
        /// <param name="baseDatos"></param>
        private static void EmpleadosSeeder(BaseDatos baseDatos)
        {
            // Nos aseguramos de que no existan ya empleados en la base de datos
            if (baseDatos.Empleados.Any() == true)
            {
                return;
            }

            // Poblamos la base de datos
            baseDatos.Empleados.AddRange(
                new Empleado
                {
                    Rut = "19952406-X",
                    Nombre = "Matias"
                },
                new Empleado
                {
                    Rut = "21260119-X",
                    Nombre = "Fyave"
                },
                new Empleado
                {
                    Rut = "13479067-X",
                    Nombre = "Barbara"
                },
                new Empleado
                {
                    Rut = "90678456-X",
                    Nombre = "Matias"
                },
                new Empleado
                {
                    Rut = "89567233-X",
                    Nombre = "Javier"
                }
            );

            // Se guardan los cambios
            baseDatos.SaveChanges();
        }

        /// <summary>
        /// Si no hay productos en la base de datos, se añaden
        /// </summary>
        /// <param name="baseDatos"></param>
        private static void ProductosSeeder(BaseDatos baseDatos)
        {
            // Nos aseguramos de que no existan ya productos en la base de datos
            if (baseDatos.Productos.Any() == true)
            {
                return;
            }

            // Poblamos la base de datos
            baseDatos.Productos.AddRange(
                new Producto
                {
                    Nombre = "Tuerca",
                    Umbral = 400,
                },
                new Producto
                {
                    Nombre = "Tornillo",
                    Umbral = 600,
                },
                new Producto
                {
                    Nombre = "Perno",
                    Umbral = 600
                },
                new Producto
                {
                    Nombre = "Aceite 1L",
                    Umbral = 100
                },
                new Producto
                {
                    Nombre = "Manguera 10m",
                    Umbral = 70
                },
                new Producto
                {
                    Nombre = "Cable 3m",
                    Umbral = 50
                },
                new Producto
                {
                    Nombre = "Agua desmineralizada 2L",
                    Umbral = 30
                }
            );

            // Guardamos los cambios
            baseDatos.SaveChanges();
        }
    }
}