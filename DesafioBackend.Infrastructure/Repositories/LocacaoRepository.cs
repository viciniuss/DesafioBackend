using DesafioBackend.Core.Interfaces;
using DesafioBackend.Core.Models;
using DesafioBackend.Infrastructure.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioBackend.Infrastructure.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly IMongoCollection<Locacao> _locacoes;

        public LocacaoRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Locacao>("Locacao");
        }

        public async Task<Locacao> CriarLocacaoAsync(Locacao locacao)
        {
            await _locacoes.InsertOneAsync(locacao);
            return locacao;
        }

        public async Task<List<Locacao>> ObterLocacoesPorEntregadorIdAsync(ObjectId entregadorId)
        {
            var filter = Builders<Locacao>.Filter.Eq(l => l.EntregadorId, entregadorId);
            return await _locacoes.Find(filter).ToListAsync();
        }

        public async Task<Locacao> ObterLocacaoPorIdAsync(ObjectId locacaoId)
        {
            var filter = Builders<Locacao>.Filter.Eq(l => l.Id, locacaoId);
            return await _locacoes.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> AtualizarLocacaoAsync(ObjectId locacaoId, Locacao locacaoAtualizada)
        {
            var filter = Builders<Locacao>.Filter.Eq(l => l.Id, locacaoId);
            var update = Builders<Locacao>.Update
                .Set(l => l.DataFim, locacaoAtualizada.DataFim)
                .Set(l => l.Status, locacaoAtualizada.Status);

            var result = await _locacoes.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
