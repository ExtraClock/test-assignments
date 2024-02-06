namespace Oktopost.Storage;

// Non-thread-safe implementation
// The difference to V1 implementation is that this one uses library LinkedList instead of manual implementation
public class ThreadLocalStoreV2 : IStore
{
    public class Factory : IStoreFactory<ThreadLocalStoreV2>
    {
        public ThreadLocalStoreV2 Create(int maxItems)
        {
            return new ThreadLocalStoreV2(maxItems);
        }
    }
    
    private readonly Dictionary<string, LinkedListNode<KeyValuePair<string, string>>> _dictionary;
    private readonly LinkedList<KeyValuePair<string, string>> _linkedList;
    private readonly int _maxItems;

    public ThreadLocalStoreV2(int maxItems)
    {
        if (maxItems <= 0) throw new ArgumentException("maxItems should be greater than zero", nameof(maxItems));
        _maxItems = maxItems;
        _dictionary = new(capacity: maxItems);
        _linkedList = new();
    }

    public string? Get(string key)
    {
        if (!_dictionary.TryGetValue(key, out var existingEntry))
        {
            return null; // the key doesn't exists
        }

        _linkedList.Remove(existingEntry);
        _linkedList.AddFirst(existingEntry);
        return existingEntry.Value.Value;
    }

    public void Put(string key, string item)
    {
        if (_dictionary.TryGetValue(key, out var existingEntry))
        {
            // key already exists, update its payload in-place
            existingEntry.Value = new (key, item);

            // we should put the entry in the head of the double-linked list
            _linkedList.Remove(existingEntry);
            _linkedList.AddFirst(existingEntry);

            return;
        }

        // the key doesn't exist yet

        var capacityLimitIsReached = _dictionary.Count == _maxItems;
        if (!capacityLimitIsReached)
        {
            // capacity limit is not reached yet - we can just add the key and make it the new head
            var newEntry = _linkedList.AddFirst(new KeyValuePair<string, string>(key, item));
            _dictionary.Add(key, newEntry);
            return;
        }

        // the key doesn't exist yet AND we've already reached the capacity limit
        // we should remove the tail and add the new key in its place

        var last = _linkedList.Last;
        if (last == null)
            throw new InvalidOperationException("Internal structure corruption detected.");
        _linkedList.RemoveLast();
        _dictionary.Remove(last.Value.Key);
        
        // we will reuse the removed entry to avoid unnecessary allocations
        var reusedEntry = last;
        reusedEntry.Value = new(key, item);
        _linkedList.AddFirst(reusedEntry);
        _dictionary.Add(key, reusedEntry);
    }
}
