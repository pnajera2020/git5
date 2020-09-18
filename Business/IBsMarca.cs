using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IBsMarca
    {
        /// <summary>
        /// Inserta un registro de Marca en base de datos
        /// </summary>
        /// <param name="Marca">Objeto de tipo Marca con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Marca si se insertó correctamente</returns>
        Task<long> AgregaMarcaJsonAsync(Marca Marca);

        /// <summary>
        /// Obtiene todos los registros de Marca activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Marca</returns>
        Task<IList<Marca>> ObtenerMarcasAsync();

        /// <summary>
        /// Obtiene Marca por Id
        /// </summary>
        /// <param name="idMarca">Identificador de la Marca</param>
        /// <returns>Devuelve el objeto Marca seleccionado por ID</returns>
        Task<Marca> ObtenerMarcaPorIdAsync(int idMarca);

        /// <summary>
        /// Realiza la actualización de datos de un registro de Marca
        /// </summary>
        /// <param name="Marca">Objeto de tipo Marca con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        Task<long> EditarMarcaJsonAsync(Marca Marca);

        /// <summary>
        /// Realiza una baja lógica de Marca
        /// <param name="idMarca"/>Id de Marca a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        Task<int> EliminarMarcaAsync(int idMarca);
    }
}
