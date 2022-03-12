```
// * Summary *

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1586 (21H2)
Intel Core i7-9700 CPU 3.00GHz, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.101
[Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
```

| Method                |      Mean |    Error |   StdDev | Ratio | Allocated |
|-----------------------|----------:|---------:|---------:|------:|----------:|
| OnlineCacheTopLevel   | 479.99 ms | 2.941 ms | 2.751 ms |  1.00 |      2 KB |
| OnlineCacheMultiLevel | 142.04 ms | 0.869 ms | 0.813 ms |  0.30 |     17 KB |
| StaticCache           |  28.58 ms | 0.551 ms | 0.566 ms |  0.06 |      2 KB |
