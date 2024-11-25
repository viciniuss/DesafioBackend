var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configuração de título, descrição e versão da API no Swagger
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "DesafioBackend API",
        Version = "v1",
        Description = "API para o projeto DesafioBackend",
    });
});

// Adicionar suporte ao redirecionamento de HTTP para HTTPS
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7208; // Porta HTTPS configurada no launchSettings.json
});

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    // Habilita o Swagger apenas no ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioBackend API V1");
        c.RoutePrefix = string.Empty; // Tornar o Swagger disponível na raiz
    });
}

// Redirecionamento de HTTP para HTTPS
app.UseHttpsRedirection();

// Mapear endpoints da API
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

// Definição do record WeatherForecast
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
