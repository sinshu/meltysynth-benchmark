using System;
using BenchmarkDotNet.Attributes;

[Config(typeof(BenchmarkConfig))]
public class Runner : IDisposable
{
    private MeltySynthTest meltySynthTest;
    private CSharpSynthTest cSharpSynthTest;

    public Runner()
    {
        meltySynthTest = new MeltySynthTest();
        cSharpSynthTest = new CSharpSynthTest();
    }

    [Benchmark]
    public void RunMeltySynth()
    {
        meltySynthTest.Run();
    }

    [Benchmark]
    public void RunCSharpSynth()
    {
        cSharpSynthTest.Run();
    }

    public void Dispose()
    {
        if (meltySynthTest != null)
        {
            meltySynthTest.Dispose();
            meltySynthTest = null;
        }
        if (cSharpSynthTest != null)
        {
            cSharpSynthTest.Dispose();
            cSharpSynthTest = null;
        }
    }
}
