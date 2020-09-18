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
    public class CategoriaController : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger = null;
        private readonly IBsCategoria _bsCategoria = null;

        public CategoriaController(ILogger<CategoriaController> logger, IBsCategoria bsCategoria)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bsCategoria = bsCategoria ?? throw new ArgumentNullException(nameof(bsCategoria));
        }

        /// <summary>
        /// Inserta un registro de Categoria en base de datos
        /// </summary>
        /// <param name="Categoria">Objeto de tipo Categoria con la información ingresada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPost("agregarCategoria")]
        public async Task<ActionResult> AgregarCategoria([FromBody]Categoria categoria)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsCategoria.AgregaCategoriaJsonAsync(categoria);
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
        /// Obtiene todos los registros de Categoria activos
        /// </summary>
        /// <returns>Devuelve una lista de objetos de tipo Categoria</returns>
        [HttpGet("obtenerCategorias")]
        public async Task<ActionResult<IList<Categoria>>> ObtenerCategoriasAsync()
        {
            ObjectResult valRet;
            _logger.LogInformation("ObtenerCategoriasAsync: INICIO");

            IList<Categoria> listaCategoriaes = null;
            try
            {
                listaCategoriaes = await _bsCategoria.ObtenerCategoriasAsync();
                valRet = Ok(listaCategoriaes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                valRet = StatusCode(StatusCodes.Status500InternalServerError, null);
            }
            _logger.LogInformation("ObtenerCategoriasAsync: FIN");
            return valRet;
        }

        /// <summary>
        /// Obtiene Categoria por Id
        /// </summary>
        /// <param name="idCategoria">Identificador del Categoria</param>
        /// <returns>Devuelve el objeto Categoria seleccionado por ID</returns>
        [HttpGet("obtenerCategoriaPorId/{idCategoria}")]
        public async Task<ActionResult<Categoria>> ObtenerCategoriaPorIdAsync(int idCategoria)
        {
            ObjectResult respuesta;
            _logger.LogInformation("ObtenerCategoriaPorId: INICIO");
            _logger.LogDebug("idCategoria={idCategoria}", idCategoria);
            try
            {
                var resultado = await _bsCategoria.ObtenerCategoriaPorIdAsync(idCategoria);
                respuesta = Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("ObtenerCategoriaPorId: FIN");
            return respuesta;
        }

        /// <summary>
        /// Realiza la actualización de datos de un registro de Categoria
        /// </summary>
        /// <param name="Categoria">Objeto de tipo Categoria con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarCategoriaI/{idCategoria}")]
        public async Task<ActionResult> EditarCategoriaI(int idCategoria, [FromBody]Categoria categoria)
        {
            ObjectResult result;
            long resultado;
            try
            {
                categoria.IdCategoria = idCategoria;
                resultado = await _bsCategoria.EditarCategoriaJsonAsync(categoria);
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
        /// Realiza la actualización de datos de un registro de Categoria
        /// </summary>
        /// <param name="Categoria">Objeto de tipo Categoria con la información actualizada</param>
        /// <returns>Mensaje "Registro exitoso" si la información se almacenó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpPut("editarCategoria")]
        public async Task<ActionResult> EditarCategoria([FromBody]Categoria categoria)
        {
            ObjectResult result;
            long resultado;
            try
            {
                resultado = await _bsCategoria.EditarCategoriaJsonAsync(categoria);
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
        /// Realiza una baja lógica del Categoria
        /// </summary>
        /// <param name="idCategoria"/>Id del Categoria a eliminar
        /// <returns>Mensaje "Borrado exitoso" si la baja lógica se realizó correctamente
        /// Mensaje "Intente nuevamente" en caso de algún error</returns>
        [HttpGet("eliminarCategoria/{idCategoria}")]
        public async Task<ActionResult<bool>> EliminarCategoriaAsync(int idCategoria)
        {
            ObjectResult respuesta;
            _logger.LogInformation("EliminarCategoria: INICIO");
            _logger.LogDebug("idCategoria={idCategoria}", idCategoria);
            try
            {
                var resultado = await _bsCategoria.EliminarCategoriaAsync(idCategoria);
                respuesta = Ok(resultado == 1 ? "Borrado exitoso" : "Intente nuevamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            _logger.LogInformation("EliminarCategoria: FIN");
            return respuesta;
        }
    }
}