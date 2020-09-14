using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Infra.Repositories.Standard
{
    public interface IRepositoryAsync<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> AddOutputAsync(TEntity entity);
        Task AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}