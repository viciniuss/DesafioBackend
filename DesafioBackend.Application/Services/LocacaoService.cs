using DesafioBackend.Core.Interfaces;
using DesafioBackend.Core.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace DesafioBackend.Application.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;

        public LocacaoService(ILocacaoRepository locacaoRepository)
        {
            _locacaoRepository = locacaoRepository;
        }

        private readonly Dictionary<int, decimal> _planos = new()
        {
            { 7, 30.00m },
            { 15, 28.00m },
            { 30, 22.00m },
            { 45, 20.00m },
            { 50, 18.00m }
        };

        public async Task<Locacao> CriarLocacaoAsync(string entregadorId, string motoId, int plano)
        {
            if (!_planos.ContainsKey(plano))
                throw new ArgumentException("Plano inválido.");

            var valorDiaria = _planos[plano];
            var dataInicio = DateTime.UtcNow.AddDays(1); // Primeiro dia após criação
            var dataPrevisaoTermino = dataInicio.AddDays(plano);

            var locacao = new Locacao
            {
                ValorDiaria = valorDiaria,
                EntregadorId = entregadorId,
                MotoId = motoId,
                DataInicio = dataInicio,
                DataPrevisaoTermino = dataPrevisaoTermino,
                DataTermino = dataPrevisaoTermino, // Inicialmente igual à previsão
                CustoTotal = plano * valorDiaria
            };

            await _locacaoRepository.CreateAsync(locacao);
            return locacao;
        }

        public async Task<Locacao> FinalizarLocacaoAsync(string locacaoId, DateTime dataDevolucao)
        {
            var locacao = await _locacaoRepository.GetByIdAsync(locacaoId);
            if (locacao == null) throw new KeyNotFoundException("Locação não encontrada.");

            locacao.DataDevolucao = dataDevolucao;

            if (dataDevolucao < locacao.DataPrevisaoTermino)
            {
                var diasNaoUsados = (locacao.DataPrevisaoTermino - dataDevolucao).Days;
                var multaPercentual = locacao.ValorDiaria switch
                {
                    30.00m => 0.20m,
                    28.00m => 0.40m,
                    _ => 0.00m
                };

                locacao.Multa = diasNaoUsados * locacao.ValorDiaria * multaPercentual;
            }
            else if (dataDevolucao > locacao.DataPrevisaoTermino)
            {
                var diasExtras = (dataDevolucao - locacao.DataPrevisaoTermino).Days;
                locacao.Multa = diasExtras * 50.00m;
            }

            locacao.CustoTotal += locacao.Multa;

            await _locacaoRepository.UpdateDevolucaoAsync(locacao.Id, dataDevolucao);
            return locacao;
        }
        public async Task<Locacao> GetByIdAsync(string id)
        {
            return await _locacaoRepository.GetByIdAsync(id);
        }

    }


}
