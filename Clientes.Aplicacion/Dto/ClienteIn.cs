namespace Clientes.Aplicacion.Dto
{
    public class ClienteIn
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public Guid IdZona { get; set; }
    }
}
