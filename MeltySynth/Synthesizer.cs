﻿using System;
using System.Collections.Generic;

namespace MeltySynth
{
    public sealed class Synthesizer
    {
        private static readonly int blockSize = 64;
        private static readonly int maxActiveVoiceCount = 40;

        private static readonly int channelCount = 16;
        private static readonly int percussionChannel = 9;

        private SoundFont soundFont;
        private int sampleRate;

        private int minimumVoiceLength;

        private Dictionary<int, Preset> presetLookup;

        private Channel[] channels;

        private VoiceCollection voices;

        private float[] blockLeft;
        private float[] blockRight;
        private int blockRead;

        private long processedSampleCount;

        private float masterVolume;

        public Synthesizer(SoundFont soundFont, int sampleRate)
        {
            if (soundFont == null)
            {
                throw new ArgumentNullException(nameof(soundFont));
            }

            if (!(8000 <= sampleRate && sampleRate <= 192000))
            {
                throw new ArgumentOutOfRangeException("The sample rate must be between 8000 and 192000.");
            }

            this.soundFont = soundFont;
            this.sampleRate = sampleRate;

            minimumVoiceLength = sampleRate / 500;

            presetLookup = new Dictionary<int, Preset>();
            foreach (var preset in soundFont.Presets)
            {
                var presetId = (preset.BankNumber << 16) | preset.PatchNumber;
                presetLookup.TryAdd(presetId, preset);
            }

            channels = new Channel[channelCount];
            for (var i = 0; i < channels.Length; i++)
            {
                channels[i] = new Channel(this, i == percussionChannel);
            }

            voices = new VoiceCollection(this, maxActiveVoiceCount);

            blockLeft = new float[blockSize];
            blockRight = new float[blockSize];
            blockRead = blockSize;

            processedSampleCount = 0;

            masterVolume = 0.5F;
        }

        internal Synthesizer(int sampleRate)
        {
            this.sampleRate = sampleRate;
        }

        public void ProcessMidiMessage(int channel, int command, int data1, int data2)
        {
            if (!(0 <= channel && channel < channels.Length))
            {
                return;
            }

            var channelInfo = channels[channel];

            switch (command)
            {
                case 0x80: // Note Off
                    NoteOff(channel, data1);
                    break;

                case 0x90: // Note On
                    NoteOn(channel, data1, data2);
                    break;

                case 0xB0: // Controller
                    switch (data1)
                    {
                        case 0x00: // Bank Selection
                            channelInfo.SetBank(data2);
                            break;

                        case 0x01: // Modulation Coarse
                            channelInfo.SetModulationCoarse(data2);
                            break;

                        case 0x21: // Modulation Fine
                            channelInfo.SetModulationCoarse(data2);
                            break;

                        case 0x06: // Data Entry Coarse
                            channelInfo.DataEntryCoarse(data2);
                            break;

                        case 0x26: // Data Entry Fine
                            channelInfo.DataEntryFine(data2);
                            break;

                        case 0x07: // Channel Volume Coarse
                            channelInfo.SetVolumeCoarse(data2);
                            break;

                        case 0x27: // Channel Volume Fine
                            channelInfo.SetVolumeFine(data2);
                            break;

                        case 0x0A: // Pan Coarse
                            channelInfo.SetPanCoarse(data2);
                            break;

                        case 0x2A: // Pan Fine
                            channelInfo.SetPanFine(data2);
                            break;

                        case 0x0B: // Expression Coarse
                            channelInfo.SetExpressionCoarse(data2);
                            break;

                        case 0x2B: // Expression Fine
                            channelInfo.SetExpressionFine(data2);
                            break;

                        case 0x40: // Hold Pedal
                            channelInfo.SetHoldPedal(data2);
                            break;

                        case 0x65: // RPN Coarse
                            channelInfo.SetRpnCoarse(data2);
                            break;

                        case 0x64: // RPN Fine
                            channelInfo.SetRpnFine(data2);
                            break;

                        case 0x78: // All Sound Off
                            NoteOffAll(channel, true);
                            break;

                        case 0x79: // Reset All Controllers
                            ResetAllControllers(channel);
                            break;

                        case 0x7B: // All Note Off
                            NoteOffAll(channel, false);
                            break;
                    }
                    break;

                case 0xC0: // Program Change
                    channelInfo.SetPatch(data1);
                    break;

                case 0xE0: // Pitch Bend
                    channelInfo.SetPitchBend(data1, data2);
                    break;
            }
        }

        public void NoteOff(int channel, int key)
        {
            if (!(0 <= channel && channel < channels.Length))
            {
                return;
            }

            foreach (var voice in voices)
            {
                if (voice.Channel == channel && voice.Key == key)
                {
                    voice.End();
                }
            }
        }

