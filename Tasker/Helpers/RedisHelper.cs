//using StackExchange.Redis;
//using Tasker.Services;

//namespace Tasker.Helpers
//{
//    public class RedisHelper : IRedisHelper
//    {
//        private readonly ConnectionMultiplexer _redisConnection;

//        public RedisHelper(string connectionString)
//        {
//            _redisConnection = ConnectionMultiplexer.Connect(connectionString);
//        }

//        public IDatabase GetDatabase() => _redisConnection.GetDatabase();
//    }
//}
