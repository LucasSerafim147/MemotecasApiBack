using api_gerenciamento_cursos.Domain;
using Domain.Models;

namespace Infrastructure.Interface
{
    public interface IPensamentoRepository
    {
        Task<bool> AdicionarPensamento(Pensamentos pensamentos);
        Task<bool> RemoverPensamento(int id);
        Task<bool> AtualizarPensamento(Pensamentos pensamentos);
        Task<List<Pensamentos>> RetornarPensamento();
        Task<RetornoPaginado<Pensamentos>> RetornoPagiandoPensamentos(int quantidade, int pagina);


    }
}
