using Clientes.Dominio.Entidades;
using Clientes.Dominio.Puertos.Repositorios;

namespace Clientes.Dominio.Servicios
{
    public class ListadoClientes(IClienteRepositorio clienteRepositorio)
    {
        private readonly IClienteRepositorio _clienteRepositorio = clienteRepositorio;

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _clienteRepositorio.ObtenerClientes();
        }
    }
}
