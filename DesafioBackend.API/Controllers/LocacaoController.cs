using DesafioBackend.Application.Services;
using DesafioBackend.Core.Models;
using DesafioBackend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DesafioBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoService _locacaoService;

        public LocacaoController(ILocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarLocacao(string entregadorId, string motoId, int plano)
        {
            try
            {
                var locacao = await _locacaoService.CriarLocacaoAsync(entregadorId, motoId, plano);
                return Ok(locacao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var locacao = await _locacaoService.GetByIdAsync(id);

                if (locacao == null)
                    return NotFound(new { Message = "Locação não encontrada." });

                return Ok(locacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao buscar locação.", Details = ex.Message });
            }
        }


        [HttpPut("{id}/finalizar")]
        public async Task<IActionResult> FinalizarLocacao(string id, [FromBody] DateTime dataDevolucao)
        {
            try
            {
                var locacao = await _locacaoService.FinalizarLocacaoAsync(id, dataDevolucao);
                return Ok(locacao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }


}
