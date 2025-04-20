using Application.Interface;
using Infrastructure.Interface;
using Domain.Models;
using AutoMapper;
using Domain.Dtos;
using api_gerenciamento_cursos.Domain;
using Microsoft.Extensions.Logging;
namespace Application.Services;


public class PensamentoService : IPensamentoService
{
    private readonly IPensamentoRepository _pensamentosRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PensamentoService> _logger;

    public PensamentoService(IPensamentoRepository pensamentosRepository, IMapper mapper, ILogger<PensamentoService> logger)
    {
        _pensamentosRepository = pensamentosRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PensamentosDto> AdicionarPensamento(PensamentosDto pensamentosDto)
    {
        try
        {
            _logger.LogInformation("DTO recebido: Modelos = {Modelos}", pensamentosDto.Modelos);

            var pensamento = new Pensamentos
            {
                Pensamento = pensamentosDto.Pensamento,
                Autor = pensamentosDto.Autor,
                Modelos = pensamentosDto.Modelos
            };

            _logger.LogInformation("Objeto mapeado: Modelos = {Modelos}", pensamento.Modelos);

            var id = await _pensamentosRepository.AdicionarPensamento(pensamento);

          
            return new PensamentosDto
            {
                Pensamento = pensamentosDto.Pensamento,
                Autor = pensamentosDto.Autor,
                Modelos = pensamentosDto.Modelos   
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar pensamento");
            throw;
        }
    }

    public async Task<bool> AtualizarPensamento(int id, PensamentosDto pensamentosDto)
    {
        if (string.IsNullOrWhiteSpace(pensamentosDto.Pensamento))
            throw new Exception("O pensamento é obrigatório.");

        if (string.IsNullOrWhiteSpace(pensamentosDto.Autor))
            throw new Exception("O autor é obrogatório");

        if (pensamentosDto.Modelos <= 0)
            throw new Exception("O modelo do pensamento deve ser maior que zero");

       
        if (id <= 0)
            throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));

        var pensamento = _mapper.Map<Pensamentos>(pensamentosDto);
        pensamento.Id = id;

        return await _pensamentosRepository.AtualizarPensamento(pensamento);
    }

    public async Task<bool> RemoverPensamento(int id)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));

            return await _pensamentosRepository.RemoverPensamento(id);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<Pensamentos>> RetornarPensamento()
    {
        try
        {
            var pensamentos = await _pensamentosRepository.RetornarPensamento();
            return _mapper.Map<List<Pensamentos>>(pensamentos);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<RetornoPaginado<Pensamentos>> RetornoPagiandoPensamentos(int quantidade, int pagina)
    {
        try
        {
            var pensamentos = await _pensamentosRepository.RetornoPagiandoPensamentos(quantidade, pagina);
            return pensamentos;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
