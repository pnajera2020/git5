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
    public class BsMarca : IBsMarca
    {
        private readonly ApiDBContext context = null;
        public BsMarca(ApiDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserta un registro de Marca en base de datos
        /// </summary>
        /// <param name="Marca">Objeto de tipo Marca con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Marca si se insertó correctamente</returns>
        public async Task<long> AgregaMarcaJsonAsync(Marca Marca)
        {
            long resultado = 0;
            try
            {
                var itemMarca = new CtMarca
                {
                    Descripcion = Marca.Descripcion,
                    Activo = Marca.Activo
                };
                context.CtMarca.Add(itemMarca);
                await context.SaveChangesAsync();
                resultado = itemMarca.PKIdMarca;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al registrar la Marca : {Marca.Descripcion}";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los registros de Marca activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Marca</returns>
        public async Task<IList<Marca>> ObtenerMarcasAsync()
        {
            Task<List<Marca>> listaMarca;
            try
            {
                listaMarca = context.CtMarca.Select(x => new Marca
                {
                    IdMarca = x.PKIdMarca,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                }).OrderBy(x => x.IdMarca).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener las Marcas.";
                throw new IOException(message, ex);
            }
            return await listaMarca;
        }

        /// <summary>
        /// Obtiene Marca por Id
        /// </summary>
        /// <param name="idMarca">Identificador de la Marca</param>
        /// <returns>Devuelve el objeto Marca seleccionado por ID</returns>
        public async Task<Marca> ObtenerMarcaPorIdAsync(int idMarca)
        {
            Task<Marca> marca;
            try
            {
                //Consulta para obtener Marca
                marca = context.CtMarca
                    .Where(x => x.PKIdMarca == idMarca)
                    .Select(x => new Marca
                    {
                        IdMarca = x.PKIdMarca,
                        Descripcion = x.Descripcion,
                        Activo = x.Activo
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la Marca.";
                throw new IOException(message, ex);
            }
            return await marca;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Marca
        /// </summary>
        /// <param name="Marca">Objeto de tipo Marca con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        public async Task<long> EditarMarcaJsonAsync(Marca Marca)
        {
            long resultado = 0;
            try
            {
                CtMarca objMarca = context.CtMarca.Where(x => x.PKIdMarca == Marca.IdMarca).FirstOrDefault();
                objMarca.PKIdMarca = Marca.IdMarca;
                objMarca.Descripcion = Marca.Descripcion;
                objMarca.Activo = Marca.Activo;

                await context.SaveChangesAsync();
                resultado = objMarca.PKIdMarca;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al Marca.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza una baja lógica de Marca
        /// <param name="idMarca"/>Id de Marca a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        public async Task<int> EliminarMarcaAsync(int idMarca)
        {
            //Se inicializan variables
            int resultado = 0;

            try
            {
                CtMarca objDelete = context.CtMarca.Where(o => o.PKIdMarca == idMarca).FirstOrDefault();

                if (objDelete != null)
                {
                    objDelete.Activo = false;
                    await context.SaveChangesAsync();
                    resultado = 1;
                }
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al eliminar al Marca.";
                throw new IOException(message, ex);
            }

            //Devuelve resultado
            return await Task.FromResult<int>(resultado);
        }
    }
}
