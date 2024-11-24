using MS_Test_Fullstack.Domain.Interfaces;
using MS_Test_Fullstack.Domain.IReposotories;
using System.Data;

namespace MS_Test_Fullstack.Infrastructure
{
    public class GenericRepository : DataAccessRepository, IDataAccessRepository
    {
        public GenericRepository(IDbConnectionFactory connection) : base(connection) { }

        public async Task<IEnumerable<TOutput>> Execute<TOutput>(string storedProcedureKey, object obj, CommandType commandType)
        {
            return await Exec<object, TOutput>(storedProcedureKey, obj, commandType);
        }

        public async Task<TOutput> ExecuteFirst<TOutput>(string storedProcedureKey, object obj, CommandType commandType)
        {
            return await ExecFirst<object, TOutput>(storedProcedureKey, obj, commandType);
        }
    }
}
