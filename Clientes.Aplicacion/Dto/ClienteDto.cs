using System.ComponentModel.DataAnnotations.Schema;

namespace Clientes.Aplicacion.Dto
{
    public class ClienteDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Guid? IdZona { get; set; }
    }

    public class ClienteOut : BaseOut 
    {
        public ClienteDto Cliente { get; set; } 
    }

    public class ClienteOutList: BaseOut
    {
        public List<ClienteDto> Clientes { get; set; }
    }
}
