using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DesafioBackend.Application.Services;
using DesafioBackend.Core.Models;
using System.Threading.Tasks;
using System.IO;

namespace DesafioBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntregadorController : ControllerBase
    {
        private readonly EntregadorService _service;
        private readonly S3StorageService _s3StorageService;

        public EntregadorController(EntregadorService service, S3StorageService s3StorageService)
        {
            _service = service;
            _s3StorageService = s3StorageService;
        }

        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetById(string id)
        {
            var entregador = await _service.GetByIdAsync(id);
            if (entregador == null)
                return NotFound();

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
            // Verifica se o arquivo foi enviado
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            // Verifica a extensão do arquivo
            var validExtensions = new[] { ".png", ".bmp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!validExtensions.Contains(fileExtension))
                return BadRequest("Somente arquivos PNG ou BMP são permitidos.");

            // Chama o serviço para fazer o upload no S3 (ou outro serviço de storage)
            var imageUrl = await _service.UploadCNHAsync(id, file);

            if (string.IsNullOrEmpty(imageUrl))
                return StatusCode(500, "Erro ao tentar salvar a imagem da CNH.");

            return Ok(new { Message = "Imagem da CNH atualizada com sucesso.", ImageUrl = imageUrl });
        }



    }
}