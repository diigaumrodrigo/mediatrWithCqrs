using System.Collections.Generic;
using System.Threading.Tasks;
using App.Application.Domain.Commands;
using App.Application.Domain.Models;

namespace App.Application.Domain.Repositories
{
    public interface IProdutoRepository
    {
        Task Cadastrar(CadastraProdutoCommand produtoCommand);
        Task<bool> ProdutoExiste(string descricao);
        Task<IEnumerable<ProdutoResponseModel>> GetProdutos();
    }
}