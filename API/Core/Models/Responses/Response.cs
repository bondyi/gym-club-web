namespace API.Core.Models.Responses
{
    public class Response(string message)
    {
        public string Message { get; set; } = message;
    }
}
