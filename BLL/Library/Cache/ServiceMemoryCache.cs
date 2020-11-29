using Microsoft.Extensions.Caching.Memory;
using PagarMe;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Library.Cache
{
    public class ServiceMemoryCache<TItem> : IServiceMemoryCache<TItem>
    {
        private readonly IMemoryCache _cache;
        private ConcurrentDictionary<object, SemaphoreSlim> _locks;

        public ServiceMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
            _locks = new ConcurrentDictionary<object, SemaphoreSlim>();
        }

        public async Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem)
        {
            TItem cacheEntry;

            if (!_cache.TryGetValue(key, out cacheEntry))// Look for cache key.
            {
                SemaphoreSlim mylock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();
                try
                {
                    if (!_cache.TryGetValue(key, out cacheEntry))
                    {
                        // Key not in cache, so get data.
                        cacheEntry = await createItem(); ;
                        _cache.Set(key, cacheEntry);
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }
    }
}
