using Clientes.Aplicacion.Enum;
using System.Net;

namespace Clientes.Aplicacion.Dto
{
    public class BaseOut
    {
        public Resultado Resultado { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
