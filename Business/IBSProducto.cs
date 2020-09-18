using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IBsProducto
    {
        /// <summary>
        /// Inserta un registro de Producto en base de datos
        /// </summary>
        /// <param name="Producto">Objeto de tipo Producto con la información ingresada</param>
        /// <returns>Variable de tipo entero indicando el identificador de Producto si se insertó correctamente</returns>
        Task<long> AgregaProductoJsonAsync(Producto Producto);

        /// <summary>
        /// Obtiene todos los registros de Producto activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Producto</returns>
        Task<IList<Producto>> ObtenerProductosAsync();

        /// <summary>
        /// Obtiene Producto por Id
        /// </summary>
        /// <param name="idProducto">Identificador de la Producto</param>
        /// <returns>Devuelve el objeto Producto seleccionado por ID</returns>
        Task<Producto> ObtenerProductoPorIdAsync(int idProducto);

        /// <summary>
        /// Obtiene Producto por SKU
        /// </summary>
        /// <param name="sku">SKU del Producto</param>
        /// <returns>Devuelve el objeto Producto seleccionado por SKU</returns>
        Task<Producto> ObtenerProductoPorSKUAsync(string sku);

        /// <summary>
        /// Realiza la actualización de datos de un registro de Producto
        /// </summary>
        /// <param name="Producto">Objeto de tipo Producto con la información actualizada</param>
        /// <returns>Regresa el identificador del registro actualizado en caso exitoso</returns>
        Task<long> EditarProductoJsonAsync(Producto Producto);

        /// <summary>
        /// Realiza una baja lógica de Producto
        /// <param name="idProducto"/>Id de Producto a eliminar
        /// </summary>
        /// <returns>Regresa un 1 en caso exitoso, 0 si ocurre algún error</returns>
        Task<int> EliminarProductoAsync(int idProducto);
    }
}
