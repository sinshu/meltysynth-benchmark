``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19043
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.5.21302.13
  [Host]   : .NET Core 3.1.17 (CoreCLR 4.700.21.31506, CoreFX 4.700.21.31502), X64 RyuJIT
  ShortRun : .NET Core 3.1.17 (CoreCLR 4.700.21.31506, CoreFX 4.700.21.31502), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                    Method |    Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |--------:|---------:|---------:|------:|------:|------:|----------:|
|                MeltySynth | 1.312 s | 0.2333 s | 0.0128 s |     - |     - |     - |         - |
| MeltySynthReverbAndChorus | 2.143 s | 0.1878 s | 0.0103 s |     - |     - |     - |         - |
|               CSharpSynth | 2.457 s | 0.4731 s | 0.0259 s |     - |     - |     - | 1063944 B |
