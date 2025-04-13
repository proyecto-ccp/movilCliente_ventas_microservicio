using Clientes.Dominio.Entidades;

namespace Clientes.Dominio.Puertos.Repositorios
{
    public interface IClienteRepositorio
    {
        Task CrearCliente(Cliente cliente);
        Task ActualizarCliente(Cliente cliente);
        Task EliminarCliente(Guid id);
        Task<Cliente> ObtenerClientePorId(Guid id);
        Task<List<Cliente>> ObtenerClientes();
        Task<bool> ExisteClientePorDocumento(string documento);
        Task<bool> ExisteClientePorEmail(string email);
    }
}
