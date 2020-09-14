using System.Data;
using System.Threading.Tasks;
using App.Application.Domain.Repositories;
using App.Infra.DbConfigurations.Dapper;
using App.Infra.Models.Entities;
using App.Infra.Repositories.Sql;
using App.Infra.Repositories.Standard.Dapper;
using Dapper;

namespace App.Infra.Repositories
{
    public class CategoriaProdutoRepository : RepositoryAsync<CategoriaProduto>, ICategoriaProdutoRepository
    {
        public CategoriaProdutoRepository(IDatabaseFactory databaseOptions, IDbTransaction dbTransaction = null)
            : base(databaseOptions, dbTransaction)
        { }

        public async Task<bool> CategoriaExiste(int codigo)
        {
            using (var db = GetConnection())
            {
                var retorno = await db.QueryFirstOrDefaultAsync<int>(CategoriaSql.CategoriaExiste,
                    new
                    {
                        Codigo = codigo
                    });

                return retorno == 1;
            }
        }
    }
}