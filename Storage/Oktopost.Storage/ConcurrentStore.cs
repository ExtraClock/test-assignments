namespace Oktopost.Storage;

// Thread-safe store
// There is not much we can do to reduce lock contention without a trade-off
// A lock-free (almost) ConcurrentDictionary won't help because we still have to write-lock LinkedList even for storage-reads
public class ConcurrentStore(int maxItems) : IStore
{
    public class Factory : IStoreFactory<ConcurrentStore>
    {
        public ConcurrentStore Create(int maxItems)
        {
            return new ConcurrentStore(maxItems);
        }
    }

    private readonly ThreadLocalStoreV2 _backingStore = new(maxItems);

    public string? Get(string key)
    {
        lock (_backingStore) return _backingStore.Get(key);
    }

    public void Put(string key, string item)
    {
        lock (_backingStore) _backingStore.Put(key, item);
    }
}
