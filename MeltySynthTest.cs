using System;
using MeltySynth;

public class MeltySynthTest : IDisposable
{
    private SoundFont soundFont;
    private MidiFile midiFile;

    private Synthesizer synthesizer;
    private MidiFileSequencer sequencer;

    private float[] left;
    private float[] right;
    private float[] writeBuffer;

    private ISampleWriter writer;

    public MeltySynthTest()
    {
        soundFont = new SoundFont(Settings.SoundFontPath);
        midiFile = new MidiFile(Settings.MidiFilePath);

        synthesizer = new Synthesizer(soundFont, Settings.SampleRate);
        sequencer = new MidiFileSequencer(synthesizer);

        sequencer.Play(midiFile, true);

        left = new float[Settings.BlockSize];
        right = new float[Settings.BlockSize];
        writeBuffer = new float[2 * Settings.BlockSize];

        if (Settings.OutputFile)
        {
            writer = new NAudioSampleWriter("meltysynth_output.wav", Settings.WaveFormat);
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
            sequencer.ProcessEvents();
            synthesizer.RenderStereo(left, right);
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
