``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.1702/22H2/2022Update/SunValley2)
12th Gen Intel Core i7-12700K, 1 CPU, 20 logical and 12 physical cores
.NET SDK=7.0.203
  [Host]   : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  ShortRun : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                    Method |       Mean |     Error |  StdDev | Allocated |
|-------------------------- |-----------:|----------:|--------:|----------:|
|                MeltySynth |   668.7 ms | 151.51 ms | 8.30 ms |     600 B |
| MeltySynthReverbAndChorus | 1,120.9 ms | 108.53 ms | 5.95 ms |     600 B |
|               CSharpSynth | 1,594.6 ms |  36.66 ms | 2.01 ms | 1091408 B |
