namespace Clientes.Aplicacion.Dto
{
    public class ZonaDto
    {
        public Guid Id { get; set; }
        public Guid IdCiudad { get; set; }
        public string? Ciudad { get; set; }
        public string Nombre { get; set; }
        public string Limites { get; set; }
    }

    public class ZonaResponseDto
    {
        public ZonaDto Zona { get; set; }
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public int Status { get; set; }
    }
}
