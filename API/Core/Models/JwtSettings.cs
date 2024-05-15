namespace API.Core.Models
{
    public class JwtSettings
    {
        public required string Secret { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required int TokenExpirationInDays { get; set; }
    }
}
