namespace App.Infra.Repositories.Sql
{
    public class CategoriaSql
    {
        public const string CategoriaExiste =
            @"SELECT CASE
                WHEN exists (
		            SELECT 1
                    FROM [dbo].[CategoriaProduto] WITH(NOLOCK)
		            WHERE Codigo = @Codigo
	            ) THEN 1
                ELSE 0
            END as clienteExists";
    }
}