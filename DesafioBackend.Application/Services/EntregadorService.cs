using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioBackend.Core.Interfaces;
using DesafioBackend.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DesafioBackend.Application.Services
{

    public class EntregadorService
    {
        private readonly IEntregadorRepository _repository;
        private readonly string _localDirectory = Path.Combine(Directory.GetCurrentDirectory(), "CNHImages");

        public EntregadorService(IEntregadorRepository repository)
        {
            _repository = repository;

            if (!Directory.Exists(_localDirectory))
            {
                Directory.CreateDirectory(_localDirectory);
            }
        }

        public async Task<string> UploadCNHAsync(string id, IFormFile cnhFile)
        {
            // Verificar se o arquivo é válido (PNG ou BMP)
            if (cnhFile == null || !(cnhFile.ContentType == "image/png" || cnhFile.ContentType == "image/bmp"))
            {
                throw new Exception("A imagem da CNH deve ser PNG ou BMP.");
            }

            // Criar o caminho completo para salvar o arquivo localmente
            var fileName = $"{id}_{cnhFile.FileName}";
            var filePath = Path.Combine(_localDirectory, fileName);

            // Salvar o arquivo no disco local
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cnhFile.CopyToAsync(stream);
            }

            // Atualizar o campo ImagemCNH no banco de dados
            await _repository.UpdateCNHImageAsync(id, fileName);

            return fileName; // Retorna o nome do arquivo salvo
        }
        public Task<Entregador> GetByIdAsync(string id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task CreateAsync(Entregador entregador)
        {
            return _repository.CreateAsync(entregador);
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
