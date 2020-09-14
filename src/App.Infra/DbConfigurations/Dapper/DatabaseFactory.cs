using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace App.Infra.DbConfigurations.Dapper
{
    public class DatabaseFactory : IDatabaseFactory
    {
        public string ConnectionString { get; }
        public IDbConnection GetDbConnection => OpenConnection();

        public DatabaseFactory(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("VendasConnStr");
        }

        private IDbConnection OpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}