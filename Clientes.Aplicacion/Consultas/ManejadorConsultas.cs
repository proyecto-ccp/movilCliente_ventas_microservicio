using AutoMapper;
using Clientes.Aplicacion.Dto;
using Clientes.Aplicacion.Enum;
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Puertos.Repositorios;
using Clientes.Dominio.Servicios;
using Clientes.Infraestructura.ZonasApiClient;
using System.Net;

namespace Clientes.Aplicacion.Consultas
{
    public class ManejadorConsultas: IConsultasCliente
    {
        private readonly ObtenerCliente _obtenerCliente;
        private readonly ListadoClientes _listadoClientes;
        private readonly ListadoClientesPorZona _listadoClientesPorZona;
        private readonly IMapper _mapper;
        private readonly IZonasApiClient _zonasApiClient;

        public ManejadorConsultas(IClienteRepositorio clienteRepositorio, IMapper mapper, IZonasApiClient zonasApiClient)
        {
            _obtenerCliente = new ObtenerCliente(clienteRepositorio);
            _listadoClientes = new ListadoClientes(clienteRepositorio);
            _listadoClientesPorZona = new ListadoClientesPorZona(clienteRepositorio);
            _mapper = mapper;
            _zonasApiClient = zonasApiClient;
        }

        private void DiligenciarZona(Cliente cliente)
        {
            if (cliente.IdZona != Guid.Empty && cliente.IdZona != null)
            {
                var zona = _zonasApiClient.ObtenerZonaPorIdAsync(cliente.IdZona.Value).Result;
                if (zona != null)
                {
                    cliente.Zona = zona.Nombre;
                    cliente.Ciudad = zona.Ciudad;
                }
            }
        }
        public async Task<ClienteOut> ObtenerClientePorId(Guid id)
        {
            ClienteOut ClienteOut = new();
            try
            {
                var Cliente = await _obtenerCliente.ObtenerClientePorId(id);

                if (Cliente == null || Cliente.Id == Guid.Empty)
                {
                    ClienteOut.Resultado = Resultado.SinRegistros;
                    ClienteOut.Mensaje = "Cliente NO encontrado";
                    ClienteOut.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    DiligenciarZona(Cliente);
                    ClienteOut.Resultado = Resultado.Exitoso;
                    ClienteOut.Mensaje = "Cliente encontrado";
                    ClienteOut.Status = HttpStatusCode.OK;
                    ClienteOut.Cliente = _mapper.Map<ClienteDto>(Cliente);
                }
            }
            catch (Exception ex)
            {
                ClienteOut.Resultado = Resultado.Error;
                ClienteOut.Mensaje = ex.Message;
                ClienteOut.Status = HttpStatusCode.InternalServerError;
            }

            return ClienteOut;
        }
        public async Task<ClienteOutList> ObtenerClientes()
        {
            ClienteOutList output = new()
            {
                Clientes = []
            };

            try
            {
                var Clientes = await _listadoClientes.ObtenerClientes();

                if (Clientes == null || Clientes.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No se encontraron Clientes";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    foreach (var cliente in Clientes)
                    {
                        DiligenciarZona(cliente);
                    }
                    output.Resultado = Resultado.Exitoso;
                    output.Mensaje = "Clientes encontrados";
                    output.Status = HttpStatusCode.OK;
                    output.Clientes = _mapper.Map<List<ClienteDto>>(Clientes);
                }
            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = ex.Message;
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }

        public async Task<ClienteOutList> ObtenerClientesPorZona(Guid idZona)
        {
            ClienteOutList output = new()
            {
                Clientes = []
            };

            try
            {
                var Clientes = await _listadoClientesPorZona.ObtenerClientesPorZona(idZona);

                if (Clientes == null || Clientes.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No se encontraron clientes para la zona";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    foreach (var cliente in Clientes)
                    {
                        DiligenciarZona(cliente);
                    }

                    output.Resultado = Resultado.Exitoso;
                    output.Mensaje = "Clientes encontrados en la zona";
                    output.Status = HttpStatusCode.OK;
                    output.Clientes = _mapper.Map<List<ClienteDto>>(Clientes);
                }
            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = ex.Message;
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }

    }
}
