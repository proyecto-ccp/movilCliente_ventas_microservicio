using AutoMapper;
using Clientes.Aplicacion.Dto;
using Clientes.Aplicacion.Enum;
using Clientes.Dominio.Puertos.Repositorios;
using Clientes.Dominio.Servicios;
using System.Net;

namespace Clientes.Aplicacion.Consultas
{
    public class ManejadorConsultas: IConsultasCliente
    {
        private readonly ObtenerCliente _obtenerCliente;
        private readonly ListadoClientes _listadoClientes;
        private readonly IMapper _mapper;

        public ManejadorConsultas(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            _obtenerCliente = new ObtenerCliente(clienteRepositorio);
            _listadoClientes = new ListadoClientes(clienteRepositorio);
            _mapper = mapper;
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
    
    }
}
