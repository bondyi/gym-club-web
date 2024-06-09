using API.Core.Dto;
using API.Core.Interfaces;
using API.Core.Models;
using API.Repositories;

namespace API.Services
{
    public class UserService(GymClubContext context) : IService<UserDto>
    {
        private readonly UserRepository _repository = new(context);

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<UserDto?> Get(int id)
        {
            var user = await _repository.Get(id) ?? throw new NullReferenceException();

            return new UserDto()
            {
                UserId = user.UserId,
                UserRole = user.UserRole,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name!,
                Surname = user.Surname!,
                BirthDate = (DateOnly)user.BirthDate!,
                Gender = (bool)user.Gender!
            };
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _repository.GetAll();

            List<UserDto> userDtos = [];
            foreach (var user in users)
            {
                userDtos.Add(new UserDto()
                {
                    UserId = user.UserId,
                    UserRole = user.UserRole,
                    PhoneNumber = user.PhoneNumber,
                    Name = user.Name!,
                    Surname = user.Surname!,
                    BirthDate = (DateOnly)user.BirthDate!,
                    Gender = (bool)user.Gender!
                });
            }

            return userDtos;
        }

        public Task Post(UserDto entity)
        {
            throw new NotSupportedException();
        }

        public async Task Put(int id, UserDto entity)
        {
            var user = await _repository.Get(id) ?? throw new NullReferenceException();

            if (entity.UserId != user.UserId) throw new Exception("Invalid Id");

            user.UserId = entity.UserId;
            user.UserRole = entity.UserRole;
            user.PhoneNumber = entity.PhoneNumber;
            user.Name = entity.Name;
            user.Surname = entity.Surname;
            user.BirthDate = entity.BirthDate;
            user.Gender = entity.Gender;

            await _repository.Put(id, user);
        }
    }
}
