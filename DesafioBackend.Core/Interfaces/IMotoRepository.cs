using DesafioBackend.Core.Models;

namespace DesafioBackend.Core.Interfaces
{
    public interface IMotoRepository
    {
        Task<Moto> GetByIdAsync(string id);
        Task<List<Moto>> GetAllAsync();
        Task<Moto> GetMotoByPlaca(string placa);
        Task CreateAsync(Moto moto);
        Task UpdatePlacaAsync(string id, string newPlaca);
        Task DeleteAsync(string id);
    }
}
