using DesafioBackend.Core.Models;
using DesafioBackend.Core.Interfaces;

namespace DesafioBackend.Application.Services
{
    public class MotoService
    {
        private readonly IMotoRepository _repository;

        public MotoService(IMotoRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateMotoAsync(Moto moto)
        {
            // Validação de regra de negócio
            var existingMoto = await _repository.GetMotoByPlaca(moto.placa);
            if (existingMoto != null)
                throw new Exception("A moto com essa placa já está cadastrada.");

            await _repository.CreateAsync(moto);
        }

        public async Task<List<Moto>> GetAllMotosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Moto> GetMotoByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Moto> GetMotoByPlaca(string placa)
        {
            return await _repository.GetMotoByPlaca(placa);
        }

        public async Task UpdateMotoPlacaAsync(string id, string newPlaca)
        {
            await _repository.UpdatePlacaAsync(id, newPlaca);
        }

        public async Task DeleteMotoAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
