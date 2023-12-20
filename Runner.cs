using System;
using BenchmarkDotNet.Attributes;

[Config(typeof(BenchmarkConfig))]
public class Runner : IDisposable
{
    private OriginalTest original;
    private MemoryMappedFileTest mmfOff;
    private MemoryMappedFileTest mmfOn;

    public Runner()
    {
        original = new OriginalTest();
        mmfOff = new MemoryMappedFileTest(false);
        mmfOn = new MemoryMappedFileTest(true);
    }

    [Benchmark]
    public void Original()
    {
        original.Run();
    }

    [Benchmark]
    public void MemoryMapOff()
    {
        mmfOff.Run();
    }

    [Benchmark]
    public void MemoryMapOn()
    {
        mmfOn.Run();
    }

    public void Dispose()
    {
        if (original != null)
        {
            original.Dispose();
            original = null;
        }
        if (mmfOff != null)
        {
            mmfOff.Dispose();
            mmfOff = null;
        }
        if (mmfOn != null)
        {
            mmfOn.Dispose();
            mmfOn = null;
        }
    }
}
