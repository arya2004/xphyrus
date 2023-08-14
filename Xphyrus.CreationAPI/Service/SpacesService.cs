using StackExchange.Redis;
using System.Text.Json;
using Xphyrus.CreationAPI.Models;
using Xphyrus.CreationAPI.Service.IService;

namespace Xphyrus.CreationAPI.Service
{
    public class SpacesService : ISpacesService
    {   
        private readonly IDatabase _database;
        public SpacesService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<Spaces> CreateSpace(Spaces spaces)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteSpace(string spacesId)
        {
            return await _database.KeyDeleteAsync(spacesId);
        }

        public async Task<Spaces> GetSpaces(string spackId)
        {
            var data = await _database.StringGetAsync(spackId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Spaces>(data);
        }

        public async Task<Spaces> UpdateSpace(Spaces spaces)
        {
            var created = await _database.StringSetAsync(spaces.Id, JsonSerializer.Serialize(spaces), TimeSpan.FromDays(30000));
            if (!created) return null;
            return await GetSpaces(spaces.Id);
        }
    }
}
