namespace API.Core.Dto
{
    public class UserDto
    {
        public required int UserId { get; set; }

        public required string UserRole { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public required DateOnly BirthDate { get; set; }

        public required bool Gender { get; set; }
    }
}
