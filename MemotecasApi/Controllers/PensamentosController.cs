using Application.Interface;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemotecasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensamentosController : ControllerBase
    {
        private readonly IPensamentoService _service;
        private readonly ILogger<PensamentosController> _logger;

        public PensamentosController(IPensamentoService service, ILogger<PensamentosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPensamento([FromBody] PensamentosDto pensamentos)
        {
            try
            {
                _logger.LogInformation("Recebido payload: {@Pensamentos}", pensamentos);
                var id = await _service.AdicionarPensamento(pensamentos);
                _logger.LogInformation("Pensamento criado com ID: {Id}", id);
                return CreatedAtAction(nameof(AdicionarPensamento), new { id }, pensamentos);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Erro de validação: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao adicionar pensamento: {Message}", ex.Message);
                return StatusCode(500, $"Erro interno ao adicionar o pensamento: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> RetornarPensamentos()
        {
            try
            {
                return Ok(await _service.RetornarPensamento());
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro interno ao retornar pensamentos: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPensamento(int id,[FromBody]PensamentosDto pensamentosDto)
        {
            try
            {
                var pensamento =  await _service.AtualizarPensamento(id, pensamentosDto);
                return Ok(pensamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPensamento(int id)
        {
            try
            {
                var pensamento = await _service.RemoverPensamento(id);
                return Ok(pensamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{pagina}/{quantidade}")]
        public async Task<IActionResult> BuscarPorPagina(int pagina, int quantidade)
        {
            try
            {
                var pensamento = await _service.RetornoPagiandoPensamentos(pagina, quantidade);
                if (pensamento == null)
                    return NotFound("Nenhum pensamento encontrado.");
                return Ok(pensamento);
            }
            catch (Exception ex) { throw; }
        }

    }
}
