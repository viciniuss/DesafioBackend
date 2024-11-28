using Amazon.S3;
using Amazon.Runtime;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using DesafioBackend.Core.Interfaces;

public class S3StorageService 
{
    private readonly IAmazonS3 _s3Client;

    public S3StorageService()
    {
        // Configurando a região para São Paulo
        var region = Amazon.RegionEndpoint.SAEast1; // Região São Paulo
        _s3Client = new AmazonS3Client(region);  // Instanciando o cliente S3
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var bucketName = "desafio-backend-s3"; // Substitua com o nome do seu bucket

        using (var stream = file.OpenReadStream())
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
                InputStream = stream,
                ContentType = file.ContentType
            };

            var response = await _s3Client.PutObjectAsync(putRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                // Retorna a URL pública do arquivo no S3
                return $"https://{bucketName}.s3.amazonaws.com/{fileName}";
            }
        }

        return null;  // Caso o upload falhe
    }
}
