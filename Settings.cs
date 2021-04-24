using System;
using NAudio.Wave;

public static class Settings
{
    public static readonly bool OutputFile = false;

    public static readonly string SoundFontPath = @"C:\temp\TimGM6mb.sf2";
    public static readonly string MidiFilePath = @"C:\temp\PROGROCK.MID";

    public static readonly int SampleRate = 44100;
    public static readonly int BlockSize = 64;
    public static readonly int BlockCount = (3 * 60 * SampleRate) / BlockSize;

    public static readonly WaveFormat WaveFormat = new WaveFormat(SampleRate, 16, 2);
}