        public void NoteOn(int channel, int key, int velocity)
        {
            if (velocity == 0)
            {
                NoteOff(channel, key);
                return;
            }

            if (!(0 <= channel && channel < channels.Length))
            {
                return;
            }

            var channelInfo = channels[channel];

            var presetId = (channelInfo.BankNumber << 16) | channelInfo.PatchNumber;

            Preset preset;
            if (!presetLookup.TryGetValue(presetId, out preset))
            {
                return;
            }

            foreach (var presetRegion in preset.Regions)
            {
                if (presetRegion.Contains(key, velocity))
                {
                    foreach (var instrumentRegion in presetRegion.Instrument.Regions)
                    {
                        if (instrumentRegion.Contains(key, velocity))
                        {
                            var regionPair = new RegionPair(presetRegion, instrumentRegion);

                            var voice = voices.RequestNew(instrumentRegion, channel);
                            if (voice != null)
                            {
                                voice.Start(regionPair, channel, key, velocity);
                            }
                        }
                    }
                }
            }
        }

        public void NoteOffAll(bool immediate)
        {
            if (immediate)
            {
                voices.Clear();
            }
            else
            {
                foreach (var voice in voices)
                {
                    voice.End();
                }
            }
        }

        public void NoteOffAll(int channel, bool immediate)
        {
            if (immediate)
            {
                foreach (var voice in voices)
                {
                    if (voice.Channel == channel)
                    {
                        voice.Kill();
                    }
                }
            }
            else
            {
                foreach (var voice in voices)
                {
                    if (voice.Channel == channel)
                    {
                        voice.End();
                    }
                }
            }
        }

        public void ResetAllControllers(int channel)
        {
            channels[channel].ResetAllControllers();
        }

        public void Reset()
        {
            voices.Clear();

            foreach (var channel in channels)
            {
                channel.Reset();
            }
        }

        public void RenderStereo(Span<float> left, Span<float> right)
        {
            if (left.Length != right.Length)
            {
                throw new ArgumentException("The output buffers must be the same length.");
            }

            var wrote = 0;
            while (wrote < left.Length)
            {
                if (blockRead == blockSize)
                {
                    RenderBlock();
                    blockRead = 0;
                }

                var srcRem = blockSize - blockRead;
                var dstRem = left.Length - wrote;
                var rem = Math.Min(srcRem, dstRem);

                blockLeft.AsSpan(blockRead, rem).CopyTo(left);
                blockRight.AsSpan(blockRead, rem).CopyTo(right);

                blockRead += rem;
                wrote += rem;
            }
        }

        public void RenderMono(Span<float> destination)
        {
            var wrote = 0;
            while (wrote < destination.Length)
            {
                if (blockRead == blockSize)
                {
                    RenderBlock();
                    blockRead = 0;
                }

                var srcRem = blockSize - blockRead;
                var dstRem = destination.Length - wrote;
                var rem = Math.Min(srcRem, dstRem);

                var blockLeftSpan = blockLeft.AsSpan(blockRead, rem);
                var blockRightSpan = blockRight.AsSpan(blockRead, rem);
                SpanMath.Mean(blockLeftSpan, blockRightSpan, destination.Slice(wrote, rem));

                blockRead += rem;
                wrote += rem;
            }
        }

        private void RenderBlock()
        {
            voices.Process();

            Array.Clear(blockLeft, 0, blockLeft.Length);
            Array.Clear(blockRight, 0, blockRight.Length);

            foreach (var voice in voices)
            {
                var source = voice.Block;

                var gainLeft = masterVolume * voice.MixGainLeft;
                if (gainLeft > SoundFontMath.NonAudible)
                {
                    SpanMath.MultiplyAdd(gainLeft, source, blockLeft);
                }

                var gainRight = masterVolume * voice.MixGainRight;
                if (gainRight > SoundFontMath.NonAudible)
                {
                    SpanMath.MultiplyAdd(gainRight, source, blockRight);
                }
            }

            processedSampleCount += blockSize;
        }

        public int BlockSize => blockSize;
        public int MaxActiveVoiceCount => maxActiveVoiceCount;

        public int ChannelCount => channelCount;
        public int PercussionChannel => percussionChannel;

        public SoundFont SoundFont => soundFont;
        public int SampleRate => sampleRate;

        public int ActiveVoiceCount => voices.ActiveVoiceCount;

        public long ProcessedSampleCount => processedSampleCount;

        public float MasterVolume
        {
            get => masterVolume;
            set => masterVolume = value;
        }

        internal int MinimumVoiceLength => minimumVoiceLength;
        internal Channel[] Channels => channels;
    }
}
