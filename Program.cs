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
        //var runner = new Runner();
        //runner.Original();
        //runner.MemoryMapOff();
        //runner.MemoryMapOn();
        //runner.Dispose();
        BenchmarkRunner.Run<Runner>();
    }
}

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddDiagnoser(MemoryDiagnoser.Default);
        AddJob(Job.ShortRun);
    }
}
