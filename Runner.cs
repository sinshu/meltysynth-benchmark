using System;
using BenchmarkDotNet.Attributes;

[Config(typeof(BenchmarkConfig))]
public class Runner : IDisposable
{
    private CSharpSynthTest cSharpSynthTest;
    private MeltySynthTest meltySynthTest;
    private MeltySynthTest meltySynthTestEffect;

    public Runner()
    {
        meltySynthTest = new MeltySynthTest(false);
        meltySynthTestEffect = new MeltySynthTest(true);
        cSharpSynthTest = new CSharpSynthTest();
    }

    [Benchmark]
    public void MeltySynth()
    {
        meltySynthTest.Run();
    }

    [Benchmark]
    public void MeltySynthReverbAndChorus()
    {
        meltySynthTestEffect.Run();
    }

    [Benchmark]
    public void CSharpSynth()
    {
        cSharpSynthTest.Run();
    }

    public void Dispose()
    {
        if (cSharpSynthTest != null)
        {
            cSharpSynthTest.Dispose();
            cSharpSynthTest = null;
        }
        if (meltySynthTest != null)
        {
            meltySynthTest.Dispose();
            meltySynthTest = null;
        }
        if (meltySynthTestEffect != null)
        {
            meltySynthTestEffect.Dispose();
            meltySynthTestEffect = null;
        }
    }
}
