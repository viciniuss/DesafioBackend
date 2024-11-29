
using DesafioBackend.Application.Services;
using DesafioBackend.Core.Interfaces;
using DesafioBackend.Infrastructure;
using DesafioBackend.Infrastructure.Repositories;
using DesafioBackend.Infrastructure.Config;
using DesafioBackend.Infrastructure.Context;
using DesafioBackend.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbContext>();


builder.Services.AddScoped<MotoService>();
builder.Services.AddScoped<EntregadorService>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IEntregadorRepository, EntregadorRepository>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();
builder.Services.AddScoped<ILocacaoService, LocacaoService>();


builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers(); 
builder.Services.AddAuthorization(); 
builder.Services.AddAuthentication(); 
builder.Services.AddSingleton<KafkaProducer>();
builder.Services.AddSingleton<KafkaConsumer>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioBackend API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection(); 
app.UseAuthorization();  

app.MapControllers();

app.Run();
