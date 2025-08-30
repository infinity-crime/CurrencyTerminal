using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.App.Common
{
    public class Result
    {
        public Error? Error { get; }
        public bool IsSuccess { get; }

        protected Result()
        {
            IsSuccess = true;
            Error = default;
        }

        protected Result(Error error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result Success() => new();
        public static Result Failure(Error error) => new(error);
    }

    public sealed class Result<TValue> : Result
    {
        private readonly TValue? _value;

        public TValue Value =>
            IsSuccess ? _value! 
            : throw new InvalidOperationException("Значение не может быть получено при отрицательном флаге IsSuccess");

        private Result(TValue value) : base() => _value = value;
        private Result(Error error) : base(error) => _value = default;

        public static Result<TValue> Success(TValue value) => new(value);
        public static Result<TValue> Failure(Error error) => new(error);
    }
}
