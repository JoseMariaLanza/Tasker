using Tasker.Repositories.Auth.Models;

namespace Tasker.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<User> GetUserByIdAsync(int id);

        Task<User> CreateUserWithIdAsync(User user);

        Task<User> UpdateUserAsync(User user);
    }
}
