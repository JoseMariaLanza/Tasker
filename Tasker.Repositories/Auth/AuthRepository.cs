using Microsoft.EntityFrameworkCore;
using Tasker.Repositories.Auth.Models;

namespace Tasker.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _authDbContext;

        public AuthRepository(AuthDbContext authDbContext) 
        {
            _authDbContext = authDbContext;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _authDbContext.Users.FindAsync(id);

            return user;
        }

        public async Task<User> CreateUserWithIdAsync(User user)
        {
            _authDbContext.Users.Add(user);
            await _authDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _authDbContext.Users.Update(user);
            await _authDbContext.SaveChangesAsync();
            return user;
        }
    }
}
