using DesafioBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DesafioBackend.Core.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<IEnumerable<Locacao>> GetAllAsync();
        Task<Locacao> GetByIdAsync(string id);
        Task CreateAsync(Locacao locacao);
        Task UpdateDevolucaoAsync(string id, DateTime dataDevolucao);

        
    }


}
