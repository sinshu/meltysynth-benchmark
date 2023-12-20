``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22631.2861)
12th Gen Intel Core i7-12700K, 1 CPU, 20 logical and 12 physical cores
.NET SDK=8.0.100
  [Host]   : .NET 7.0.14 (7.0.1423.51910), X64 RyuJIT AVX2
  ShortRun : .NET 7.0.14 (7.0.1423.51910), X64 RyuJIT AVX2

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|       Method |    Mean |    Error |   StdDev | Allocated |
|------------- |--------:|---------:|---------:|----------:|
|     Original | 1.143 s | 0.1504 s | 0.0082 s |     600 B |
| MemoryMapOff | 1.373 s | 0.0702 s | 0.0038 s |     600 B |
|  MemoryMapOn | 1.327 s | 0.1516 s | 0.0083 s |     600 B |
