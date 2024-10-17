namespace Application
{
    public class BlogEntity
    {
        public int id { get; set; }
        public required string title { get; set; }
        public string? content { get; set; }
    }
}
