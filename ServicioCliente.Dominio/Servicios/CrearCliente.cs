using Clientes.Dominio.Puertos.Repositorios;
using Clientes.Dominio.Entidades;   

namespace Clientes.Dominio.Servicios
{
    public class CrearCliente (IClienteRepositorio clienteRepositorio)
    {
        private readonly IClienteRepositorio _clienteRepositorio = clienteRepositorio;
        public async Task<bool> Ejecutar(Cliente cliente)
        {
            await ValidarCliente(cliente);
            
            cliente.Id = Guid.NewGuid();
            cliente.FechaCreacion = DateTime.UtcNow;
            cliente.FechaActualizacion = null;

            await _clienteRepositorio.CrearCliente(cliente);
            return true;
        }

        private async Task ValidarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
            }

            if (await _clienteRepositorio.ExisteClientePorDocumento(cliente.Documento))
            {
                throw new InvalidOperationException("El cliente ya existe con el mismo documento.");
            }
            if (await _clienteRepositorio.ExisteClientePorEmail(cliente.Email))
            {
                throw new InvalidOperationException("El cliente ya existe con el mismo email.");
            }

            if (!string.IsNullOrEmpty(cliente.Nombre)
                && !string.IsNullOrEmpty(cliente.Apellido)
                && !string.IsNullOrEmpty(cliente.TipoDocumento)
                && !string.IsNullOrEmpty(cliente.Documento)
                && !string.IsNullOrEmpty(cliente.Direccion)
                && !string.IsNullOrEmpty(cliente.Telefono)
                && !string.IsNullOrEmpty(cliente.Email))
            {
                throw new InvalidOperationException("Valores incorrectos para los parametros minimos.");
            }
        }
    }
}
