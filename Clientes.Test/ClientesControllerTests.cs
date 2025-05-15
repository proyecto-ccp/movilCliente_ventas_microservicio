using Clientes.Aplicacion.Comandos;
using Clientes.Aplicacion.Consultas;
using Clientes.Aplicacion.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using ServicioCliente.Controllers;
using System.Net;
using System.Text;
using System.Text.Json;
using Bogus;

namespace Clientes.Test
{
    public class ClientesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        Guid zoneId = Guid.Parse("11e86372-1b67-4d4b-b234-53f716dab601");
        public ClientesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ObtenerPorId_DevuelveOkSiExiste()
        {
            //Arrange
            var mockService = new Mock<IConsultasCliente>();
            var clienteId = Guid.NewGuid();

            // Update the mock setup to match the expected return type of the method
            var clienteEjemplo = new ClienteOut
            {
                Cliente = new ClienteDto
                {
                    Id = clienteId,
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    TipoDocumento = "CC",
                    Documento = "123987456",
                    Direccion = "Cra 124C # 162F - 16",
                    Telefono = "311254789",
                    Email = "mail@mail.com"
                },
                Resultado = Clientes.Aplicacion.Enum.Resultado.Exitoso,
                Mensaje = "Cliente encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            mockService.Setup(x => x.ObtenerClientePorId(clienteId)).ReturnsAsync(clienteEjemplo);

            var controller = new ClienteController(null, mockService.Object);

            //Act
            var result = await controller.ObtenerCliente(clienteId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var clienteResult = Assert.IsType<ClienteOut>(okResult.Value);
            Assert.Equal(clienteId, clienteResult.Cliente.Id);

        }

        [Fact]
        public async Task ObtenerPorId_DevuelveNotFoundSiNoExiste()
        {
            //Arrange
            var mockService = new Mock<IConsultasCliente>();
            var clienteId = Guid.NewGuid();
            var clienteEjemplo = new ClienteOut
            {
                Cliente = null,
                Resultado = Clientes.Aplicacion.Enum.Resultado.SinRegistros,
                Mensaje = "Cliente no encontrado",
                Status = System.Net.HttpStatusCode.NotFound
            };
            mockService.Setup(x => x.ObtenerClientePorId(clienteId)).ReturnsAsync(clienteEjemplo);
            var controller = new ClienteController(null, mockService.Object);
            //Act
            var result = await controller.ObtenerCliente(clienteId);
            var objectResult = Assert.IsType<ObjectResult>(result);
            var clienteResult = Assert.IsType<ProblemDetails>(objectResult.Value);

            //Assert
            Assert.Equal(clienteResult.Status, (int)System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ObtenerClientes_Ok()
        {
            //Arrange
            var mockService = new Mock<IConsultasCliente>();

            var clienteId = Guid.NewGuid();
            var cliente1 = new ClienteDto
            {
                Id = clienteId,
                Nombre = "Juan",
                Apellido = "Pérez",
                TipoDocumento = "CC",
                Documento = "123987456",
                Direccion = "Cra 124C # 162F - 16",
                Telefono = "311254789",
                Email = "mail@mail.com"
            };

            var cliente2 = new ClienteDto
            {
                Id = clienteId,
                Nombre = "Juan",
                Apellido = "Pérez",
                TipoDocumento = "CC",
                Documento = "123987456",
                Direccion = "Cra 124C # 162F - 16",
                Telefono = "311254789",
                Email = "mail@mail.com"
            };

            var clienteList = new ClienteOutList();
            clienteList.Clientes = new List<ClienteDto> { cliente1, cliente2 };
            clienteList.Resultado = Clientes.Aplicacion.Enum.Resultado.Exitoso;
            clienteList.Mensaje = "Clientes encontrados";
            clienteList.Status = System.Net.HttpStatusCode.OK;

            mockService.Setup(x => x.ObtenerClientes()).ReturnsAsync(clienteList);
            var controller = new ClienteController(null, mockService.Object);

            //Act
            var result = await controller.ObtenerClientes();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var clienteResult = Assert.IsType<ClienteOutList>(okResult.Value);
            Assert.Equal(clienteList.Clientes.Count, clienteResult.Clientes.Count);
        }

        [Fact]
        public async Task crearCliente_DevuelveCreaterConResultado()
        {
            //Arrange
            var mockService = new Mock<IComandosCliente>();
            var clienteIn = new ClienteIn
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                TipoDocumento = "CC",
                Documento = "123987456",
                Direccion = "Cra 124C # 162F - 16",
                Telefono = "311254789",
                Email = "mail@mail.com"
            };

            var clienteId = Guid.NewGuid();
            var clienteResult = new ClienteOut
            {
                Cliente = new ClienteDto
                {
                    Id = clienteId,
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    TipoDocumento = "CC",
                    Documento = "123987456",
                    Direccion = "Cra 124C # 162F - 16",
                    Telefono = "311254789",
                    Email = "mail@mail.com"
                },
                Resultado = Clientes.Aplicacion.Enum.Resultado.Exitoso,
                Mensaje = "Cliente creado exitosamente",
                Status = System.Net.HttpStatusCode.OK
            };

            mockService.Setup(x => x.CrearCliente(clienteIn)).ReturnsAsync(clienteResult);
            var controller = new ClienteController(mockService.Object, null);

            //Act
            var resultado = await controller.CrearCliente(clienteIn);

            //Assert
            Assert.NotNull(resultado);

            var createdResult = Assert.IsType<OkObjectResult>(resultado);
            var clientedevuelto = Assert.IsType<ClienteOut>(createdResult.Value);
        }

        [Fact]
        public async Task PostCliente_Created()
        {
            var faker = new Faker();

            var clienteIn = new ClienteIn
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                TipoDocumento = "CC",
                Documento = faker.Database.Random.Number(123456789, 987654321).ToString(),
                Direccion = "Cra 124C # 162F - 16",
                Telefono = "311254789",
                Email = faker.Internet.Email(),
                Contrasenia = faker.Internet.Password(),
                IdZona = zoneId,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(clienteIn),
                Encoding.UTF8,
                "application/json"
            );

            //Act
            var response = await _client.PostAsync("/api/Cliente/CrearCliente", content);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var clienteResult = JsonSerializer.Deserialize<ClienteOut>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(clienteResult);
            Assert.Equal(Clientes.Aplicacion.Enum.Resultado.Exitoso, clienteResult.Resultado);
            Assert.Equal(HttpStatusCode.Created, clienteResult.Status);
        }

        [Fact]
        public async Task getAllClientes()
        {
            // Arrange
            var response = await _client.GetAsync("/api/Cliente/ObtenerClientes");
            // Act
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var clienteResult = JsonSerializer.Deserialize<ClienteOutList>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            // Assert
            Assert.NotNull(clienteResult);
            Assert.Equal(Clientes.Aplicacion.Enum.Resultado.Exitoso, clienteResult.Resultado);
        }

        [Fact]
        public async Task getClienteById()
        {
            // Arrange
            var clienteId = Guid.Parse("5ba9f1b7-ec06-4af0-8f84-25b039d95101");
            var response = await _client.GetAsync($"/api/Cliente/ObtenerCliente/{clienteId}");
            // Act
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var clienteResult = JsonSerializer.Deserialize<ClienteOut>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            // Assert
            Assert.NotNull(clienteResult);
            Assert.Equal(Clientes.Aplicacion.Enum.Resultado.Exitoso, clienteResult.Resultado);
        }

        [Fact]
        public async Task getClienteByIdNotFound()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/Cliente/ObtenerCliente/{clienteId}");
            
            // Act
            var responseString = await response.Content.ReadAsStringAsync();
            var clienteResult = JsonSerializer.Deserialize<ClienteOut>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            // Assert
            Assert.NotNull(clienteResult);
            Assert.Equal(HttpStatusCode.NotFound, clienteResult.Status);

        }

        [Fact]
        public async Task getClienteByZona()
        {
            // Arrange
            var response = await _client.GetAsync($"/api/Cliente/ObtenerClientesPorZona/{zoneId}");
            // Act
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var clienteResult = JsonSerializer.Deserialize<ClienteOut>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            // Assert
            Assert.NotNull(clienteResult);
            Assert.Equal(Clientes.Aplicacion.Enum.Resultado.Exitoso, clienteResult.Resultado);
        }
    }
}
