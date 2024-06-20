namespace NexusAPI.Service.IService
{
    public interface ICachingService
    {


        public Task<bool> Cache(string id, object body, TimeSpan timeToLive);

        public Task<string> GetCached(string id);
    }
}
