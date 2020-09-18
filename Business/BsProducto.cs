using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class BsProducto : IBsProducto
    {
        private readonly ApiDBContext context = null;
        public BsProducto(ApiDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserta un registro de Producto en base de datos
        /// </summary>
        /// <param name="Producto">Objeto de tipo Producto con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Producto si se insertó correctamente</returns>
        public async Task<long> AgregaProductoJsonAsync(Producto producto)
        {
            long resultado = 0;
            try
            {
                var itemProducto = new CtProducto
                {
                    SKU = producto.SKU,
                    Descripcion = producto.Descripcion,
                    PrecioVenta = producto.PrecioVenta,
                    Costo = producto.Costo,
                    Imagen = producto.Imagen,
                    FKIdProveedor = producto.IdProveedor,
                    FKIdCategoria = producto.IdCategoria,
                    FKIdMarca = producto.IdMarca,
                    FKIdUnidadMedida = producto.IdUnidadMedida,
                    Activo = producto.Activo
                };
                context.CtProducto.Add(itemProducto);
                await context.SaveChangesAsync();
                resultado = itemProducto.PKIdProducto;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al registrar la Producto : {producto.Descripcion}";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los registros de Producto 
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Producto</returns>
        public async Task<IList<Producto>> ObtenerProductosAsync()
        {
            Task<List<Producto>> listaProducto;
            try
            {
                //listaProducto = context.CtProducto.Where(x => x.Activo).Select(x => new Producto
                listaProducto = context.CtProducto.Select(x => new Producto
                {
                    IdProducto = x.PKIdProducto,
                    SKU = x.SKU,
                    Descripcion = x.Descripcion,
                    PrecioVenta = x.PrecioVenta,
                    Costo = x.Costo,
                    Imagen = x.Imagen,
                    IdProveedor = x.FKIdProveedor,
                    IdCategoria = x.FKIdCategoria,
                    IdMarca = x.FKIdMarca,
                    IdUnidadMedida = x.FKIdUnidadMedida,
                    Activo = x.Activo
                }).OrderBy(x => x.IdProducto).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener las Productoes.";
                throw new IOException(message, ex);
            }
            return await listaProducto;
        }

        /// <summary>
        /// Obtiene Producto por Id
        /// </summary>
        /// <param name="idProducto">Identificador de la Producto</param>
        /// <returns>Devuelve el objeto Producto seleccionado por ID</returns>
        public async Task<Producto> ObtenerProductoPorIdAsync(int idProducto)
        {
            Task<Producto> Producto;
            try
            {
                //Consulta para obtener Producto
                Producto = context.CtProducto
                    .Where(x => x.PKIdProducto == idProducto)
                    .Select(x => new Producto
                    {
                        IdProducto = x.PKIdProducto,
                        SKU = x.SKU,
                        Descripcion = x.Descripcion,
                        PrecioVenta = x.PrecioVenta,
                        Costo = x.Costo,
                        Imagen = x.Imagen,
                        IdProveedor = x.FKIdProveedor,
                        IdCategoria = x.FKIdCategoria,
                        IdMarca = x.FKIdMarca,
                        IdUnidadMedida = x.FKIdUnidadMedida,
                        Activo = x.Activo
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la Producto.";
                throw new IOException(message, ex);
            }
            return await Producto;
        }

        /// <summary>
        /// Obtiene Producto por SKU
        /// </summary>
        /// <param name="sku">SKU del Producto</param>
        /// <returns>Devuelve el objeto Producto seleccionado por SKU</returns>
        public async Task<Producto> ObtenerProductoPorSKUAsync(string sku)
        {
            Task<Producto> producto;
            try
            {
                //Consulta para obtener Producto
                producto = context.CtProducto
                     .Where(x => x.SKU == sku)
                     .Select(x => new Producto
                     {
                         IdProducto = x.PKIdProducto,
                         SKU = x.SKU,
                         Descripcion = x.Descripcion,
                         PrecioVenta = x.PrecioVenta,
                         Costo = x.Costo,
                         Imagen = x.Imagen,
                         IdProveedor = x.FKIdProveedor,
                         IdCategoria = x.FKIdCategoria,
                         IdMarca = x.FKIdMarca,
                         IdUnidadMedida = x.FKIdUnidadMedida,
                         Activo = x.Activo
                     }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la Producto.";
                throw new IOException(message, ex);
            }
            return await producto;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Producto
        /// </summary>
        /// <param name="Producto">Objeto de tipo Producto con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        public async Task<long> EditarProductoJsonAsync(Producto producto)
        {
            long resultado = 0;
            try
            {
                CtProducto objProducto = context.CtProducto.Where(x => x.PKIdProducto == producto.IdProducto).FirstOrDefault();
                //objProducto.PKIdProducto = producto.IdProducto;
                objProducto.SKU = producto.SKU;
                objProducto.Descripcion = producto.Descripcion;
                objProducto.PrecioVenta = producto.PrecioVenta;
                objProducto.Costo = producto.Costo;
                objProducto.Imagen = producto.Imagen;
                objProducto.FKIdProveedor = producto.IdProveedor;
                objProducto.FKIdCategoria = producto.IdCategoria;
                objProducto.FKIdMarca = producto.IdMarca;
                objProducto.FKIdUnidadMedida = producto.IdUnidadMedida;
                objProducto.Activo = producto.Activo;

                await context.SaveChangesAsync();
                resultado = objProducto.PKIdProducto;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al Producto.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza una baja lógica de Producto
        /// <param name="idProducto"/>Id de Producto a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        public async Task<int> EliminarProductoAsync(int idProducto)
        {
            //Se inicializan variables
            int resultado = 0;

            try
            {
                CtProducto objDelete = context.CtProducto.Where(o => o.PKIdProducto == idProducto).FirstOrDefault();

                if (objDelete != null)
                {
                    objDelete.Activo = false;
                    await context.SaveChangesAsync();
                    resultado = 1;
                }
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al eliminar al Producto.";
                throw new IOException(message, ex);
            }

            //Devuelve resultado
            return await Task.FromResult<int>(resultado);
        }
    }
}
