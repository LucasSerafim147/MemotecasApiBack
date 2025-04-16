using Application.Interface;
using Infrastructure.Interface;
using Domain.Models;
namespace Application.Services;


public class PensamentoService : IPensamentoService
{
    private readonly IPensamentoRepository _pensamentosRepository;


    public Task<bool> AdicionarPensamento(Pensamentos pensamentos)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AtualizarPensamento(Pensamentos pensamentos)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoverPensamento(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Pensamentos>> RetornarPensamento()
    {
        throw new NotImplementedException();
    }
}
