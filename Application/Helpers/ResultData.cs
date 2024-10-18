
using System.Dynamic;

namespace Application
{
    public class Result<T>
    {
        private Dictionary<string, object>? dictionary;
        private ExpandoObject? exc;

        public T? Data { get; set; }
        public string? Message { get; set; }

        public Result(T? data = default, string? message = default)
        {
            Data = data;
            Message = message;
        }

        public Result(Dictionary<string, object>? dictionary)
        {
            this.dictionary = dictionary;
        }

        public Result(ExpandoObject? exc)
        {
            this.exc = exc;
        }
    }
}
