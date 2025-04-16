using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.Interface
{
    public interface IPensamentoRepository
    {
        Task<bool> AdicionarPensamento(Pensamentos pensamentos);
        Task<bool> RemoverPensamento(int id);
        Task<bool> AtualizarPensamento(Pensamentos pensamentos);
        Task<bool> RetornarPensamento();


    }
}
