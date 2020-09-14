using System.Data;

namespace App.Infra.DbConfigurations.Dapper
{
    public interface IDatabaseFactory
    {
        string ConnectionString { get; }
        IDbConnection GetDbConnection { get; }
    }
}