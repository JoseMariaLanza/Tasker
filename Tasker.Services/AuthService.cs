using AuthOrchestrator.Redis;
using StackExchange.Redis;
using System.Text.Json;
using Tasker.Repositories.Auth;
using Tasker.Repositories.Auth.Models;

namespace Tasker.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDatabase _database;

        public AuthService(IRedisHelper redisHelper, IAuthRepository authRepository)
        {
            _database = redisHelper.GetDatabase();
        }

        public async Task<User?> GetUser(string userToken)
        {
            var userData = _database.StringGet(userToken);
            if (userData.IsNullOrEmpty) return null;

            var userModel = JsonSerializer.Deserialize<User>(userData);

            return userModel;
        }
    }
}