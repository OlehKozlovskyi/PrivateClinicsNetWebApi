using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Wrapper
{
    public class Result<T>
    {
        public Result() 
        {
            errorMessage = new List<string>();
        }

        public T Data { get; set; }
        public bool IsSucceess {  get; set; }
        public List<string> errorMessage { get; set; }

        public static Result<T> Failure() => new Result<T> {IsSucceess = false };

        public static Task<Result<T>> FailureAsync() => Task.FromResult(Failure());

        public static Result<T> Failure(string errorMessage) 
        {
            return new Result<T> { IsSucceess = false, errorMessage = new List<string>() { errorMessage } };
        } 
        
        public static Task<Result<T>> FailureAsync(string errorMessage) => Task.FromResult(Failure(errorMessage));
        
        public static Result<T> Success() => new Result<T> {IsSucceess = true};

        public static Task<Result<T>> SuccessAsync() => Task.FromResult(Success());

        public static Result<T> Success(T data) => new Result<T> { IsSucceess = true, Data = data };

        public static Task<Result<T>> SuccessAsync(T data) => Task.FromResult(Success(data));

    }
}
