using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api_gerenciamento_cursos.Domain;
using Domain.Dtos;
using Domain.Models;

namespace Application.Interface
{
    public interface IPensamentoService
    {
        Task<bool> AdicionarPensamento(PensamentosDto pensamentosDto);
        Task<bool> RemoverPensamento(int id);
        Task<bool> AtualizarPensamento(int id, PensamentosDto pensamentosDto);
        Task<List<Pensamentos>> RetornarPensamento();
        Task<RetornoPaginado<Pensamentos>> RetornoPagiandoPensamentos(int quantidade, int pagina);

    }
}
