namespace Oktopost.Storage;

// Non-thread-safe implementation
public class ThreadLocalStore : IStore
{
    public class Factory : IStoreFactory<ThreadLocalStore>
    {
        public ThreadLocalStore Create(int maxItems)
        {
            return new ThreadLocalStore(maxItems);
        }
    }
    
    private class Entry
    {
        // double linked list entry
        public Entry? Prev { get; set; } // to the head
        public Entry? Next { get; set; } // to the tail

        public string? Key { get; set; } // the key has to be stored twice since we need to address it in the dictionary
        public string? Value { get; set; } // payload
    }

    private Entry? _head = null; // the head of the double-linked list
    private Entry? _tail = null; // the tail of the double-linked list
    private readonly Dictionary<string, Entry> _dictionary;
    private readonly int _maxItems;

    public ThreadLocalStore(int maxItems)
    {
        if (maxItems <= 0) throw new ArgumentException("maxItems should be greater than zero", nameof(maxItems));
        _maxItems = maxItems;
        _dictionary = new(capacity: maxItems);
    }

    public string? Get(string key)
    {
        if (!_dictionary.TryGetValue(key, out var existingEntry))
        {
            return null; // the key doesn't exists
        }
        
        MakeHead(existingEntry); // we should put the entry in the head of the double-linked list
        return existingEntry.Value;
    }

    public void Put(string key, string item)
    {
        if (_dictionary.TryGetValue(key, out var existingEntry))
        {
            // key already exists, update its payload in-place
            existingEntry.Value = item;

            // we should put the entry in the head of the double-linked list
            MakeHead(existingEntry);

            return;
        }

        // the key doesn't exist yet

        var capacityLimitIsReached = _dictionary.Count == _maxItems;
        if (!capacityLimitIsReached)
        {
            // capacity limit is not reached yet - we can just add the key and make it the new head
            Entry newEntry = new() { Key = key, Value = item };
            MakeHead(newEntry); // make it the new head
            _dictionary.Add(key, newEntry);
            return;
        }

        // the key doesn't exist yet AND we've already reached the capacity limit
        // we should remove the tail and add the new key in its place
        
        if (_tail == null)
            throw new InvalidOperationException("Internal structure corruption detected.");
        if (_tail.Key == null)
            throw new InvalidOperationException("Internal structure corruption detected.");
        _dictionary.Remove(_tail.Key);
        var reusedEntry = _tail; // we will reuse the removed entry to avoid unnecessary allocations
        
        // move tail by one position
        if (_tail.Prev != null) _tail.Prev.Next = null;
        _tail = _tail.Prev;
        
        // reinitialize reused entry and make it the new head
        reusedEntry.Key = key;
        reusedEntry.Value = item;
        reusedEntry.Prev = null;
        reusedEntry.Next = null;
        MakeHead(reusedEntry); // make it the new head
        _dictionary.Add(key, reusedEntry);
    }

    private void MakeHead(Entry entry)
    {
        if (_head == entry)
        {
            // the entry is already at the head
            return;
        }

        // remove the entry from its current position in the double-linked list
        if (entry.Prev != null)
        {
            if (entry.Prev.Next != entry)
                throw new InvalidOperationException("Internal structure corruption detected.");
            entry.Prev.Next = entry.Next;
        }

        if (entry.Next != null)
        {
            if (entry.Next.Prev != entry)
                throw new InvalidOperationException("Internal structure corruption detected.");
            entry.Next.Prev = entry.Prev;
        }
        else if (_tail == entry)
        {
            // the entry previously was at the tail of the double-linked list
            _tail = entry.Prev;
        }

        // put the entry at the head of the double-linked list
        entry.Prev = null;
        entry.Next = _head;
        if (_head != null)
            _head.Prev = entry;
        _head = entry;
        _tail ??= entry;
    }
}
