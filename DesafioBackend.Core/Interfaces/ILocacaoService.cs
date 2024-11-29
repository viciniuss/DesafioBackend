using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioBackend.Core.Models;
using MongoDB.Bson;

namespace DesafioBackend.Core.Interfaces
{
    public interface ILocacaoService
    {
        Task<Locacao> CriarLocacaoAsync(string entregadorId, string motoId, int plano);
        Task<Locacao> FinalizarLocacaoAsync(string locacaoId, DateTime dataDevolucao);
        Task<Locacao> GetByIdAsync(string id);

    }

}