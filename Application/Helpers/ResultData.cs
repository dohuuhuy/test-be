
namespace Application
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public IDictionary<string, object>? Exc { get; }

        public Result(T? data = default, string? message = default)
        {
            Data = data;
            Message = message;
        }

        public Result(IDictionary<string, object>? exc)
        {
            Exc = exc;
        }
    }
}
