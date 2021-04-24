using System;

public interface ISampleWriter : IDisposable
{
    void Write(float[] data);
}
