using Clientes.Aplicacion.Comandos;
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
        public ClienteController(IComandosCliente comandosCliente)
        {
            _comandosCliente = comandosCliente;
        }

        [HttpPost]
        [Route("CargarCliente")]
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
    }
}
