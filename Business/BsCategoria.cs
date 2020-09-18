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
    public class BsCategoria : IBsCategoria
    {
        private readonly ApiDBContext context = null;
        public BsCategoria(ApiDBContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserta un registro de Categoria en base de datos
        /// </summary>
        /// <param name="Categoria">Objeto de tipo Categoria con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Categoria si se insertó correctamente</returns>
        public async Task<long> AgregaCategoriaJsonAsync(Categoria Categoria)
        {
            long resultado = 0;
            try
            {
                var itemCategoria = new CtCategoria
                {
                    Descripcion = Categoria.Descripcion,
                    Activo = Categoria.Activo
                };
                context.CtCategoria.Add(itemCategoria);
                await context.SaveChangesAsync();
                resultado = itemCategoria.PKIdCategoria;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al registrar la Categoria : {Categoria.Descripcion}";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los registros de Categoria activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Categoria</returns>
        public async Task<IList<Categoria>> ObtenerCategoriasAsync()
        {
            Task<List<Categoria>> listaCategoria;
            try
            {
                listaCategoria = context.CtCategoria.Select(x => new Categoria
                {
                    IdCategoria = x.PKIdCategoria,
                    Descripcion = x.Descripcion,
                    Activo = x.Activo
                }).OrderBy(x => x.IdCategoria).ToListAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener las Categoriaes.";
                throw new IOException(message, ex);
            }
            return await listaCategoria;
        }

        /// <summary>
        /// Obtiene Categoria por Id
        /// </summary>
        /// <param name="idCategoria">Identificador de la Categoria</param>
        /// <returns>Devuelve el objeto Categoria seleccionado por ID</returns>
        public async Task<Categoria> ObtenerCategoriaPorIdAsync(int idCategoria)
        {
            Task<Categoria> categoria;
            try
            {
                //Consulta para obtener Categoria
                categoria = context.CtCategoria
                    .Where(x => x.PKIdCategoria == idCategoria)
                    .Select(x => new Categoria
                    {
                        IdCategoria = x.PKIdCategoria,
                        Descripcion = x.Descripcion,
                        Activo = x.Activo
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al obtener la Categoria.";
                throw new IOException(message, ex);
            }
            return await categoria;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Categoria
        /// </summary>
        /// <param name="Categoria">Objeto de tipo Categoria con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        public async Task<long> EditarCategoriaJsonAsync(Categoria Categoria)
        {
            long resultado = 0;
            try
            {
                CtCategoria objCategoria = context.CtCategoria.Where(x => x.PKIdCategoria == Categoria.IdCategoria).FirstOrDefault();
                objCategoria.PKIdCategoria = Categoria.IdCategoria;
                objCategoria.Descripcion = Categoria.Descripcion;
                objCategoria.Activo = Categoria.Activo;

                await context.SaveChangesAsync();
                resultado = objCategoria.PKIdCategoria;
            }
            catch (Exception e)
            {
                var message = $"Ocurrió un error al actualizar al Categoria.";
                throw new IOException(message, e);
            }
            return resultado;
        }

        /// <summary>
        /// Realiza una baja lógica de Categoria
        /// <param name="idCategoria"/>Id de Categoria a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        public async Task<int> EliminarCategoriaAsync(int idCategoria)
        {
            //Se inicializan variables
            int resultado = 0;

            try
            {
                CtCategoria objDelete = context.CtCategoria.Where(o => o.PKIdCategoria == idCategoria).FirstOrDefault();

                if (objDelete != null)
                {
                    objDelete.Activo = false;
                    await context.SaveChangesAsync();
                    resultado = 1;
                }
            }
            catch (Exception ex)
            {
                var message = $"Ocurrió un error al eliminar al Categoria.";
                throw new IOException(message, ex);
            }

            //Devuelve resultado
            return await Task.FromResult<int>(resultado);
        }
    }
}
