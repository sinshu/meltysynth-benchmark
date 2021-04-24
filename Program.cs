using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<Runner>();
    }
}

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddExporter(MarkdownExporter.GitHub);
        AddDiagnoser(MemoryDiagnoser.Default);
        AddJob(Job.ShortRun);
    }
}
