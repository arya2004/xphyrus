using System;
using System.Threading.Tasks;

namespace AssessmentAPI.Service.IService
{
    /// <summary>
    /// Interface for the caching service.
    /// </summary>
    public interface ICachingService
    {
        /// <summary>
        /// Caches an object with a specified time to live.
        /// </summary>
        /// <param name="id">The identifier for the cached item.</param>
        /// <param name="body">The object to cache.</param>
        /// <param name="timeToLive">The time to live for the cached item.</param>
        /// <returns>A task representing the asynchronous operation, with a result indicating success or failure.</returns>
        Task<bool> CacheAsync(string id, object body, TimeSpan timeToLive);

        /// <summary>
        /// Retrieves a cached item by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the cached item.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the cached item as a string.</returns>
        Task<string> GetCachedAsync(string id);
    }
}
