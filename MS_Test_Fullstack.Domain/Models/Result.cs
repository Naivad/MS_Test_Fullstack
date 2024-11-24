using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS_Test_Fullstack.Domain.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public Result(bool IsSuccess, string Message, T? Data)
        {
            this.IsSuccess = IsSuccess;
            this.Message = Message;
            this.Data = Data;
        }

        public static Result<T> Success(T data) => new Result<T>(true, "Successful request", data);
        public static Result<T> Failure(string message) => new Result<T>(false, message, default);
    }
}
