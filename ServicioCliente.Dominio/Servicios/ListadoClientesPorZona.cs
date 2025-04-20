using Clientes.Dominio.Entidades;
using Clientes.Dominio.Puertos.Repositorios;

namespace Clientes.Dominio.Servicios
{
    public class ListadoClientesPorZona(IClienteRepositorio clienteRepositorio)
    {
        private readonly IClienteRepositorio _clienteRepositorio = clienteRepositorio;

        public async Task<List<Cliente>> ObtenerClientesPorZona(Guid idZona)
        {
            return await _clienteRepositorio.ObtenerClientesPorZona(idZona);
        }
    }
}
