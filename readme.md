``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.2130 (21H1/May2021Update)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100-preview.4.22252.9
  [Host]   : .NET Core 3.1.30 (CoreCLR 4.700.22.47601, CoreFX 4.700.22.47602), X64 RyuJIT
  ShortRun : .NET Core 3.1.30 (CoreCLR 4.700.22.47601, CoreFX 4.700.22.47602), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                    Method |    Mean |    Error |   StdDev |   Allocated |
|-------------------------- |--------:|---------:|---------:|------------:|
|                MeltySynth | 1.130 s | 0.1895 s | 0.0104 s |           - |
| MeltySynthReverbAndChorus | 1.884 s | 0.2022 s | 0.0111 s |           - |
|               CSharpSynth | 2.429 s | 0.1492 s | 0.0082 s | 1,063,944 B |
