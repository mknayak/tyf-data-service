using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

public class CacheManager : ICacheManager
{
    private readonly IMemoryCache _cache;
    private List<string> _keys;

    public CacheManager(IMemoryCache cache)
    {
        _cache = cache;
        _keys = new List<string>();
    }
    public T Get<T>(string key)
    {
        return _cache.Get<T>(key)!;
    }
    public T GetOrCreate<T>(string key, Func<T> createItem){
        return _cache.GetOrCreate<T>(key,(key)=>createItem())!;
    }
    public void Set(string key, object data, int cacheTime)
    {
        _cache.Set(key, data, TimeSpan.FromMinutes(cacheTime));
        _keys.Add(key);
    }

    public bool IsSet(string key)
    {
        return _cache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
       var matchedKeys = _keys.Where(k => k.Contains(pattern)).ToList();
         foreach (var key in matchedKeys)
         {
              _cache.Remove(key);
         }
    }

    public void Clear()
    {
        var _memoryCache = _cache as MemoryCache;
        if (_memoryCache != null)
            _memoryCache.Compact(100);
    }
}

