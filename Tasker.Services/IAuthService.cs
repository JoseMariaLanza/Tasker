using Tasker.Repositories.Auth.Models;

namespace Tasker.Services
{
    public interface IAuthService
    {
        Task<User?> GetUser(string key);
    }
}
