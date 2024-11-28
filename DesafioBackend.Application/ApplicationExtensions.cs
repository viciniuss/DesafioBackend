using Microsoft.Extensions.DependencyInjection;
using DesafioBackend.Application.Services; 
using DesafioBackend.Core.Interfaces;
using DesafioBackend.Infrastructure.Repositories; 

namespace DesafioBackend.Application
{
    public static class ApplicationExtensions 
    {
        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
 
            services.AddScoped<MotoService>();    
            services.AddScoped<EntregadorService>();  
            services.AddScoped<S3StorageService>();
           
            return services;
        }
    }
}
