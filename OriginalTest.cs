using System;
using Original;

public class OriginalTest : IDisposable
{
    private SoundFont soundFont;
    private MidiFile midiFile;

    private Synthesizer synthesizer;
    private MidiFileSequencer sequencer;

    private float[] left;
    private float[] right;
    private float[] writeBuffer;

    private ISampleWriter writer;

    public OriginalTest()
    {
        soundFont = new SoundFont(Settings.SoundFontPath);
        midiFile = new MidiFile(Settings.MidiFilePath);

        var settings = new SynthesizerSettings(Settings.SampleRate);
        settings.MaximumPolyphony = 40;

        synthesizer = new Synthesizer(soundFont, settings);
        sequencer = new MidiFileSequencer(synthesizer);

        sequencer.Play(midiFile, true);

        left = new float[Settings.BlockSize];
        right = new float[Settings.BlockSize];
        writeBuffer = new float[2 * Settings.BlockSize];

        var name = "original.wav";

        if (Settings.OutputFile)
        {
            writer = new NAudioSampleWriter(name, Settings.WaveFormat);
        }
        else
        {
            writer = new NullSampleWriter();
        }
    }

    public void Run()
    {
        for (var i = 0; i < Settings.BlockCount; i++)
        {
            sequencer.Render(left, right);
            for (var t = 0; t < Settings.BlockSize; t++)
            {
                writeBuffer[2 * t] = left[t];
                writeBuffer[2 * t + 1] = right[t];
            }
            writer.Write(writeBuffer);
        }
    }

    public void Dispose()
    {
        if (writer != null)
        {
            writer.Dispose();
            writer = null;
        }
    }
}
