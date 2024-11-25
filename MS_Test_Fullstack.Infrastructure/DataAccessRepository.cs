using Dapper;
using MS_Test_Fullstack.Domain.Interfaces;
using Newtonsoft.Json;
using System.Data;


namespace MS_Test_Fullstack.Infrastructure
{
    public class DataAccessRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public DataAccessRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        internal async Task<TOutput> ExecFirst<TInput, TOutput>(string spName, TInput inputObject, CommandType command) where TInput : class
        {


            using IDbConnection conn = _connectionFactory.CreateConnection();

    
            if (conn.State != ConnectionState.Open)
            {
                 conn.Open();
            }


            var json = JsonConvert.SerializeObject(inputObject, Formatting.None);

 
            var result = await conn.QueryFirstOrDefaultAsync<TOutput>(spName,
                inputObject == null ? null : new { json },
                commandType: command);

            return result!;
        }

        internal async Task<IEnumerable<TOutput>> Exec<TInput, TOutput>(string spName, TInput inputObject, CommandType command) where TInput : class
        {


            using IDbConnection conn = _connectionFactory.CreateConnection();


            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            var json = JsonConvert.SerializeObject(inputObject, Formatting.None);
            var result = await conn.QueryAsync<TOutput>(spName, inputObject == null ? null : new { json }, commandType: command);
    
            return result!;
        }


    }
}
