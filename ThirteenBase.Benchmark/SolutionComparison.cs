using BenchmarkDotNet.Attributes;

namespace ThirteenBase.Benchmark;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "BechmarkDotNet won't accept a static method as a benchmark")]
[MemoryDiagnoser]
public class SolutionComparison
{
    // PlainStupid тестировать бесмысленно в виду запредельного времени выполнения

    [Benchmark(Baseline = true)]
    public long OnlineCacheTopLevel() => new OnlineCacheTopLevel().Solve();

    [Benchmark]
    public long OnlineCacheMultiLevel() => new OnlineCacheMultiLevel().Solve();

    [Benchmark]
    public long StaticCache() => new StaticCache().Solve();
}