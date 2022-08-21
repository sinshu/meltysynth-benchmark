``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1889 (21H1/May2021Update)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100-preview.4.22252.9
  [Host]   : .NET Core 3.1.27 (CoreCLR 4.700.22.30802, CoreFX 4.700.22.31504), X64 RyuJIT
  ShortRun : .NET Core 3.1.27 (CoreCLR 4.700.22.30802, CoreFX 4.700.22.31504), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                    Method |    Mean |    Error |   StdDev |   Allocated |
|-------------------------- |--------:|---------:|---------:|------------:|
|                MeltySynth | 1.137 s | 0.1871 s | 0.0103 s |           - |
| MeltySynthReverbAndChorus | 1.891 s | 0.2540 s | 0.0139 s |           - |
|               CSharpSynth | 2.440 s | 0.1687 s | 0.0092 s | 1,063,944 B |
