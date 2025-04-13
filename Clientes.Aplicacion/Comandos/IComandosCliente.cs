using Clientes.Aplicacion.Dto;

namespace Clientes.Aplicacion.Comandos
{
    public interface IComandosCliente
    {
        Task<BaseOut> CrearCliente(ClienteIn cliente);
    }
}
