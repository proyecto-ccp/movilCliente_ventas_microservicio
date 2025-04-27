using Clientes.Aplicacion.Dto;

namespace Clientes.Aplicacion.Consultas
{
    public interface IConsultasCliente
    {
        public Task<ClienteOut> ObtenerClientePorId(Guid id);
        public Task<ClienteOutList> ObtenerClientes();
        public Task<ClienteOutList> ObtenerClientesPorZona(Guid idZona);
    }
}
