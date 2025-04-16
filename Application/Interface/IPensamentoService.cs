using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Models;

namespace Application.Interface
{
    public interface IPensamentoService
    {
        Task<int> AdicionarPensamento(PensamentosDto pensamentosDto);
        Task<bool> RemoverPensamento(int id);
        Task<bool> AtualizarPensamento(int id, Pensamentos pensamentos);
        Task<List<Pensamentos>> RetornarPensamento();

    }
}
