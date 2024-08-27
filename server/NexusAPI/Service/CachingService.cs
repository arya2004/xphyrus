using NexusAPI.Service.IService;
using StackExchange.Redis;
using System.Text.Json;

namespace NexusAPI.Service
{
    public class CachingService : ICachingService
    {
        private readonly IDatabase _database;
        public CachingService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> Cache(string id, object body, TimeSpan timeToLive)
        {
            if (body == null)
            {
                return false;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var serializedResp = JsonSerializer.Serialize(body, options);

            await _database.StringSetAsync(id, serializedResp, timeToLive);

            return true;
        }



        public async Task<string> GetCached(string id)
        {
            var cachedResp = await _database.StringGetAsync(id);
            return cachedResp.ToString();
        }


    }
}
