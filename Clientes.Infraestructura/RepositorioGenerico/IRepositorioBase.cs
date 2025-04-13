using Clientes.Dominio.Entidades;

namespace Clientes.Infraestructura.RepositorioGenerico
{
    public interface IRepositorioBase<T> : IDisposable where T : EntidadBase
    {
        Task<T> Crear(T entity);
        Task<T> BuscarPorLlave(object ValueKey);
        Task<List<T>> BuscarPorAtributo(string ValueAttribute, string Attribute);
        Task<List<T>> DarListado();
    }
}
