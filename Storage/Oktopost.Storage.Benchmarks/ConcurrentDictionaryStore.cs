using System.Collections.Concurrent;

namespace Oktopost.Storage.Benchmarks;

public class ConcurrentDictionaryStore(int maxItems) : IStore
{
    public class Factory : IStoreFactory<ConcurrentDictionaryStore>
    {
        public ConcurrentDictionaryStore Create(int maxItems)
        {
            return new ConcurrentDictionaryStore(maxItems);
        }
    }

    private readonly ConcurrentDictionary<string, string> _dictionary = new(concurrencyLevel: -1, capacity: maxItems);


    public string? Get(string key) => _dictionary.GetValueOrDefault(key);
    public void Put(string key, string item) => 
        _dictionary.AddOrUpdate(key, (_) => item, (_, _) => item);
}