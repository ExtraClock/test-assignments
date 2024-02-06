namespace Oktopost.Storage.Benchmarks;

public class DictionaryStore(int maxItems) : IStore
{
    public class Factory : IStoreFactory<DictionaryStore>
    {
        public DictionaryStore Create(int maxItems)
        {
            return new DictionaryStore(maxItems);
        }
    }

    private readonly Dictionary<string, string> _dictionary = new(capacity: maxItems);


    public string? Get(string key) => _dictionary.GetValueOrDefault(key);
    public void Put(string key, string item) => _dictionary.Add(key, item);
}