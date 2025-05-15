using System.Net;
using Clientes.Aplicacion.Dto;
using Clientes.Aplicacion.Enum;
using Clientes.Dominio.Servicios;
using AutoMapper;
using Clientes.Dominio.Puertos.Repositorios;
using Clientes.Dominio.Entidades;
using Clientes.Aplicacion.Clientes;

namespace Clientes.Aplicacion.Comandos
{
    public class ManejadorComandos : IComandosCliente
    {
        private readonly CrearCliente _crearCliente;
        private readonly IUsuariosApiClient _usuariosApiClient;
        private readonly IMapper _mapper;

        public ManejadorComandos(IClienteRepositorio clienteRepositorio, IUsuariosApiClient usuariosApiClient ,IMapper mapper)
        {
            _crearCliente = new CrearCliente(clienteRepositorio);
            _usuariosApiClient = usuariosApiClient;
            _mapper = mapper;
        }

        public async Task<BaseOut> CrearCliente(ClienteIn cliente)
        {
            BaseOut baseOut = new();
            try
            {
                var clienteDominio = _mapper.Map<Cliente>(cliente);
                await _crearCliente.Ejecutar(clienteDominio);

                //crear usuario
                var usuario = new UsuarioDto
                {
                    UserName = clienteDominio.Email,
                    Nombres = clienteDominio.Nombre,
                    Apellidos = clienteDominio.Apellido,
                    Correo = clienteDominio.Email,
                    Contrasena = cliente.Contrasenia,
                    Telefono = clienteDominio.Telefono,
                };

                var usuarioResponse = await _usuariosApiClient.CrearUsuarioAsync(usuario);

                if (usuarioResponse == null || usuarioResponse.IdUsuario == Guid.Empty)
                {
                    baseOut.Resultado = Resultado.Error;
                    baseOut.Mensaje = "Error al crear el usuario";
                    baseOut.Status = HttpStatusCode.InternalServerError;
                    return baseOut;
                }

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
