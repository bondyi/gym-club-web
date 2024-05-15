namespace API.Core.Models.Responses
{
    public class AuthResponse(string message, string accessToken, string refreshToken) : Response(message)
    {
        public string AccessToken { get; set; } = accessToken;
        public string RefreshToken { get; set; } = refreshToken;
    }
}
