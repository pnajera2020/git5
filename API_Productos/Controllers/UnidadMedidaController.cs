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
    public class UnidadMedidaController : ControllerBase
    {
        private readonly ILogger<UnidadMedidaController> _logger = null;
        private readonly IBsUnidadMedida _bsUnidadMedida = null;

        public UnidadMedidaController(ILogger<UnidadMedidaController> logger, IBsUnidadMedida bsUnidadMedida)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bsUnidadMedida = bsUnidadMedida ?? throw new ArgumentNullException(nameof(bsUnidadMedida));
        }

        /// <summary>
        /// Inserta un registro de UnidadMedida en base de datos
        /// </summary>
        /// <param name="UnidadMedida">Objeto de tipo UnidadMedida con la información ingresada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPost("agregarUnidadMedida")]
        public async Task<ActionResult> AgregarUnidadMedida([FromBody]UnidadMedida UnidadMedida)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsUnidadMedida.AgregaUnidadMedidaJsonAsync(UnidadMedida);
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
        /// Obtiene todos los registros de UnidadMedida activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo UnidadMedida</returns>
        [HttpGet("obtenerUnidadesMedida")]
        public async Task<ActionResult<IList<UnidadMedida>>> ObtenerUnidadesMedidaAsync()
        {
            ObjectResult valRet;
            _logger.LogInformation("ObtenerUnidadesMedidaAsync: INICIO");

            IList<UnidadMedida> listaUnidadMedidaes = null;
            try
            {
                listaUnidadMedidaes = await _bsUnidadMedida.ObtenerUnidadesMedidaAsync();
                valRet = Ok(listaUnidadMedidaes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                valRet = StatusCode(StatusCodes.Status500InternalServerError, null);
            }
            _logger.LogInformation("ObtenerUnidadesMedidaAsync: FIN");
            return valRet;
        }

        /// <summary>
        /// Obtiene UnidadMedida por Id
        /// </summary>
        /// <param name="idUnidadMedida">Identificador del UnidadMedida</param>
        /// <returns>Devuelve el objeto UnidadMedida seleccionado por ID</returns>
        [HttpGet("obtenerUnidadMedidaPorId/{idUnidadMedida}")]
        public async Task<ActionResult<UnidadMedida>> ObtenerUnidadMedidaPorIdAsync(int idUnidadMedida)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerUnidadMedidaPorId: INICIO");
            _logger.LogDebug("idUnidadMedida={idUnidadMedida}", idUnidadMedida);
            try
            {
                var resultado = await _bsUnidadMedida.ObtenerUnidadMedidaPorIdAsync(idUnidadMedida);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerUnidadMedidaPorId: FIN");
            return respuesta;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de UnidadMedida
        /// </summary>
        /// <param name="UnidadMedida">Objeto de tipo UnidadMedida con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarUnidadMedidaI/{idUnidadMedida}")]
        public async Task<ActionResult> EditarUnidadMedidaI(int idUnidadMedida, [FromBody]UnidadMedida UnidadMedida)
        {
            ObjectResult result;
            long resultado;
            try
            {
                UnidadMedida.IdUnidadMedida = idUnidadMedida;
                resultado = await _bsUnidadMedida.EditarUnidadMedidaJsonAsync(UnidadMedida);
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
        /// Realiza la actualización de datos de un registro de UnidadMedida
        /// </summary>
        /// <param name="UnidadMedida">Objeto de tipo UnidadMedida con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarUnidadMedida")]
        public async Task<ActionResult> EditarUnidadMedida([FromBody]UnidadMedida UnidadMedida)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsUnidadMedida.EditarUnidadMedidaJsonAsync(UnidadMedida);
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
        /// Realiza una baja lógica del UnidadMedida
        /// </summary>
        /// <param name="idUnidadMedida"/>Id del UnidadMedida a eliminar
        /// <returns>Mensaje "Borrado exitoso" si la baja lógica se realizó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpGet("eliminarUnidadMedida/{idUnidadMedida}")]
        public async Task<ActionResult<bool>> EliminarUnidadMedidaAsync(int idUnidadMedida)
        {
            ObjectResult respuesta;
            _logger.LogInformation("EliminarUnidadMedida: INICIO");
            _logger.LogDebug("idUnidadMedida={idUnidadMedida}", idUnidadMedida);
            try
            {
                var resultado = await _bsUnidadMedida.EliminarUnidadMedidaAsync(idUnidadMedida);
                respuesta = Ok(resultado == 1 ? "Borrado exitoso" : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("EliminarUnidadMedida: FIN");
            return respuesta;
        }
    }
}