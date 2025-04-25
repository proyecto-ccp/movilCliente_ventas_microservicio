
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Puertos.Repositorios;

namespace Clientes.Dominio.Servicios
{
    public class ObtenerCliente(IClienteRepositorio clienteRepositorio)
    {
        private readonly IClienteRepositorio _clienteRepositorio = clienteRepositorio;

        public async Task<Cliente> ObtenerClientePorId(Guid id)
        {
            var cliente= await _clienteRepositorio.ObtenerClientePorId(id);

            if(cliente == null)
            {
                throw new Exception("Cliente no encontrado");
            }
            else
            {
                return cliente;
            }
        }
    }
}
