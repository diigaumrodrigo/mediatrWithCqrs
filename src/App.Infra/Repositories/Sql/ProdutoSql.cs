namespace App.Infra.Repositories.Sql
{
    public class ProdutoSql
    {
        protected ProdutoSql() { }

        public const string InsertProduto = 
            @"INSERT [dbo].[Produto]
                (ProdutoId, CategoriaProdutoId, Descricao, Complemento, Valor, Ativo, DataCriacao)
            VALUES
                (@ProdutoId, @CategoriaProdutoId, @Descricao, @Complemento, @Valor, @Ativo, @DataCriacao)";

        public const string ProdutoJaExiste =
            @"SELECT CASE
                WHEN exists (
		            SELECT 1
                    FROM [dbo].[Produto] WITH(NOLOCK)
		            WHERE Descricao = @Descricao
	            ) THEN 1
                ELSE 0
            END as clienteExists";

        public const string GetProdutos =
            @"SELECT ProdutoId, CategoriaProdutoId, Descricao, Complemento, Valor, Ativo, DataCriacao
            FROM [dbo].[Produto]";
    }
}