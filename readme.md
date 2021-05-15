``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.985 (2004/?/20H1)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.3.21202.5
  [Host]   : .NET Core 3.1.15 (CoreCLR 4.700.21.21202, CoreFX 4.700.21.21402), X64 RyuJIT
  ShortRun : .NET Core 3.1.15 (CoreCLR 4.700.21.21202, CoreFX 4.700.21.21402), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                    Method |    Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |--------:|---------:|---------:|------:|------:|------:|----------:|
|                MeltySynth | 1.283 s | 0.1708 s | 0.0094 s |     - |     - |     - |         - |
| MeltySynthReverbAndChorus | 2.097 s | 0.2863 s | 0.0157 s |     - |     - |     - |         - |
|               CSharpSynth | 2.536 s | 0.1496 s | 0.0082 s |     - |     - |     - | 1063944 B |
