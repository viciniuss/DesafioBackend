using DesafioBackend.Core.Interfaces;
using DesafioBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DesafioBackend.Application.Services
{
    public class LocacaoService
    {
        private readonly ILocacaoRepository _repository;

        public LocacaoService(ILocacaoRepository locacaoRepository)
        {
            _repository = locacaoRepository;
        }

        public async Task<Locacao> CriarLocacaoAsync(ObjectId entregadorId, ObjectId motoId, DateTime dataInicio)
        {
            var novaLocacao = new Locacao
            {
                EntregadorId = entregadorId,
                MotoId = motoId,
                DataInicio = dataInicio,
                Status = "Ativa"
            };

            return await _repository.CriarLocacaoAsync(novaLocacao);
        }

        public async Task<List<Locacao>> ObterLocacoesPorEntregadorIdAsync(ObjectId entregadorId)
        {
            return await _repository.ObterLocacoesPorEntregadorIdAsync(entregadorId);
        }

        public async Task<Locacao> ObterLocacaoPorIdAsync(ObjectId locacaoId)
        {
            return await _repository.ObterLocacaoPorIdAsync(locacaoId);
        }

        public async Task<bool> FinalizarLocacaoAsync(ObjectId locacaoId)
        {
            var locacao = await _repository.ObterLocacaoPorIdAsync(locacaoId);

            if (locacao == null || locacao.Status != "Ativa")
            {
                return false;
            }

            locacao.Status = "Concluída";
            locacao.DataFim = DateTime.Now;

            return await _repository.AtualizarLocacaoAsync(locacaoId, locacao);
        }
    }
}
