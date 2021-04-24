using System;
using NAudio.Wave;

public class NAudioSampleWriter : ISampleWriter
{
    private WaveFileWriter writer;

    public NAudioSampleWriter(string path, WaveFormat format)
    {
        writer = new WaveFileWriter(path, format);
    }

    public void Write(float[] data)
    {
        writer.WriteSamples(data, 0, data.Length);
    }

    public void Dispose()
    {
        if (writer != null)
        {
            Console.WriteLine("Wrote file: " + writer.Filename);
            writer.Dispose();
            writer = null;
        }
    }
}
