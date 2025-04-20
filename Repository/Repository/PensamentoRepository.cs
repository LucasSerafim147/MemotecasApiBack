using System.Data;
using api_gerenciamento_cursos.Domain;
using AutoMapper;
using Dapper;
using Domain.Models;
using Infrastructure.Interface;

namespace Infrastructure.Repository
{
    public class PensamentoRepository : IPensamentoRepository
    {

        private readonly IDbConnection _conn;
        private readonly IMapper _mapper;

        public PensamentoRepository(IDbConnection conn, IMapper mapper)
        {
            _conn = conn;
            _mapper = mapper;
        }

        public async Task<int> AdicionarPensamento(Pensamentos pensamentos)
        {
            try
            {
                string sql = @"INSERT INTO PENSAMENTOS(PENSAMENTO,AUTOR,MODELOS) VALUES(@PENSAMENTO,@AUTOR,@MODELOS);
                SELECT CAST(SCOPE_IDENTITY() AS INT)";

                var parametros = new
                {
                    PENSAMENTO = pensamentos.Pensamento,
                    AUTOR = pensamentos.Autor,
                    MODELOS = pensamentos.Modelos
                };

                   

                var id = await _conn.ExecuteScalarAsync<int>(sql, parametros);
                

                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AtualizarPensamento(Pensamentos pensamentos)
        {
            try
            {
                string sql = @"UPDATE PENSAMENTOS SET PENSAMENTO = @PENSAMENTO, AUTOR = @AUTOR, MODELOS = @MODELOS WHERE ID = @ID";

                var paramentros = new
                {
                    PENSAMENTO = pensamentos.Pensamento,
                    AUTOR = pensamentos.Autor,
                    MODELOS = pensamentos.Modelos    ,
                    ID = pensamentos.Id
                };

                var resultado = await _conn.ExecuteAsync(sql, paramentros);

                return resultado > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RemoverPensamento(int id)
        {
            try
            {
                string sql = string.Format("DELETE FROM PENSAMENTOS WHERE ID ={0}", id);
                var resultado = await _conn.ExecuteAsync(sql);

                return resultado > 0 ? true: false;
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
                string sql = "SELECT * FROM PENSAMENTOS";

                var resutaldo = await _conn.QueryAsync<Pensamentos>(sql);

                return resutaldo.ToList();
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
                string sql = "SELECT * FROM PENSAMENTOS ORDER BY ID OFFSET @OFFSET ROWS FETCH NEXT @QUANTIDADE ROWS ONLY";

                var parametros = new
                {
                    OFFSET = (pagina - 1) * quantidade,
                    QUANTIDADE = quantidade
                };

                var pensamentos = await _conn.QueryAsync<Pensamentos>(sql, parametros);
                var totalPensamentos = "SELECT COUNT(*) FROM PENSAMENTOS";

                var retornoTotalPensamentos = await _conn.ExecuteScalarAsync<int>(totalPensamentos);

                var retornoPaginado = new RetornoPaginado<Pensamentos>
                {
                    TotalRegistros = retornoTotalPensamentos,
                    Pagina = pagina,
                    QtdPagina = quantidade,
                    Retorno = pensamentos.ToList()
                };

                return retornoPaginado;
            }
            catch (Exception ex) { throw; }
        }
    }
    }

