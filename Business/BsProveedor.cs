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
    public class BsProveedor : IBsProveedor
    {
        private readonly ApiDBContext context = null;
        public BsProveedor(ApiDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserta un registro de Proveedor en base de datos
        /// </summary>
        /// <param name="Proveedor">Objeto de tipo Proveedor con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Proveedor si se insertó correctamente</returns>
        public async Task<long> AgregaProveedorJsonAsync(Proveedor proveedor)
        {
            long resultado = 0;
            try
            {
                var itemProveedor = new CtProveedor
                {
                    Descripcion = proveedor.Descripcion,
                    Activo = proveedor.Activo
                };
                context.CtProveedor.Add(itemProveedor);
                await context.SaveChangesAsync();
                resultado = itemProveedor.PKIdProveedor;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al registrar la Proveedor : {proveedor.Descripcion}";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los registros de Proveedor activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Proveedor</returns>
        public async Task<IList<Proveedor>> ObtenerProveedoresAsync()
        {
            Task<List<Proveedor>> listaProveedor;
            try
            {
                listaProveedor = context.CtProveedor.Select(x => new Proveedor
                {
                    IdProveedor = x.PKIdProveedor,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                }).OrderBy(x => x.IdProveedor).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener las Proveedores.";
                throw new IOException(message, ex);
            }
            return await listaProveedor;
        }

        /// <summary>
        /// Obtiene Proveedor por Id
        /// </summary>
        /// <param name="idProveedor">Identificador de la Proveedor</param>
        /// <returns>Devuelve el objeto Proveedor seleccionado por ID</returns>
        public async Task<Proveedor> ObtenerProveedorPorIdAsync(int idProveedor)
        {
            Task<Proveedor> proveedor;
            try
            {
                //Consulta para obtener Proveedor
                proveedor = context.CtProveedor
                    .Where(x => x.PKIdProveedor == idProveedor)
                    .Select(x => new Proveedor
                    {
                        IdProveedor = x.PKIdProveedor,
                        Descripcion = x.Descripcion,
                        Activo = x.Activo
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la Proveedor.";
                throw new IOException(message, ex);
            }
            return await proveedor;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Proveedor
        /// </summary>
        /// <param name="Proveedor">Objeto de tipo Proveedor con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        public async Task<long> EditarProveedorJsonAsync(Proveedor proveedor)
        {
            long resultado = 0;
            try
            {
                CtProveedor objProveedor = context.CtProveedor.Where(x => x.PKIdProveedor == proveedor.IdProveedor).FirstOrDefault();
                objProveedor.PKIdProveedor = proveedor.IdProveedor;
                objProveedor.Descripcion = proveedor.Descripcion;
                objProveedor.Activo = proveedor.Activo;

                await context.SaveChangesAsync();
                resultado = objProveedor.PKIdProveedor;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al proveedor.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza una baja lógica de Proveedor
        /// <param name="idProveedor"/>Id de Proveedor a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        public async Task<int> EliminarProveedorAsync(int idProveedor)
        {
            //Se inicializan variables
            int resultado = 0;

            try
            {
                CtProveedor objDelete = context.CtProveedor.Where(o => o.PKIdProveedor == idProveedor).FirstOrDefault();

                if (objDelete != null)
                {
                    objDelete.Activo = false;
                    await context.SaveChangesAsync();
                    resultado = 1;
                }
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al eliminar al Proveedor.";
                throw new IOException(message, ex);
            }

            //Devuelve resultado
            return await Task.FromResult<int>(resultado);
        }
    }
}
