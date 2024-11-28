using DesafioBackend.Core.Models;
using MongoDB.Bson;

namespace DesafioBackend.Core.Interfaces
{
    public interface IEntregadorRepository
    {
        Task<IEnumerable<Entregador>> GetAllAsync();
        Task<Entregador> GetByIdAsync(string id);
        Task CreateAsync(Entregador entregador);
        Task UpdateCNHImageAsync(string id, string imageUrl);

    }
}