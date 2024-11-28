using DesafioBackend.Application.Services;
using DesafioBackend.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly LocacaoService _locacaoService;

        public LocacaoController(LocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarLocacao([FromBody] Locacao locacao)
        {
            if (locacao == null)
                return BadRequest("Dados da locação inválidos.");

            var locacaoCriada = await _locacaoService.CriarLocacaoAsync(locacao.EntregadorId, locacao.MotoId, locacao.DataInicio);
            return CreatedAtAction(nameof(ObterLocacaoPorId), new { id = locacaoCriada.Id }, locacaoCriada);
        }
        [HttpGet("entregador/{entregadorId}")]
        public async Task<IActionResult> ObterLocacoesPorEntregadorId([FromRoute] string entregadorId)
        {
            var locacoes = await _locacaoService.ObterLocacoesPorEntregadorIdAsync(new MongoDB.Bson.ObjectId(entregadorId));
            return Ok(locacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterLocacaoPorId([FromRoute] string id)
        {
            var locacao = await _locacaoService.ObterLocacaoPorIdAsync(new MongoDB.Bson.ObjectId(id));
            if (locacao == null)
                return NotFound();

            return Ok(locacao);
        }

        [HttpPut("{id}/finalizar")]
        public async Task<IActionResult> FinalizarLocacao([FromRoute] string id)
        {
            var sucesso = await _locacaoService.FinalizarLocacaoAsync(new MongoDB.Bson.ObjectId(id));
            if (!sucesso)
                return BadRequest("Não foi possível finalizar a locação.");

            return NoContent();
        }
    }
}
