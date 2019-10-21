using Microsoft.Extensions.Caching.Memory;

namespace Jaya.Shared.Services
{
    public sealed class MemoryCacheService: IService
    {
        readonly MemoryCache _cache;

        public MemoryCacheService()
        {
            var options = new MemoryCacheOptions();

            _cache = new MemoryCache(options);
        }

        ~MemoryCacheService()
        {
            _cache.Dispose();
        }

        public long Count => _cache.Count;

        public bool TryGetValue<T>(object key, out T result)
        {
            return _cache.TryGetValue<T>(key, out result);
        }

        public void Set<T>(object key, T value)
        {
            _cache.Set<T>(key, value);
        }
    }
}
