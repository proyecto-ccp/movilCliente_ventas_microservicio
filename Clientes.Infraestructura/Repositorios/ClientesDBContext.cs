using Microsoft.EntityFrameworkCore;
using Clientes.Dominio.Entidades;

namespace Clientes.Infraestructura.Repositorios
{
    public class ClientesDBContext : DbContext
    {
        public ClientesDBContext(DbContextOptions<ClientesDBContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        
    }
}
