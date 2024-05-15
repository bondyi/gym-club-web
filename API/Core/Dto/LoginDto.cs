namespace API.Core.Dto
{
    public class LoginDto
    {
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
    }
}
