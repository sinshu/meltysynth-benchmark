``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.1052 (2004/?/20H1)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.4.21255.9
  [Host]   : .NET Core 3.1.16 (CoreCLR 4.700.21.26205, CoreFX 4.700.21.26205), X64 RyuJIT
  ShortRun : .NET Core 3.1.16 (CoreCLR 4.700.21.26205, CoreFX 4.700.21.26205), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                    Method |    Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |--------:|---------:|---------:|------:|------:|------:|----------:|
|                MeltySynth | 1.322 s | 0.2844 s | 0.0156 s |     - |     - |     - |         - |
| MeltySynthReverbAndChorus | 2.124 s | 0.4925 s | 0.0270 s |     - |     - |     - |         - |
|               CSharpSynth | 2.454 s | 0.6218 s | 0.0341 s |     - |     - |     - | 1063984 B |
