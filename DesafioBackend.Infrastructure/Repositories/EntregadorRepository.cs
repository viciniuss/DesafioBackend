using DesafioBackend.Core.Models;
using DesafioBackend.Core.Interfaces;
using DesafioBackend.Infrastructure.Context;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DesafioBackend.Infrastructure.Repositories
{
    public class EntregadorRepository : IEntregadorRepository
    {
        private readonly IMongoCollection<Entregador> _collection;

        public EntregadorRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Entregador>("Entregadores");
        }

        public async Task<IEnumerable<Entregador>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Entregador> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Entregador> GetByCNPJAsync(string cnpj)
        {
            return await _collection.Find(e => e.CNPJ == cnpj).FirstOrDefaultAsync();
        }

        public async Task<Entregador> GetByNumeroCNHAsync(string numeroCNH)
        {
            return await _collection.Find(e => e.NumeroCNH == numeroCNH).FirstOrDefaultAsync();
        }


        public async Task CreateAsync(Entregador entregador)
        {
            await _collection.InsertOneAsync(entregador);
        }

        public async Task UpdateCNHImageAsync(string id, string imageUrl)
        {
            var filter = Builders<Entregador>.Filter.Eq(e => e.Id, id);
            var update = Builders<Entregador>.Update.Set(e => e.ImagemCNH, imageUrl);
            await _collection.UpdateOneAsync(filter, update);
        }




    }
}
