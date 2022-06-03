``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1706 (21H1/May2021Update)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100-preview.4.22252.9
  [Host]   : .NET Core 3.1.25 (CoreCLR 4.700.22.21202, CoreFX 4.700.22.21303), X64 RyuJIT
  ShortRun : .NET Core 3.1.25 (CoreCLR 4.700.22.21202, CoreFX 4.700.22.21303), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                    Method |    Mean |    Error |   StdDev |   Allocated |
|-------------------------- |--------:|---------:|---------:|------------:|
|                MeltySynth | 1.250 s | 0.1949 s | 0.0107 s |           - |
| MeltySynthReverbAndChorus | 2.112 s | 3.5026 s | 0.1920 s |           - |
|               CSharpSynth | 2.436 s | 0.2526 s | 0.0138 s | 1,063,944 B |
