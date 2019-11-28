using Microsoft.Extensions.Caching.Memory;
using System.Composition;

namespace Jaya.Shared.Services
{
    [Export(nameof(MemoryCacheService), typeof(IService))]
    [Shared]
    public sealed class MemoryCacheService: IService
    {
        MemoryCache _cache;

        ~MemoryCacheService()
        {
            _cache.Dispose();
        }

        #region properties

        MemoryCache Cache
        {
            get
            {
                if (_cache == null)
                {
                    var options = new MemoryCacheOptions();
                    _cache = new MemoryCache(options);
                }

                return _cache;
            }
        }

        public long Count => Cache.Count;

        #endregion

        public bool TryGetValue<T>(object key, out T result)
        {
            return Cache.TryGetValue(key, out result);
        }

        public void Set<T>(object key, T value)
        {
            Cache.Set(key, value);
        }
    }
}
