using Clientes.Aplicacion.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace Clientes.Aplicacion.Clientes
{
    public interface IUsuariosApiClient
    {
        Task<UsuarioResponseDto> CrearUsuarioAsync(UsuarioDto usuario);
    }
    public class UsuariosApiClient: IUsuariosApiClient
    {
        private readonly HttpClient _httpClient;
        public UsuariosApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UsuarioResponseDto> CrearUsuarioAsync(UsuarioDto usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuarios/Crear", usuario);
            
            if (!response.IsSuccessStatusCode)
                throw new Exception("No se pudo crear el usuario");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            
            var usuarioResponse = JsonSerializer.Deserialize<UsuarioResponseDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return usuarioResponse;
        }
    }
}
