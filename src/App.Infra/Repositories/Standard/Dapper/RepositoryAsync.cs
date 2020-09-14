using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using App.Infra.DbConfigurations.Dapper;
using Dapper;

namespace App.Infra.Repositories.Standard.Dapper
{
    public abstract class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        private readonly IDatabaseFactory databaseOptions;

        protected IDbTransaction dbTransaction { get; set; }

        protected virtual string InsertQueryReturnInserted { get; }
        protected virtual string UpdateByIdQuery { get; }
        protected virtual string SelectByIdQuery { get; }
        protected virtual string SelectAllQuery { get; }

        protected RepositoryAsync(IDatabaseFactory databaseOptions, IDbTransaction dbTransaction = null)
        {
            this.databaseOptions = databaseOptions;
            this.dbTransaction = dbTransaction;
        }

        protected IDbConnection GetConnection()
        {
            return databaseOptions.GetDbConnection;
        }

        protected string GetConnectionString()
        {
            return databaseOptions.ConnectionString;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            using (var dbConn = GetConnection())
                await dbConn.QuerySingleAsync<TEntity>(InsertQueryReturnInserted, entity, transaction: dbTransaction);
        }

        public virtual async Task<TEntity> AddOutputAsync(TEntity entity)
        {
            using (var dbConn = GetConnection())
            {
                TEntity entitySaved = await dbConn.QuerySingleAsync<TEntity>(InsertQueryReturnInserted, entity, transaction: dbTransaction);
                return entitySaved;
            }
        }
        
        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            using (var dbConn = GetConnection())
                return await dbConn.ExecuteAsync(InsertQueryReturnInserted, entities, transaction: dbTransaction);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var dbConn = GetConnection())
                return await dbConn.QueryAsync<TEntity>(SelectAllQuery, transaction: dbTransaction);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            using (var dbConn = GetConnection())
            {
                var entity = await dbConn.QueryAsync<TEntity>(SelectByIdQuery, new { Id = id }, transaction: dbTransaction);
                return entity.FirstOrDefault();
            }
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            using (var dbConn = GetConnection())
                return await dbConn.ExecuteAsync(UpdateByIdQuery, entity, transaction: dbTransaction);
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            using (var dbConn = GetConnection())
                return await dbConn.ExecuteAsync(UpdateByIdQuery, entities.Select(obj => obj), transaction: dbTransaction);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }
    }
}