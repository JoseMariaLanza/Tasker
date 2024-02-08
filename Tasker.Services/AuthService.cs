using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Tasker.Repositories.Auth.Models;
using System.Text.Json;
using AuthOrchestrator.Redis;
using Tasker.Repositories.Auth;

namespace Tasker.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDatabase _database;
        private IAuthRepository _authRepository;

        public AuthService(IRedisHelper redisHelper, IAuthRepository authRepository)
        {
            _database = redisHelper.GetDatabase();
            _authRepository = authRepository;
        }

        public async Task<User?> GetUser(string userToken)
        {
            var userData = _database.StringGet(userToken);
            if (userData.IsNullOrEmpty) return null;

            var userModel = JsonSerializer.Deserialize<User>(userData);

            return userModel;

            var existingUser = await _authRepository.GetUserByIdAsync(userModel.Id);

            User newOrUpdatedUser;
            if (existingUser is null)
            {
                newOrUpdatedUser = await _authRepository.CreateUserWithIdAsync(userModel);
            }
            else
            {
                newOrUpdatedUser = await _authRepository.UpdateUserAsync(existingUser);

                //existingUser.Id = userModel.Id;
                //existingUser.UserName = userModel.UserName;
                //existingUser.FirstName = userModel.FirstName;
                //existingUser.LastName = userModel.LastName;
                //existingUser.Emails = userModel.Emails;
                //existingUser.UserType = userModel.UserType;
            }

            //await _authDbContext.Users.Update(userModel);

            //return userModel;
            return newOrUpdatedUser;
        }
    }
}