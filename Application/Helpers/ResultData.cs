namespace Application
{
    public class Result<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }

        public Result(T? data = default, string? message = default)
        {
            Data = data;
            Message = message;
        }
    }
}
