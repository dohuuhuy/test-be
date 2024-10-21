namespace Application
{
    public class Result
    {
        public dynamic? Data { get; set; }
        public string? Message { get; set; }

        public Result(dynamic? data = default, string? message = default)
        {
            Data = data;
            Message = message;
        }

        public Result(string? message = default)
        {
            Message = message;
        }
    }
}
