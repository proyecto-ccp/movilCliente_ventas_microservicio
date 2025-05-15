namespace Clientes.Aplicacion.Dto
{
    public class UsuarioDto
    {
        public string UserName { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public Guid IdRol { get; set; } = Guid.Parse("53710214-e144-4dc0-85a1-842ba816d2e0");
    }

    public class UsuarioResponseDto
    {
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public int Status { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdRol { get; set; }

    }
}
