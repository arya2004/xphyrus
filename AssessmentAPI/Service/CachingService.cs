using AssessmentAPI.Service.IService;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AssessmentAPI.Service
{
    /// <summary>
    /// Service class for handling caching operations using Redis.
    /// </summary>
    public class CachingService : ICachingService
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingService"/> class.
        /// </summary>
        /// <param name="redis">The Redis connection multiplexer.</param>
        /// <exception cref="ArgumentNullException">Thrown when the redis parameter is null.</exception>
        public CachingService(IConnectionMultiplexer redis)
        {
            _database = redis?.GetDatabase() ?? throw new ArgumentNullException(nameof(redis), "Redis connection cannot be null.");
        }

        /// <summary>
        /// Caches an object with a specified time to live.
        /// </summary>
        /// <param name="id">The identifier for the cached item.</param>
        /// <param name="body">The object to cache.</param>
        /// <param name="timeToLive">The time to live for the cached item.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating success or failure.</returns>
        public async Task<bool> CacheAsync(string id, object body, TimeSpan timeToLive)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Cache ID cannot be null or empty.", nameof(id));
            }

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Cache body cannot be null.");
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var serializedResp = JsonSerializer.Serialize(body, options);
                return await _database.StringSetAsync(id, serializedResp, timeToLive);
            }
            catch (Exception ex)
            {
                // Log the exception details for further analysis (not implemented here)
                // For example: _logger.LogError(ex, "Error occurred while caching data.");
                return false;
            }
        }

        /// <summary>
        /// Retrieves a cached item by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the cached item.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the cached item as a string.</returns>
        public async Task<string> GetCachedAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Cache ID cannot be null or empty.", nameof(id));
            }

            try
            {
                var cachedResp = await _database.StringGetAsync(id);
                return cachedResp.ToString();
            }
            catch (Exception ex)
            {
                // Log the exception details for further analysis (not implemented here)
                // For example: _logger.LogError(ex, "Error occurred while retrieving cached data.");
                return null;
            }
        }
    }
}
