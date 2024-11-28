using Microsoft.Extensions.DependencyInjection;
using DesafioBackend.Application.Services; 
using DesafioBackend.Core.Interfaces;
using DesafioBackend.Infrastructure.Repositories; 

namespace DesafioBackend.Application
{
    public static class ApplicationExtensions // Altere para static
    {
        /// <summary>
        /// Método de extensão para configurar os serviços da camada Application.
        /// </summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registra os serviços de aplicação
            services.AddScoped<MotoService>();     // Serviço responsável pelas operações de Moto
            services.AddScoped<EntregadorService>();  
            services.AddScoped<S3StorageService>();
            // services.AddScoped<LocacaoService>(); // Serviço responsável pelas operações de Locação
            
            return services;
        }
    }
}
