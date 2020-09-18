using Business;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Productos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ProductoController> _logger = null;
        private readonly IBsProducto _bsProducto = null;

        public ProductoController(ILogger<ProductoController> logger, IBsProducto bsProducto)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bsProducto = bsProducto ?? throw new ArgumentNullException(nameof(bsProducto));
        }

        /// <summary>
        /// Inserta un registro de Producto en base de datos
        /// </summary>
        /// <param name="Producto">Objeto de tipo Producto con la información ingresada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPost("agregarProducto")]
        public async Task<ActionResult> AgregarProducto([FromBody]Producto producto)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsProducto.AgregaProductoJsonAsync(producto);
                result = Ok(resultado >= 1 ? "Registro exitoso " : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                resultado = 0;
                result = StatusCode(StatusCodes.Status500InternalServerError, resultado);
                _logger.LogInformation($"Ha ocurrido un error: {ex.Message.ToString()}");
            }
            return result;
        }

        /// <summary>
        /// Obtiene todos los registros de Producto 
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Producto</returns>
        [HttpGet("obtenerProductos")]
        public async Task<ActionResult<IList<Producto>>> ObtenerProductoesAsync()
        {
            ObjectResult valRet;
            _logger.LogInformation("ObtenerProductosAsync: INICIO");

            IList<Producto> listaProductoes = null;
            try
            {
                listaProductoes = await _bsProducto.ObtenerProductosAsync();
                valRet = Ok(listaProductoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                valRet = StatusCode(StatusCodes.Status500InternalServerError, null);
            }
            _logger.LogInformation("ObtenerProductosAsync: FIN");
            return valRet;
        }

        /// <summary>
        /// Obtiene Producto por Id
        /// </summary>
        /// <param name="idProducto">Identificador del Producto</param>
        /// <returns>Devuelve el objeto Producto seleccionado por ID</returns>
        [HttpGet("obtenerProductoPorId/{idProducto}")]
        public async Task<ActionResult<Producto>> ObtenerProductoPorIdAsync(int idProducto)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerProductoPorId: INICIO");
            _logger.LogDebug("idProducto={idProducto}", idProducto);
            try
            {
                var resultado = await _bsProducto.ObtenerProductoPorIdAsync(idProducto);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerProductoPorId: FIN");
            return respuesta;
        }

        /// <summary>
        /// Obtiene Producto por SKU
        /// </summary>
        /// <param name="sku">SKU del Producto</param>
        /// <returns>Devuelve el objeto Producto seleccionado por SKU</returns>
        [HttpGet("obtenerProductoPorSKU/{sku}")]
        public async Task<ActionResult<Producto>> ObtenerProductoPorSKUAsync(string sku)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerProductoPorSKU: INICIO");
            _logger.LogDebug("sku={sku}", sku);
            try
            {
                var resultado = await _bsProducto.ObtenerProductoPorSKUAsync(sku);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerProductoPorSKU: FIN");
            return respuesta;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Producto
        /// </summary>
        /// <param name="Producto">Objeto de tipo Producto con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarProductoI/{idProducto}")]
        public async Task<ActionResult> EditarProductoI(int idProducto, [FromBody]Producto producto)
        {
            ObjectResult result;
            long resultado;
            try
            {
                producto.IdProducto = idProducto;
                resultado = await _bsProducto.EditarProductoJsonAsync(producto);
                result = Ok(resultado >= 1 ? "Registro exitoso " : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                resultado = 0;
                result = StatusCode(StatusCodes.Status500InternalServerError, resultado);
                _logger.LogInformation($"Ha ocurrido un error: {ex.Message.ToString()}");
            }
            return result;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Producto
        /// </summary>
        /// <param name="Producto">Objeto de tipo Producto con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarProducto")]
        public async Task<ActionResult> EditarProducto([FromBody]Producto producto)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsProducto.EditarProductoJsonAsync(producto);
                result = Ok(resultado >= 1 ? "Registro exitoso " : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                resultado = 0;
                result = StatusCode(StatusCodes.Status500InternalServerError, resultado);
                _logger.LogInformation($"Ha ocurrido un error: {ex.Message.ToString()}");
            }
            return result;
        }

        /// <summary>
        /// Realiza una baja lógica del Producto
        /// </summary>
        /// <param name="idProducto"/>Id del Producto a eliminar
        /// <returns>Mensaje "Borrado exitoso" si la baja lógica se realizó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpGet("eliminarProducto/{idProducto}")]
        public async Task<ActionResult<bool>> EliminarProductoAsync(int idProducto)
        {
            ObjectResult respuesta;
            _logger.LogInformation("EliminarProducto: INICIO");
            _logger.LogDebug("idProducto={idProducto}", idProducto);
            try
            {
                var resultado = await _bsProducto.EliminarProductoAsync(idProducto);
                respuesta = Ok(resultado == 1 ? "Borrado exitoso" : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("EliminarProducto: FIN");
            return respuesta;
        }
    }
}