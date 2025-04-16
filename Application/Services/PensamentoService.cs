using Application.Interface;
using Infrastructure.Interface;
using Domain.Models;
using AutoMapper;
using Domain.Dtos;
namespace Application.Services;


public class PensamentoService : IPensamentoService
{
    private readonly IPensamentoRepository _pensamentosRepository;
    private readonly IMapper _mapper;

    public PensamentoService(IPensamentoRepository pensamentosRepository, IMapper mapper)
    {
        _pensamentosRepository = pensamentosRepository;
        _mapper = mapper;
    }

    public async Task<bool> AdicionarPensamento(PensamentosDto pensamentosDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(pensamentosDto.Pensamento))
                throw new Exception("O pensamento é obrigatório.");

            if (string.IsNullOrWhiteSpace(pensamentosDto.Autor))
                throw new Exception("O autor é obrogatório");

            if (pensamentosDto.Modelos <= 0)
                throw new Exception("O modelo do pensamento deve ser maior que zero");

            var pensamentos = _mapper.Map<Pensamentos>(pensamentosDto);

            return await _pensamentosRepository.AdicionarPensamento(pensamentos);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> AtualizarPensamento(int id, Pensamentos pensamentos)
    {
        if (string.IsNullOrWhiteSpace(pensamentos.Pensamento))
            throw new Exception("O pensamento é obrigatório.");

        if (string.IsNullOrWhiteSpace(pensamentos.Autor))
            throw new Exception("O autor é obrogatório");

        if (pensamentos.Modelos <= 0)
            throw new Exception("O modelo do pensamento deve ser maior que zero");

        if (id != pensamentos.Id)
            throw new ArgumentException("O ID fornecido não corresponde ao ID do pensamento.", nameof(id));
        if (id <= 0)
            throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));


        return await _pensamentosRepository.AtualizarPensamento(pensamentos);
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

}
