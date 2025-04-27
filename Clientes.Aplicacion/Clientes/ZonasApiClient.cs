using Clientes.Aplicacion.Dto;
using System.Text.Json;

namespace Clientes.Infraestructura.ZonasApiClient
{
    public interface IZonasApiClient
    {
        Task<ZonaDto> ObtenerZonaPorIdAsync(Guid idZona);
    }
    public class ZonasApiClient : IZonasApiClient
    {
        private readonly HttpClient _httpClient;
        public ZonasApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ZonaDto> ObtenerZonaPorIdAsync(Guid idZona)
        {
            var response = await _httpClient.GetAsync($"Zona/ObtenerZona/{idZona}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("No se pudo obtener la zona");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var zonaResponse = JsonSerializer.Deserialize<ZonaResponseDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return zonaResponse.Zona;
        }
    
    
    }
}
