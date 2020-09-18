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
    public class MarcaController : ControllerBase
    {
        private readonly ILogger<MarcaController> _logger = null;
        private readonly IBsMarca _bsMarca = null;

        public MarcaController(ILogger<MarcaController> logger, IBsMarca bsMarca)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bsMarca = bsMarca ?? throw new ArgumentNullException(nameof(bsMarca));
        }

        /// <summary>
        /// Inserta un registro de Marca en base de datos
        /// </summary>
        /// <param name="Marca">Objeto de tipo Marca con la información ingresada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPost("agregarMarca")]
        public async Task<ActionResult> AgregarMarca([FromBody]Marca marca)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsMarca.AgregaMarcaJsonAsync(marca);
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
        /// Obtiene todos los registros de Marca activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Marca</returns>
        [HttpGet("obtenerMarcas")]
        public async Task<ActionResult<IList<Marca>>> ObtenerMarcasAsync()
        {
            ObjectResult valRet;
            _logger.LogInformation("ObtenerMarcasAsync: INICIO");

            IList<Marca> listaMarcaes = null;
            try
            {
                listaMarcaes = await _bsMarca.ObtenerMarcasAsync();
                valRet = Ok(listaMarcaes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                valRet = StatusCode(StatusCodes.Status500InternalServerError, null);
            }
            _logger.LogInformation("ObtenerMarcasAsync: FIN");
            return valRet;
        }

        /// <summary>
        /// Obtiene Marca por Id
        /// </summary>
        /// <param name="idMarca">Identificador del Marca</param>
        /// <returns>Devuelve el objeto Marca seleccionado por ID</returns>
        [HttpGet("obtenerMarcaPorId/{idMarca}")]
        public async Task<ActionResult<Marca>> ObtenerMarcaPorIdAsync(int idMarca)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerMarcaPorId: INICIO");
            _logger.LogDebug("idMarca={idMarca}", idMarca);
            try
            {
                var resultado = await _bsMarca.ObtenerMarcaPorIdAsync(idMarca);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerMarcaPorId: FIN");
            return respuesta;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Marca
        /// </summary>
        /// <param name="Marca">Objeto de tipo Marca con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarMarcaI/{idMarca}")]
        public async Task<ActionResult> EditarMarcaI(int idMarca, [FromBody]Marca marca)
        {
            ObjectResult result;
            long resultado;
            try
            {
                marca.IdMarca = idMarca;
                resultado = await _bsMarca.EditarMarcaJsonAsync(marca);
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
        /// Realiza la actualización de datos de un registro de Marca
        /// </summary>
        /// <param name="Marca">Objeto de tipo Marca con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarMarca")]
        public async Task<ActionResult> EditarMarca([FromBody]Marca marca)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsMarca.EditarMarcaJsonAsync(marca);
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
        /// Realiza una baja lógica del Marca
        /// </summary>
        /// <param name="idMarca"/>Id del Marca a eliminar
        /// <returns>Mensaje "Borrado exitoso" si la baja lógica se realizó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpGet("eliminarMarca/{idMarca}")]
        public async Task<ActionResult<bool>> EliminarMarcaAsync(int idMarca)
        {
            ObjectResult respuesta;
            _logger.LogInformation("EliminarMarca: INICIO");
            _logger.LogDebug("idMarca={idMarca}", idMarca);
            try
            {
                var resultado = await _bsMarca.EliminarMarcaAsync(idMarca);
                respuesta = Ok(resultado == 1 ? "Borrado exitoso" : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("EliminarMarca: FIN");
            return respuesta;
        }
    }
}