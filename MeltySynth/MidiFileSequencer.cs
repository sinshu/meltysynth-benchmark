﻿using System;

namespace MeltySynth
{
    /// <summary>
    /// An instance of the MIDI file sequencer.
    /// </summary>
    public sealed class MidiFileSequencer
    {
        private readonly Synthesizer synthesizer;

        private float speed;

        private MidiFile midiFile;
        private bool loop;

        private int blockWrote;

        private TimeSpan currentTime;
        private int msgIndex;
        private int loopIndex;

        /// <summary>
        /// Initializes a new instance of the sequencer.
        /// </summary>
        /// <param name="synthesizer">The synthesizer to be handled by the sequencer.</param>
        public MidiFileSequencer(Synthesizer synthesizer)
        {
            if (synthesizer == null)
            {
                throw new ArgumentNullException(nameof(synthesizer));
            }

            this.synthesizer = synthesizer;

            speed = 1F;
        }

        /// <summary>
        /// Plays the MIDI file.
        /// </summary>
        /// <param name="midiFile">The MIDI file to be played.</param>
        /// <param name="loop">If <c>true</c>, the MIDI file loops after reaching the end.</param>
        public void Play(MidiFile midiFile, bool loop)
        {
            if (midiFile == null)
            {
                throw new ArgumentNullException(nameof(midiFile));
            }

            this.midiFile = midiFile;
            this.loop = loop;

            blockWrote = synthesizer.BlockSize;

            currentTime = TimeSpan.Zero;
            msgIndex = 0;
            loopIndex = 0;

            synthesizer.Reset();
        }

        /// <summary>
        /// Stop playing.
        /// </summary>
        public void Stop()
        {
            midiFile = null;

            synthesizer.Reset();
        }

        /// <summary>
        /// Renders the waveform.
        /// </summary>
        /// <param name="left">The buffer of the left channel to store the rendered waveform.</param>
        /// <param name="right">The buffer of the right channel to store the rendered waveform.</param>
        /// <remarks>
        /// The buffers must be the same length.
        /// </remarks>
        public void Render(Span<float> left, Span<float> right)
        {
            if (left.Length != right.Length)
            {
                throw new ArgumentException("The output buffers must be the same length.");
            }

            var wrote = 0;
            while (wrote < left.Length)
            {
                if (blockWrote == synthesizer.BlockSize)
                {
                    ProcessEvents();
                    blockWrote = 0;
                    currentTime += TimeSpan.FromSeconds((double)speed * synthesizer.BlockSize / synthesizer.SampleRate);
                }

                var srcRem = synthesizer.BlockSize - blockWrote;
                var dstRem = left.Length - wrote;
                var rem = Math.Min(srcRem, dstRem);

                synthesizer.Render(left.Slice(wrote, rem), right.Slice(wrote, rem));

                blockWrote += rem;
                wrote += rem;
            }
        }

        /// <summary>
        /// Send the MIDI events to the synthesizer.
        /// This method should be called enough frequently in the rendering process.
        /// </summary>
        private void ProcessEvents()
        {
            if (midiFile == null)
            {
                return;
            }

            while (msgIndex < midiFile.Messages.Length)
            {
                var time = midiFile.Times[msgIndex];
                var msg = midiFile.Messages[msgIndex];
                if (time <= currentTime)
                {
                    if (msg.Type == MidiFile.MessageType.Normal)
                    {
                        synthesizer.ProcessMidiMessage(msg.Channel, msg.Command, msg.Data1, msg.Data2);
                    }
                    else if (msg.Type == MidiFile.MessageType.LoopPoint)
                    {
                        loopIndex = msgIndex;
                    }
                    msgIndex++;
                }
                else
                {
                    break;
                }
            }

            if (msgIndex == midiFile.Messages.Length && loop)
            {
                currentTime = midiFile.Times[loopIndex];
                msgIndex = loopIndex;
            }
        }

        /// <summary>
        /// Gets or sets the playback speed.
        /// </summary>
        /// <remarks>
        /// The default value is 1.
        /// The tempo will be multiplied by this value.
        /// </remarks>
        public float Speed
        {
            get => speed;

            set
            {
                if (value > 0)
                {
                    speed = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The playback speed must be a positive value.");
                }
            }
        }
    }
}
