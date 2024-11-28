using Amazon.S3;
using DesafioBackend.Application;
using DesafioBackend.Application.Services;
using DesafioBackend.Core.Interfaces;
using DesafioBackend.Infrastructure;
using DesafioBackend.Infrastructure.Repositories;
using DesafioBackend.Infrastructure.Config;
using DesafioBackend.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Configurações do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "DesafioBackend API",
        Version = "v1",
        Description = "API para o gerenciamento de aluguel de motos e entregadores.",
    });
});

// Configurações do MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<S3StorageService>();
builder.Services.AddScoped<EntregadorService>();


builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IEntregadorRepository, EntregadorRepository>();

// Registro de serviços da camada Application
builder.Services.AddApplication(); // Certifique-se de ter o método de extensão `AddApplication`

// Registro da infraestrutura
builder.Services.AddInfrastructure(builder.Configuration);

// Adicionar controladores e serviços essenciais
builder.Services.AddControllers(); // Adiciona suporte para controladores MVC/API
builder.Services.AddAuthorization(); // Adiciona suporte para autorização
builder.Services.AddAuthentication(); // Opcional: Adicione se necessário para autenticação

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioBackend API V1");
        c.RoutePrefix = string.Empty; // Torna o Swagger acessível na raiz
    });
}

app.UseHttpsRedirection(); // Força o uso de HTTPS
app.UseAuthorization();    // Garante suporte à autorização (caso necessário)

// Mapeamento de rotas e controladores
app.MapControllers();

app.Run();
