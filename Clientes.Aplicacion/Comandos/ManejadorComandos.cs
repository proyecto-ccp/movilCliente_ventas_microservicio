using System.Net;
using Clientes.Aplicacion.Dto;
using Clientes.Aplicacion.Enum;
using Clientes.Dominio.Servicios;
using AutoMapper;
using Clientes.Dominio.Puertos.Repositorios;
using Clientes.Dominio.Entidades;

namespace Clientes.Aplicacion.Comandos
{
    public class ManejadorComandos : IComandosCliente
    {
        private readonly CrearCliente _crearCliente;
        private readonly IMapper _mapper;

        public ManejadorComandos(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            _crearCliente = new CrearCliente(clienteRepositorio);
            _mapper = mapper;
        }

        public async Task<BaseOut> CrearCliente(ClienteIn cliente)
        {
            BaseOut baseOut = new();
            try
            {
                var clienteDominio = _mapper.Map<Cliente>(cliente);
                await _crearCliente.Ejecutar(clienteDominio);
                baseOut.Mensaje = "Cliente creado exitosamente";
                baseOut.Resultado = Resultado.Exitoso; 
                baseOut.Status = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                baseOut.Resultado = Resultado.Error;
                baseOut.Mensaje = ex.Message;
                baseOut.Status = HttpStatusCode.InternalServerError;
            }

            return baseOut;
        }
    }
}
