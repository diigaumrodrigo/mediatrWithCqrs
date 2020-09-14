using System.Threading.Tasks;

namespace App.Application.Domain.Repositories
{
    public interface ICategoriaProdutoRepository
    {
         Task<bool> CategoriaExiste(int codigo);
    }
}