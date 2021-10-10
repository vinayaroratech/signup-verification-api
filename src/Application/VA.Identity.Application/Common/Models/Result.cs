using System.Collections.Generic;
using System.Linq;

namespace VA.Identity.Application.Common.Models
{
    public class Result
    {
        internal Result()
        {
        }

        internal Result(IEnumerable<string> errors)
        {
            Succeeded = false;
            Errors = errors.ToArray();
        }

        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }

    public class Result<T> : Result
    {
        internal Result()
        {
        }

        internal Result(T data)
        {
            Data = data;
            Succeeded = true;
        }

        internal Result(T data, bool succeeded, IEnumerable<string> errors)
        {
            Data = data;
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public T Data { get; set; }


        public static Result<T> Success(T data)
        {
            return new Result<T>(data, true, new string[] { });
        }

        public static Result<T> Failure(T data, IEnumerable<string> errors)
        {
            return new Result<T>(data, false, errors);
        }
    }
}
