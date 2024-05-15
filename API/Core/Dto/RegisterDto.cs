namespace API.Core.Dto
{
    public sealed class RegisterDto : LoginDto
    {
        public required string UserRole { get; set; }
    }
}
