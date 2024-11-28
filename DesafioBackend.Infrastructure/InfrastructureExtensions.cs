using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
// using DesafioBackend.Infrastructure.Messaging;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;

namespace DesafioBackend.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // // Configuração do AWS S3
            // services.AddSingleton<IAmazonS3>(sp =>
            // {
            //     var accessKey = configuration.GetValue<string>("AWS:AccessKey");
            //     var secretKey = configuration.GetValue<string>("AWS:SecretKey");
            //     var region = configuration.GetValue<string>("AWS:Region");

            //     var amazonS3Config = new AmazonS3Config
            //     {
            //         RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region) // Região configurada no appsettings
            //     };

            //     var s3Client = new AmazonS3Client(accessKey, secretKey, amazonS3Config);
            //     return s3Client;
            // });

            // services.AddSingleton<S3Service>();

            return services;
        }
    }
}
