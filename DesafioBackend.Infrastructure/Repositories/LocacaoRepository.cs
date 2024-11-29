using DesafioBackend.Core.Models;
using DesafioBackend.Core.Interfaces;
using DesafioBackend.Infrastructure.Context;
using MongoDB.Driver;

namespace DesafioBackend.Infrastructure.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly IMongoCollection<Locacao> _collection;

        public LocacaoRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Locacao>("Locacao");
        }

        public async Task<IEnumerable<Locacao>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Locacao> GetByIdAsync(string id)
        {
            return await _collection.Find(l => l.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Locacao locacao)
        {
            await _collection.InsertOneAsync(locacao);
        }

        public async Task UpdateDevolucaoAsync(string id, DateTime dataDevolucao)
        {
            var filter = Builders<Locacao>.Filter.Eq(l => l.Id, id);
            var update = Builders<Locacao>.Update.Set(l => l.DataDevolucao, dataDevolucao);
            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
