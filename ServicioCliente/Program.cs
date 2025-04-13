using Clientes.Aplicacion.Comandos;
using Clientes.Dominio.Puertos.Repositorios;
using Clientes.Infraestructura.Repositorios;
using Clientes.Infraestructura.RepositorioGenerico;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ClientesDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ClientesDbContext")), ServiceLifetime.Transient);
builder.Services.AddTransient(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
builder.Services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IComandosCliente, ManejadorComandos>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
