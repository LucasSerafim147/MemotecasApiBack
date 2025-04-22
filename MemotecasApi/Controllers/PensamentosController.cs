using System.Text.Json;
using Application.Interface;
using Application.Services;
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
        public async Task<ActionResult<PensamentosDto>> AdicionarPensamento([FromBody] PensamentosDto pensamentoDto)
        {
            try
            {
                var resultado = await _service.AdicionarPensamento(pensamentoDto);
                return Ok(resultado); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> RetornarPensamentos()
        {
            try
            {
                var pensamentos = await _service.RetornarPensamento();
                Console.WriteLine(JsonSerializer.Serialize(pensamentos));
                    return Ok(pensamentos);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro interno ao retornar pensamentos: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPensamento(int id, [FromBody] PensamentosDto pensamentosDto)
        {
            try
            {
                var pensamento = await _service.AtualizarPensamento(id, pensamentosDto);
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

        [HttpGet("{quantidade}/{pagina}")]
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
