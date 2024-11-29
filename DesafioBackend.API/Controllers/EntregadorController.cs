using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DesafioBackend.Application.Services;
using DesafioBackend.Core.Models;
using DesafioBackend.Infrastructure.Messaging;
using System.Threading.Tasks;
using System.IO;

namespace DesafioBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntregadorController : ControllerBase
    {
        private readonly EntregadorService _service;
        private readonly KafkaProducer _kafkaProducer;

        public EntregadorController(EntregadorService service, KafkaProducer kafkaProducer)
        {
            _service = service;
            _kafkaProducer = kafkaProducer;
        }

        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetById(string id)
        {
            var entregador = await _service.GetByIdAsync(id);
            if (entregador == null)
            {
                _kafkaProducer.SendMessageAsync("Não encontrado");
                return NotFound();
            }
            _kafkaProducer.SendMessageAsync("Encontrado");
            return Ok(entregador);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Entregador entregador)
        {
            if (!new[] { "A", "B", "A+B" }.Contains(entregador.TipoCNH))
                return BadRequest("O tipo da CNH deve ser 'A', 'B' ou 'A+B'.");

            var cnpjExists = await _service.CNPJExistsAsync(entregador.CNPJ);
            if (cnpjExists)
                return Conflict("O CNPJ já está cadastrado.");

            var cnhExists = await _service.CNHExistsAsync(entregador.NumeroCNH);
            if (cnhExists)
                return Conflict("O número da CNH já está cadastrado.");

            await _service.CreateAsync(entregador);
            return CreatedAtAction(nameof(GetById), new { id = entregador.Id }, entregador);
        }

        [HttpPost("{id}/upload-cnh")]
        public async Task<IActionResult> UploadCNHAsync(string id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            var validExtensions = new[] { ".png", ".bmp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!validExtensions.Contains(fileExtension))
                return BadRequest("Somente arquivos PNG ou BMP são permitidos.");

            var imageUrl = await _service.UploadCNHAsync(id, file);

            if (string.IsNullOrEmpty(imageUrl))
                return StatusCode(500, "Erro ao tentar salvar a imagem da CNH.");

            return Ok(new { Message = "Imagem da CNH atualizada com sucesso.", ImageUrl = imageUrl });
        }



    }
}