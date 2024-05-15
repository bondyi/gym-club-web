using API.Core.Interfaces;
using API.Core.Models;
using API.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository(GymClubContext context) : Repository<User>(context), IRepository<User>
    {
        public async override Task<IEnumerable<User>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var users = await GetAll();
            var user = users.Where(u => u.PhoneNumber == phoneNumber).FirstOrDefault();
            return user;
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            return await context.Users.FirstAsync(e => e.RefreshToken == refreshToken);
        }
    }
}
