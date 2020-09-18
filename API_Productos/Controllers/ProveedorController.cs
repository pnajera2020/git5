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
    public class ProveedorController : ControllerBase
    {
        private readonly ILogger<ProveedorController> _logger = null;
        private readonly IBsProveedor _bsProveedor = null;

        public ProveedorController(ILogger<ProveedorController> logger, IBsProveedor bsProveedor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bsProveedor = bsProveedor ?? throw new ArgumentNullException(nameof(bsProveedor));
        }

        /// <summary>
        /// Inserta un registro de Proveedor en base de datos
        /// </summary>
        /// <param name="proveedor">Objeto de tipo Proveedor con la información ingresada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPost("agregarProveedor")]
        public async Task<ActionResult> AgregarProveedor([FromBody]Proveedor proveedor)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsProveedor.AgregaProveedorJsonAsync(proveedor);
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
        /// Obtiene todos los registros de Proveedor activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Proveedor</returns>
        [HttpGet("obtenerProveedores")]
        public async Task<ActionResult<IList<Proveedor>>> ObtenerProveedoresAsync()
        {
            ObjectResult valRet;
            _logger.LogInformation("ObtenerProveedoresAsync: INICIO");

            IList<Proveedor> listaProveedores = null;
            try
            {
                listaProveedores = await _bsProveedor.ObtenerProveedoresAsync();
                valRet = Ok(listaProveedores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                valRet = StatusCode(StatusCodes.Status500InternalServerError, null);
            }
            _logger.LogInformation("ObtenerProveedoresAsync: FIN");
            return valRet;
        }

        /// <summary>
        /// Obtiene Proveedor por Id
        /// </summary>
        /// <param name="idProveedor">Identificador del Proveedor</param>
        /// <returns>Devuelve el objeto Proveedor seleccionado por ID</returns>
        [HttpGet("obtenerProveedorPorId/{idProveedor}")]
        public async Task<ActionResult<Proveedor>> ObtenerProveedorPorIdAsync(int idProveedor)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerProveedorPorId: INICIO");
            _logger.LogDebug("idProveedor={idProveedor}", idProveedor);
            try
            {
                var resultado = await _bsProveedor.ObtenerProveedorPorIdAsync(idProveedor);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerProveedorPorId: FIN");
            return respuesta;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Proveedor
        /// </summary>
        /// <param name="proveedor">Objeto de tipo Proveedor con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarProveedorI/{idProveedor}")]
        public async Task<ActionResult> EditarProveedorI(int idProveedor, [FromBody]Proveedor proveedor)
        {
            ObjectResult result;
            long resultado;
            try
            {
                proveedor.IdProveedor = idProveedor;
                resultado = await _bsProveedor.EditarProveedorJsonAsync(proveedor);
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
        /// Realiza la actualización de datos de un registro de Proveedor
        /// </summary>
        /// <param name="proveedor">Objeto de tipo Proveedor con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarProveedor")]
        public async Task<ActionResult> EditarProveedor([FromBody]Proveedor proveedor)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsProveedor.EditarProveedorJsonAsync(proveedor);
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
        /// Realiza una baja lógica del Proveedor
        /// </summary>
        /// <param name="idProveedor"/>Id del Proveedor a eliminar
        /// <returns>Mensaje "Borrado exitoso" si la baja lógica se realizó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpGet("eliminarProveedor/{idProveedor}")]
        public async Task<ActionResult<bool>> EliminarProveedorAsync(int idProveedor)
        {
            ObjectResult respuesta;
            _logger.LogInformation("EliminarProveedor: INICIO");
            _logger.LogDebug("idProveedor={idProveedor}", idProveedor);
            try
            {
                var resultado = await _bsProveedor.EliminarProveedorAsync(idProveedor);
                respuesta = Ok(resultado == 1 ? "Borrado exitoso" : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("EliminarProveedor: FIN");
            return respuesta;
        }
    }
}