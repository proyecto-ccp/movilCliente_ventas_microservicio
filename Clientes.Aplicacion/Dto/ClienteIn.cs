namespace Clientes.Aplicacion.Dto
{
    public class ClienteIn
    {
        public Guid IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
    }
}
