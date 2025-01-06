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
            Messages = new List<string>();
        }

        public T Data { get; set; }
        public bool Succeeded {  get; set; }
        public List<string> Messages { get; set; }

        public static Result<T> Failure() => new Result<T> {Succeeded = false };

        public static Task<Result<T>> FailureAsync() => Task.FromResult(Failure());

        public static Result<T> Failure(string errorMessage) 
        {
            return new Result<T> { Succeeded = false, Messages = new List<string>() { errorMessage } };
        } 
        
        public static Task<Result<T>> FailureAsync(string errorMessage)
        {
            return Task.FromResult(Failure(errorMessage));
        }

        public static Result<T> Succeess() => new Result<T> {Succeeded = false};

        public static Task<Result<T>> SucceessAsync() => Task.FromResult(Succeess());

    }
}
