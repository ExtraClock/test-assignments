using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Oktopost.Storage;
using Oktopost.Storage.Benchmarks;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("Error", "StdDev", "Median", "RatioSD")]
public class Benchmark
{
    private readonly string[] _keys;
    private readonly string[] _values;

    private const int TestDataSize = 500_000;

    public Benchmark()
    {
        var rnd = new Random(1234);
        _keys = Enumerable.Range(1, TestDataSize * 2) // double size to have enough after distinct
            .Select(_ => rnd.Next().ToString())
            .Distinct() // remove duplicates
            .Take(TestDataSize)
            .ToArray();
        _values = Enumerable.Range(1, TestDataSize)
            .Select(_ => rnd.Next().ToString())
            .ToArray();
    }

    private const int Capacity = 10_000;

    private IStore Test<TStore, TStoreFactory>(int putCount)
        where TStore : IStore
        where TStoreFactory : IStoreFactory<TStore>, new()
    {
        var store = new TStoreFactory().Create(Capacity);
        for (var i = 0; i < putCount; i++)
        {
            store.Put(_keys[i], _values[i]);
        }

        for (var i = 0; i < TestDataSize; i++)
        {
            var value = store.Get(_keys[i]);
            if (value != null && value != _values[i])
            {
                throw new UnreachableException($"value={value}; values[i]={_values[i]}");
            }
        }

        return store;
    }

    // ===========================================================================================
    // tests without reaching the limit

    [Benchmark]
    public IStore UnboundDictionary() => Test<DictionaryStore, DictionaryStore.Factory>(putCount: Capacity);

    [Benchmark]
    public IStore Unbound() => Test<ThreadLocalStore, ThreadLocalStore.Factory>(putCount: Capacity);

    [Benchmark]
    public IStore UnboundV2() => Test<ThreadLocalStoreV2, ThreadLocalStoreV2.Factory>(putCount: Capacity);

    [Benchmark]
    public IStore UnboundConcurrentDictionary() =>
        Test<ConcurrentDictionaryStore, ConcurrentDictionaryStore.Factory>(putCount: Capacity);

    [Benchmark]
    public IStore UnboundConcurrent() => Test<ConcurrentStore, ConcurrentStore.Factory>(putCount: Capacity);

    // ===========================================================================================
    // tests what happens once the limit is reached

    [Benchmark]
    public IStore BoundDictionary() => Test<DictionaryStore, DictionaryStore.Factory>(putCount: TestDataSize);

    [Benchmark]
    public IStore Bound() => Test<ThreadLocalStore, ThreadLocalStore.Factory>(putCount: TestDataSize);

    [Benchmark]
    public IStore BoundV2() => Test<ThreadLocalStoreV2, ThreadLocalStoreV2.Factory>(putCount: TestDataSize);

    [Benchmark]
    public IStore BoundConcurrentDictionary() =>
        Test<ConcurrentDictionaryStore, ConcurrentDictionaryStore.Factory>(putCount: TestDataSize);

    [Benchmark]
    public IStore BoundConcurrent() => Test<ConcurrentStore, ConcurrentStore.Factory>(putCount: TestDataSize);
}