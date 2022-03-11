using BenchmarkDotNet.Running;
using ThirteenBase.Benchmark;

_ = BenchmarkRunner.Run<SolutionComparison>();

/*

// * Summary *

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1586 (21H2)
Intel Core i7-9700 CPU 3.00GHz, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT


|                Method |       Mean |     Error |    StdDev | Ratio | Allocated |
|---------------------- |-----------:|----------:|----------:|------:|----------:|
|   OnlineCacheTopLevel | 300.372 ms | 1.6331 ms | 1.3637 ms | 1.000 |      4 KB |
| OnlineCacheMultiLevel |  10.244 ms | 0.0566 ms | 0.0530 ms | 0.034 |     14 KB |
|           StaticCache |   2.133 ms | 0.0131 ms | 0.0123 ms | 0.007 |      2 KB |

*/
