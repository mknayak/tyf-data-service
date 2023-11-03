/// <summary>
/// Provides an interface for caching data.
/// </summary>
public interface ICacheManager
{
    /// <summary>
    /// Gets the cached item with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the cached item.</typeparam>
    /// <param name="key">The key of the cached item.</param>
    /// <returns>The cached item, or default(T) if the item is not found.</returns>
    T Get<T>(string key);

    /// <summary>
    /// Gets the cached item with the specified key, or creates and caches a new item if the key is not found.
    /// </summary>
    /// <typeparam name="T">The type of the cached item.</typeparam>
    /// <param name="key">The key of the cached item.</param>
    /// <param name="createItem">A function that creates the new item to be cached.</param>
    /// <returns>The cached item, either retrieved or newly created.</returns>
    T GetOrCreate<T>(string key, Func<T> createItem);

    /// <summary>
    /// Sets a new item to be cached with the specified key and cache time.
    /// </summary>
    /// <param name="key">The key of the item to be cached.</param>
    /// <param name="data">The data to be cached.</param>
    /// <param name="cacheTime">The time in seconds for which the item should be cached.</param>
    void Set(string key, object data, int cacheTime);

    /// <summary>
    /// Determines whether an item with the specified key exists in the cache.
    /// </summary>
    /// <param name="key">The key of the item to check.</param>
    /// <returns>True if the item exists in the cache, otherwise false.</returns>
    bool IsSet(string key);

    /// <summary>
    /// Removes the item with the specified key from the cache.
    /// </summary>
    /// <param name="key">The key of the item to remove.</param>
    void Remove(string key);

    /// <summary>
    /// Removes all items from the cache that match the specified pattern.
    /// </summary>
    /// <param name="pattern">The pattern to match against the cache keys.</param>
    void RemoveByPattern(string pattern);

    /// <summary>
    /// Clears all items from the cache.
    /// </summary>
    void Clear();
}