//using Microsoft.EntityFrameworkCore;
//using StackExchange.Redis;
//using Tasker.Repositories;
//using Tasker.Repositories.Auth.Models;
//using System.Text.Json;

//namespace Tasker.Services
//{
//    public class RedisService : IRedisService
//    {
//        private readonly IDatabase _database;
//        private readonly IAuthDbContext _authDbContext;

//        public RedisService(IRedisHelper redisHelper, IAuthDbContext authDbContext)
//        {
//            _database = redisHelper.GetDatabase();
//            _authDbContext = authDbContext;
//        }

//        public async Task<User?> GetUser(string userToken)
//        {
//            var userData = _database.StringGet(userToken);
//            if (userData.IsNullOrEmpty) return null;

//            var userModel = JsonSerializer.Deserialize<User>(userData);

//            var existingUser = _authDbContext.Users.Find(userModel.Id);

//            if (existingUser is null)
//            {
//                _authDbContext.Users.Add(userModel);
//            }
//            else
//            {
//                _authDbContext.Users.Update(existingUser);

//                //existingUser.Id = userModel.Id;
//                //existingUser.UserName = userModel.UserName;
//                //existingUser.FirstName = userModel.FirstName;
//                //existingUser.LastName = userModel.LastName;
//                //existingUser.Emails = userModel.Emails;
//                //existingUser.UserType = userModel.UserType;
//            }

//            //await _authDbContext.Users.Update(userModel);

//            return userModel;
//        }
//    }
//}