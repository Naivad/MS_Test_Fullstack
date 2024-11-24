using System.Data;

namespace MS_Test_Fullstack.Domain.IReposotories
{
    public interface IDataAccessRepository
    {
        Task<TOutput> ExecuteFirst<TOutput>(string storedProcedureKey, object obj, CommandType commandType);
        Task<IEnumerable<TOutput>> Execute<TOutput>(string storedProcedureKey, object obj, CommandType commandType);
    }

}
