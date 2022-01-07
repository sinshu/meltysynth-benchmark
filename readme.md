``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1415 (21H1/May2021Update)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]   : .NET Core 3.1.21 (CoreCLR 4.700.21.51404, CoreFX 4.700.21.51508), X64 RyuJIT
  ShortRun : .NET Core 3.1.21 (CoreCLR 4.700.21.51404, CoreFX 4.700.21.51508), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                    Method |    Mean |    Error |   StdDev |   Allocated |
|-------------------------- |--------:|---------:|---------:|------------:|
|                MeltySynth | 1.235 s | 0.2303 s | 0.0126 s |           - |
| MeltySynthReverbAndChorus | 1.983 s | 0.3909 s | 0.0214 s |           - |
|               CSharpSynth | 2.467 s | 0.5358 s | 0.0294 s | 1,063,984 B |
