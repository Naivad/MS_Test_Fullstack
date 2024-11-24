using Microsoft.Extensions.Configuration;
using MS_Test_Fullstack.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace MS_Test_Fullstack.Infrastructure
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbConnection")!;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
