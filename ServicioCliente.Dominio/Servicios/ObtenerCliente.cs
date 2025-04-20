
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Puertos.Repositorios;

namespace Clientes.Dominio.Servicios
{
    public class ObtenerCliente(IClienteRepositorio _clienteRepositorio)
    {
        private readonly IClienteRepositorio clienteRepositorio = _clienteRepositorio;

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
