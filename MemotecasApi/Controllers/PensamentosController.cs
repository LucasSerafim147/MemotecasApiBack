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

        public  PensamentosController(IPensamentoService service)
        {
            _service = service;
        }


        [HttpPost("pensamento")]
        public async Task<IActionResult> AdicionarPensamento([FromBody] PensamentosDto pensamentos)
        {
            try
            {
                var id =  await _service.AdicionarPensamento(pensamentos);
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
        public async Task<IActionResult> RetornarPensamentos()
        {
            try
            {
                return Ok( await _service.RetornarPensamento());
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro interno ao retornar pensamentos: {ex.Message}");
            }
        }

        [HttpPut("pensamento")]
        public async Task<IActionResult> AtualizarPensamento(int id ,Pensamentos pensamentos)
        {
            try
            {
                var pensamento =  await _service.AtualizarPensamento(id, pensamentos);
                return Ok(pensamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
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

    }
}
