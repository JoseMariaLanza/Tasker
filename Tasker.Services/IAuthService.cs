//using StackExchange.Redis;
using Tasker.Repositories.Auth.Models;

namespace Tasker.Services
{
    //public interface IRedisHelper
    //{
    //    IDatabase GetDatabase();
    //}

    public interface IAuthService
    {
        Task<User?> GetUser(string key);
    }
}
