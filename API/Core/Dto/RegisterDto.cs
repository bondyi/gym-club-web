namespace API.Core.Dto
{
    public sealed class RegisterDto : LoginDto
    {
        public required string UserRole { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public required DateOnly BirthDate { get; set; }

        public required bool Gender { get; set; }
    }
}
