using API.Core.Models;
using API.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository(GymClubContext context) : BaseRepository<User>(context)
    {
        private readonly GymClubContext context = context;

        public new async Task<IEnumerable<User>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
        {
            var users = await context.Users.ToListAsync();
            return users.Where(u => u.PhoneNumber == phoneNumber).FirstOrDefault();
        }

        public async Task<User?> GetUserByRefreshToken(string refreshToken)
        {
            var users = await context.Users.ToListAsync();
            return users.Where(u => u.RefreshToken == refreshToken).FirstOrDefault();
        }
    }
}
