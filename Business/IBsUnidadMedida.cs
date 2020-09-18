using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IBsUnidadMedida
    {
        /// <summary>
        /// Inserta un registro de UnidadMedida en base de datos
        /// </summary>
        /// <param name="UnidadMedida">Objeto de tipo UnidadMedida con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de UnidadMedida si se insertó correctamente</returns>
        Task<long> AgregaUnidadMedidaJsonAsync(UnidadMedida UnidadMedida);

        /// <summary>
        /// Obtiene todos los registros de UnidadMedida activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo UnidadMedida</returns>
        Task<IList<UnidadMedida>> ObtenerUnidadesMedidaAsync();

        /// <summary>
        /// Obtiene UnidadMedida por Id
        /// </summary>
        /// <param name="idUnidadMedida">Identificador de la UnidadMedida</param>
        /// <returns>Devuelve el objeto UnidadMedida seleccionado por ID</returns>
        Task<UnidadMedida> ObtenerUnidadMedidaPorIdAsync(int idUnidadMedida);

        /// <summary>
        /// Realiza la actualización de datos de un registro de UnidadMedida
        /// </summary>
        /// <param name="UnidadMedida">Objeto de tipo UnidadMedida con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        Task<long> EditarUnidadMedidaJsonAsync(UnidadMedida UnidadMedida);

        /// <summary>
        /// Realiza una baja lógica de UnidadMedida
        /// <param name="idUnidadMedida"/>Id de UnidadMedida a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        Task<int> EliminarUnidadMedidaAsync(int idUnidadMedida);
    }
}
