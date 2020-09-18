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
    public class BsUnidadMedida : IBsUnidadMedida
    {
        private readonly ApiDBContext context = null;
        public BsUnidadMedida(ApiDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserta un registro de UnidadMedida en base de datos
        /// </summary>
        /// <param name="UnidadMedida">Objeto de tipo UnidadMedida con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de UnidadMedida si se insertó correctamente</returns>
        public async Task<long> AgregaUnidadMedidaJsonAsync(UnidadMedida UnidadMedida)
        {
            long resultado = 0;
            try
            {
                var itemUnidadMedida = new CtUnidadMedida
                {
                    Descripcion = UnidadMedida.Descripcion,
                    Activo = UnidadMedida.Activo
                };
                context.CtUnidadMedida.Add(itemUnidadMedida);
                await context.SaveChangesAsync();
                resultado = itemUnidadMedida.PKIdUnidadMedida;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al registrar la UnidadMedida : {UnidadMedida.Descripcion}";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los registros de UnidadMedida activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo UnidadMedida</returns>
        public async Task<IList<UnidadMedida>> ObtenerUnidadesMedidaAsync()
        {
            Task<List<UnidadMedida>> listaUnidadMedida;
            try
            {
                listaUnidadMedida = context.CtUnidadMedida.Select(x => new UnidadMedida
                {
                    IdUnidadMedida = x.PKIdUnidadMedida,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                }).OrderBy(x => x.IdUnidadMedida).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener las Unidades de Medida.";
                throw new IOException(message, ex);
            }
            return await listaUnidadMedida;
        }

        /// <summary>
        /// Obtiene UnidadMedida por Id
        /// </summary>
        /// <param name="idUnidadMedida">Identificador de la UnidadMedida</param>
        /// <returns>Devuelve el objeto UnidadMedida seleccionado por ID</returns>
        public async Task<UnidadMedida> ObtenerUnidadMedidaPorIdAsync(int idUnidadMedida)
        {
            Task<UnidadMedida> unidadMedida;
            try
            {
                //Consulta para obtener UnidadMedida
                unidadMedida = context.CtUnidadMedida
                    .Where(x => x.PKIdUnidadMedida == idUnidadMedida)
                    .Select(x => new UnidadMedida
                    {
                        IdUnidadMedida = x.PKIdUnidadMedida,
                        Descripcion = x.Descripcion,
                        Activo = x.Activo
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la UnidadMedida.";
                throw new IOException(message, ex);
            }
            return await unidadMedida;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de UnidadMedida
        /// </summary>
        /// <param name="UnidadMedida">Objeto de tipo UnidadMedida con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        public async Task<long> EditarUnidadMedidaJsonAsync(UnidadMedida UnidadMedida)
        {
            long resultado = 0;
            try
            {
                CtUnidadMedida objUnidadMedida = context.CtUnidadMedida.Where(x => x.PKIdUnidadMedida == UnidadMedida.IdUnidadMedida).FirstOrDefault();
                objUnidadMedida.PKIdUnidadMedida = UnidadMedida.IdUnidadMedida;
                objUnidadMedida.Descripcion = UnidadMedida.Descripcion;
                objUnidadMedida.Activo = UnidadMedida.Activo;

                await context.SaveChangesAsync();
                resultado = objUnidadMedida.PKIdUnidadMedida;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al UnidadMedida.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza una baja lógica de UnidadMedida
        /// <param name="idUnidadMedida"/>Id de UnidadMedida a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        public async Task<int> EliminarUnidadMedidaAsync(int idUnidadMedida)
        {
            //Se inicializan variables
            int resultado = 0;

            try
            {
                CtUnidadMedida objDelete = context.CtUnidadMedida.Where(o => o.PKIdUnidadMedida == idUnidadMedida).FirstOrDefault();

                if (objDelete != null)
                {
                    objDelete.Activo = false;
                    await context.SaveChangesAsync();
                    resultado = 1;
                }
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al eliminar al UnidadMedida.";
                throw new IOException(message, ex);
            }

            //Devuelve resultado
            return await Task.FromResult<int>(resultado);
        }
    }
}
