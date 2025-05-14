using Clientes.Aplicacion.Comandos;
using Clientes.Aplicacion.Consultas;
using Clientes.Aplicacion.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ServicioCliente.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ClienteController : ControllerBase
    {
        private readonly IComandosCliente _comandosCliente;
        private readonly IConsultasCliente _consultasCliente;
        public ClienteController(IComandosCliente comandosCliente, IConsultasCliente consultasCliente)
        {
            _comandosCliente = comandosCliente;
            _consultasCliente = consultasCliente;
        }

        [HttpPost]
        [Route("CrearCliente")]
        [ProducesResponseType(typeof(ClienteOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteIn ClienteIn)
        {
            try
            {
                var resultado = await _comandosCliente.CrearCliente(ClienteIn);

                if (resultado.Resultado != Clientes.Aplicacion.Enum.Resultado.Error)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: HttpContext.Request.Path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerCliente/{id}")]
        [ProducesResponseType(typeof(ClienteOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerCliente(Guid id)
        {
            try
            {
                var resultado = await _consultasCliente.ObtenerClientePorId(id);
                if (resultado.Resultado == Clientes.Aplicacion.Enum.Resultado.Exitoso)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: (HttpContext != null ? HttpContext.Request.Path : null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerClientes")]
        [ProducesResponseType(typeof(ClienteOutList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerClientes()
        {
            try
            {
                var resultado = await _consultasCliente.ObtenerClientes();
                if (resultado.Resultado != Clientes.Aplicacion.Enum.Resultado.Error)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: HttpContext.Request.Path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerClientesPorZona/{idZona}")]
        [ProducesResponseType(typeof(ClienteOutList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerClientesPorZona(Guid idZona)
        {
            try
            {
                var resultado = await _consultasCliente.ObtenerClientesPorZona(idZona);
                if (resultado.Resultado != Clientes.Aplicacion.Enum.Resultado.Error)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: HttpContext.Request.Path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
