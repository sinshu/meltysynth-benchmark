``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.928 (2004/?/20H1)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.3.21202.5
  [Host]   : .NET Core 3.1.14 (CoreCLR 4.700.21.16201, CoreFX 4.700.21.16208), X64 RyuJIT
  ShortRun : .NET Core 3.1.14 (CoreCLR 4.700.21.16201, CoreFX 4.700.21.16208), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|         Method |    Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |--------:|---------:|---------:|------:|------:|------:|----------:|
|  RunMeltySynth | 1.288 s | 0.0222 s | 0.0012 s |     - |     - |     - |         - |
| RunCSharpSynth | 2.460 s | 0.2270 s | 0.0124 s |     - |     - |     - | 1063944 B |
