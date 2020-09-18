using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IBsProveedor
    {
        /// <summary>
        /// Inserta un registro de Proveedor en base de datos
        /// </summary>
        /// <param name="Proveedor">Objeto de tipo Proveedor con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Proveedor si se insertó correctamente</returns>
        Task<long> AgregaProveedorJsonAsync(Proveedor proveedor);

        /// <summary>
        /// Obtiene todos los registros de Proveedor activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Proveedor</returns>
        Task<IList<Proveedor>> ObtenerProveedoresAsync();

        /// <summary>
        /// Obtiene Proveedor por Id
        /// </summary>
        /// <param name="idProveedor">Identificador de la Proveedor</param>
        /// <returns>Devuelve el objeto Proveedor seleccionado por ID</returns>
        Task<Proveedor> ObtenerProveedorPorIdAsync(int idProveedor);

        /// <summary>
        /// Realiza la actualización de datos de un registro de Proveedor
        /// </summary>
        /// <param name="Proveedor">Objeto de tipo Proveedor con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        Task<long> EditarProveedorJsonAsync(Proveedor Proveedor);

        /// <summary>
        /// Realiza una baja lógica de Proveedor
        /// <param name="idProveedor"/>Id de Proveedor a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        Task<int> EliminarProveedorAsync(int idProveedor);
    }
}
