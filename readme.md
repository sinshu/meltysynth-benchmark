``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1237 (21H1/May2021Update)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.7.21379.14
  [Host]   : .NET Core 3.1.18 (CoreCLR 4.700.21.35901, CoreFX 4.700.21.36305), X64 RyuJIT
  ShortRun : .NET Core 3.1.18 (CoreCLR 4.700.21.35901, CoreFX 4.700.21.36305), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                    Method |    Mean |    Error |   StdDev |   Allocated |
|-------------------------- |--------:|---------:|---------:|------------:|
|                MeltySynth | 1.267 s | 0.2200 s | 0.0121 s |           - |
| MeltySynthReverbAndChorus | 2.039 s | 0.2628 s | 0.0144 s |           - |
|               CSharpSynth | 2.504 s | 0.6264 s | 0.0343 s | 1,063,944 B |
