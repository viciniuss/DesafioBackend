using Microsoft.AspNetCore.Mvc;
using DesafioBackend.Application.Services;
using DesafioBackend.Core.Models;

namespace DesafioBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly MotoService _motoService;

        public MotoController(MotoService motoService)
        {
            _motoService = motoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMoto([FromBody] Moto moto)
        {
            try
            {
                await _motoService.CreateMotoAsync(moto);
                return CreatedAtAction(nameof(GetMotoById), new { Id = moto.id }, moto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotoById(string id)
        {
            var moto = await _motoService.GetMotoByIdAsync(id);
            if (moto == null)
                return NotFound();

            return Ok(moto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMotos()
        {
            var motos = await _motoService.GetAllMotosAsync();
            return Ok(motos);
        }
        [HttpGet("placa/{placa}")]
        public async Task<IActionResult> GetMotoByPlaca(string placa)
        {
            var moto = await _motoService.GetMotoByPlaca(placa);
            if (moto == null)
                return NotFound();

            return Ok(moto);
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMotoPlaca(string id, [FromBody] string newPlaca)
        {
            await _motoService.UpdateMotoPlacaAsync(id, newPlaca);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(string id)
        {
            await _motoService.DeleteMotoAsync(id);
            return NoContent();
        }
    }
}
