
using Microsoft.Extensions.DependencyInjection;
using DesafioBackend.Application.Services; 

namespace DesafioBackend.Application
{
    public static class ApplicationExtensions 
    {
        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddApplication(IServiceCollection services)
        {
 
            services.AddScoped<MotoService>();    
            services.AddScoped<EntregadorService>(); 
            services.AddScoped<LocacaoService>();         
            return services;
        }
    }
}
