using DesafioBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DesafioBackend.Core.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<Locacao> CriarLocacaoAsync(Locacao locacao);
        Task<List<Locacao>> ObterLocacoesPorEntregadorIdAsync(ObjectId entregadorId);
        Task<Locacao> ObterLocacaoPorIdAsync(ObjectId locacaoId);
        Task<bool> AtualizarLocacaoAsync(ObjectId locacaoId, Locacao locacaoAtualizada);
    }
}
