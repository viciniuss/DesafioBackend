using MongoDB.Driver;
using DesafioBackend.Core.Models;
using DesafioBackend.Core.Interfaces;
using DesafioBackend.Infrastructure.Context;

namespace DesafioBackend.Infrastructure.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly IMongoCollection<Moto> _motosCollection;

        public MotoRepository(MongoDbContext context)
        {
            _motosCollection = context.GetCollection<Moto>("Motos");
        }

        public async Task<Moto> GetByIdAsync(string id)
        {
            var filter = Builders<Moto>.Filter.Eq(m => m.id, id);
            return await _motosCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Moto>> GetAllAsync()
        {
            return await _motosCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Moto> GetMotoByPlaca(string placa)
        {
            var filter = Builders<Moto>.Filter.Eq(m => m.placa, placa);
            return await _motosCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Moto moto)
        {
            await _motosCollection.InsertOneAsync(moto);
        }

        public async Task UpdatePlacaAsync(string id, string newplaca)
        {
            var filter = Builders<Moto>.Filter.Eq(m => m.id, id);
            var update = Builders<Moto>.Update.Set(m => m.placa, newplaca);
            await _motosCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Moto>.Filter.Eq(m => m.id, id);
            await _motosCollection.DeleteOneAsync(filter);
        }
    }
}
