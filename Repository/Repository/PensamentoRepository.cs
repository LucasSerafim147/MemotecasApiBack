using System.Data.Common;
using AutoMapper;
using Dapper;
using Domain.Models;
using Infrastructure.Interface;

namespace Infrastructure.Repository
{
    public class PensamentoRepository : IPensamentoRepository
    {

        private readonly DbConnection _conn;
        private readonly IMapper _mapper;

        public PensamentoRepository(DbConnection conn, IMapper mapper)
        {
            _conn = conn;
            _mapper = mapper;
        }

        public async Task<int> AdicionarPensamento(Pensamentos pensamentos)
        {
            try
            {
                string sql = @"INSERT INTO PENSAMENTOS(PENSAMENTO,AUTOR,MODELO) VALUES(@PENSAMENTO,@AUTOR,@MODELO)";

                var parametros = new
                {
                    PENSAMENTO = pensamentos.Pensamento,
                    AUTOR = pensamentos.Autor,
                    MODELO = pensamentos.Modelos
                };

                if (_conn.State != System.Data.ConnectionState.Open)
                    await _conn.OpenAsync();

                var id = await _conn.ExecuteScalarAsync<int>(sql, parametros);
                await _conn.CloseAsync();

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
                string sql = @"UPDATE PENSAMENTOS SET PENSAMENTO = @PENSAMENTO, AUTOR = @AUTOR, MODELO = @MODELO WHERE ID = @ID";

                var paramentros = new
                {
                    PENSAMENTO = pensamentos.Pensamento,
                    AUTOR = pensamentos.Autor,
                    MODELO = pensamentos.Modelos,
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
    }
}
