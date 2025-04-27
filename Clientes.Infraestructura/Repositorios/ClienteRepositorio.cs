using Clientes.Dominio.Puertos.Repositorios;
using Clientes.Dominio.Entidades;
using Clientes.Infraestructura.RepositorioGenerico;

namespace Clientes.Infraestructura.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IRepositorioBase<Cliente> _repositorioBase;
        public ClienteRepositorio(IRepositorioBase<Cliente> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public async Task CrearCliente(Cliente cliente)
        {
            await _repositorioBase.Crear(cliente);
        }

        public async Task<Cliente> ObtenerClientePorId(Guid id)
        {
            return await _repositorioBase.BuscarPorLlave(id);
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _repositorioBase.DarListado();
        }

        public async Task<List<Cliente>> ObtenerClientesPorZona(Guid idZona)
        {
            return await _repositorioBase.BuscarPorAtributo(idZona, "IdZona");
        }

        public async Task<bool> ExisteClientePorDocumento(string documento)
        {
            var existe = await _repositorioBase.BuscarPorAtributo(documento, "Documento");

            if (existe.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ExisteClientePorEmail(string email)
        {
            var existe = await _repositorioBase.BuscarPorAtributo(email, "Email");

            if (existe.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task ActualizarCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Task EliminarCliente(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
