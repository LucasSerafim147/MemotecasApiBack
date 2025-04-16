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

        public PensamentosController(IPensamentoService service)
        {
            _service = service;
        }


        [HttpPost("pensamento")]
        public IActionResult AdicionarPensamento([FromBody] PensamentosDto pensamentos)
        {
            try
            {
                var id = _service.AdicionarPensamento(pensamentos);
                return CreatedAtAction(nameof(AdicionarPensamento), new { id }, pensamentos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao adicionar o pensamento: {ex.Message}");
            }
        }
        [HttpGet("pensamento")]
        public IActionResult RetornarPensamentos()
        {
            try
            {
                return Ok(_service.RetornarPensamento());
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro interno ao retornar pensamentos: {ex.Message}");
            }
        }

        [HttpPut("pensamento")]
        public IActionResult AtualizarPensamento(int id ,Pensamentos pensamentos)
        {
            try
            {
                var pensamento = _service.AtualizarPensamento(id, pensamentos);
                return Ok(pensamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeletarPensamento(int id)
        {
            try
            {
                var pensamento = _service.RemoverPensamento(id);
                return Ok(pensamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
