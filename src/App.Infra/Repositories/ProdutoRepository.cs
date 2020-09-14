using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using App.Application.Domain.Commands;
using App.Application.Domain.Models;
using App.Application.Domain.Repositories;
using App.Infra.DbConfigurations.Dapper;
using App.Infra.Models.Entities;
using App.Infra.Repositories.Sql;
using App.Infra.Repositories.Standard.Dapper;
using AutoMapper;
using Dapper;

namespace App.Infra.Repositories
{
    public class ProdutoRepository : RepositoryAsync<Produto>, IProdutoRepository
    {
        private readonly IMapper _mapper;

        public ProdutoRepository(
            IMapper mapper,
            IDatabaseFactory databaseOptions,
            IDbTransaction dbTransaction = null
            ) : base(databaseOptions, dbTransaction)
        {
            _mapper = mapper;
        }

        public async Task Cadastrar(CadastraProdutoCommand produtoCommand)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync(sql: ProdutoSql.InsertProduto, param: new
                {
                    ProdutoId = Guid.NewGuid(),
                    CategoriaProdutoId = produtoCommand.Categoria,
                    Descricao = produtoCommand.Descricao,
                    Complemento = produtoCommand.Complemento,
                    Valor = produtoCommand.Valor,
                    Ativo = true,
                    DataCriacao = DateTime.Now
                });
            }
        }

        public async Task<IEnumerable<ProdutoResponseModel>> GetProdutos()
        {
            using (var db = GetConnection())
            {
                var produtos = await db.QueryAsync<Produto>(sql: ProdutoSql.GetProdutos);

                if (produtos != null)
                    return _mapper.Map<IEnumerable<ProdutoResponseModel>>(produtos);

                return null;
            }
        }

        public async Task<bool> ProdutoExiste(string descricao)
        {
            using (var db = GetConnection())
            {
                var retorno = await db.QueryFirstOrDefaultAsync<int>(ProdutoSql.ProdutoJaExiste,
                    new
                    {
                        Descricao = descricao
                    });

                return retorno == 1;
            }
        }
    }
}