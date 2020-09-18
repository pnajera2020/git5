using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IBsCategoria
    {
        /// <summary>
        /// Inserta un registro de Categoria en base de datos
        /// </summary>
        /// <param name="Categoria">Objeto de tipo Categoria con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Categoria si se insertó correctamente</returns>
        Task<long> AgregaCategoriaJsonAsync(Categoria Categoria);

        /// <summary>
        /// Obtiene todos los registros de Categoria activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Categoria</returns>
        Task<IList<Categoria>> ObtenerCategoriasAsync();

        /// <summary>
        /// Obtiene Categoria por Id
        /// </summary>
        /// <param name="idCategoria">Identificador de la Categoria</param>
        /// <returns>Devuelve el objeto Categoria seleccionado por ID</returns>
        Task<Categoria> ObtenerCategoriaPorIdAsync(int idCategoria);

        /// <summary>
        /// Realiza la actualización de datos de un registro de Categoria
        /// </summary>
        /// <param name="Categoria">Objeto de tipo Categoria con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        Task<long> EditarCategoriaJsonAsync(Categoria Categoria);

        /// <summary>
        /// Realiza una baja lógica de Categoria
        /// <param name="idCategoria"/>Id de Categoria a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        Task<int> EliminarCategoriaAsync(int idCategoria);
    }
}
