using System;
using AudioSynthesis.Midi;
using AudioSynthesis.Sequencer;
using AudioSynthesis.Synthesis;

public class CSharpSynthTest : IDisposable
{
    private MidiFile midiFile;

    private Synthesizer synthesizer;
    private MidiFileSequencer sequencer;

    private ISampleWriter writer;

    public CSharpSynthTest()
    {
        midiFile = new MidiFile(Settings.MidiFilePath);

        synthesizer = new Synthesizer(Settings.SampleRate, 2, Settings.BlockSize, 1);
        synthesizer.LoadBank(Settings.SoundFontPath);
        sequencer = new MidiFileSequencer(synthesizer);

        sequencer.LoadMidi(midiFile);
        sequencer.Play();

        if (Settings.OutputFile)
        {
            writer = new NAudioSampleWriter("csharpsynth_output.wav", Settings.WaveFormat);
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
            sequencer.FillMidiEventQueue(true);
            synthesizer.GetNext();
            writer.Write(synthesizer.sampleBuffer);
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
