using Amazon.S3;
using Amazon.S3.Transfer;
using DesafioBackend.Core.Models;
using DesafioBackend.Core.Interfaces;
using DesafioBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace DesafioBackend.Application.Services
{
    public interface IEntregadorService
    {
        Task<string> UploadCNHAsync(string id, IFormFile cnhFile);
    }
    public class EntregadorService : IEntregadorService
    {
        private readonly IEntregadorRepository _repository;
        private readonly IAmazonS3 _storageService;

        public EntregadorService(IEntregadorRepository repository, IAmazonS3 storageService)
        {
            _repository = repository;
            _storageService = storageService;
        }

        public Task<IEnumerable<Entregador>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Entregador> GetByIdAsync(string id)
        {
            return _repository.GetByIdAsync(id);
        }
        public Task CreateAsync(Entregador entregador)
        {
            return _repository.CreateAsync(entregador);
        }

        public async Task<string> UploadCNHAsync(string id, IFormFile cnhFile)
        {
            // Verificar se o arquivo é válido (PNG ou BMP)
            if (cnhFile == null || !(cnhFile.ContentType == "image/png" || cnhFile.ContentType == "image/bmp"))
            {
                throw new Exception("A imagem da CNH deve ser PNG ou BMP.");
            }

            // Enviar o arquivo para o S3
            var filePath = Path.Combine(Path.GetTempPath(), cnhFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cnhFile.CopyToAsync(stream);
            }

            var fileTransferUtility = new TransferUtility(_storageService);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                BucketName = "seu-bucket-s3",
                FilePath = filePath,
                Key = $"cnhs/{id}/{cnhFile.FileName}", 
                ContentType = cnhFile.ContentType
            };

            await fileTransferUtility.UploadAsync(uploadRequest);

            var imageUrl = $"https://seu-bucket-s3.s3.amazonaws.com/cnhs/{id}/{cnhFile.FileName}";

            await _repository.UpdateCNHImageAsync(id, imageUrl);

            return imageUrl;
        }


        public async Task<bool> CNPJExistsAsync(string cnpj)
        {
            var entregadores = await _repository.GetAllAsync();
            return entregadores.Any(e => e.CNPJ == cnpj);
        }
        public async Task<bool> CNHExistsAsync(string numeroCNH)
        {
            var entregadores = await _repository.GetAllAsync();
            return entregadores.Any(e => e.NumeroCNH == numeroCNH);
        }
    }
}
