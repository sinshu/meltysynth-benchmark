using System;
using NAudio.Wave;

public class NullSampleWriter : ISampleWriter
{
    public NullSampleWriter()
    {
    }

    public void Write(float[] data)
    {
    }

    public void Dispose()
    {
    }
}
