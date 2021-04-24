﻿using System;

namespace MeltySynth
{
    internal struct RegionPair
    {
        private PresetRegion preset;
        private InstrumentRegion instrument;

        internal RegionPair(PresetRegion preset, InstrumentRegion instrument)
        {
            this.preset = preset;
            this.instrument = instrument;
        }

        private int this[GeneratorParameterType generatortType] => instrument[generatortType] + preset[generatortType];

        public PresetRegion Preset => preset;
        public InstrumentRegion Instrument => instrument;

        public int SampleStart => instrument.SampleStart;
        public int SampleEnd => instrument.SampleEnd;
        public int SampleStartLoop => instrument.SampleStartLoop;
        public int SampleEndLoop => instrument.SampleEndLoop;

        public int StartAddressOffset => instrument.StartAddressOffset;
        public int EndAddressOffset => instrument.EndAddressOffset;
        public int StartLoopAddressOffset => instrument.StartLoopAddressOffset;
        public int EndLoopAddressOffset => instrument.EndLoopAddressOffset;

        public int ModulationLfoToPitch => this[GeneratorParameterType.ModulationLfoToPitch];
        public int VibratoLfoToPitch => this[GeneratorParameterType.VibratoLfoToPitch];
        public int ModulationEnvelopeToPitch => this[GeneratorParameterType.ModulationEnvelopeToPitch];
        public float InitialFilterCutoffFrequency => SoundFontMath.CentsToHertz(this[GeneratorParameterType.InitialFilterCutoffFrequency]);
        public float InitialFilterQ => this[GeneratorParameterType.InitialFilterQ] / 10F;
        public int ModulationLfoToFilterCutoffFrequency => this[GeneratorParameterType.ModulationLfoToFilterCutoffFrequency];
        public int ModulationEnvelopeToFilterCutoffFrequency => this[GeneratorParameterType.ModulationEnvelopeToFilterCutoffFrequency];

        public float ModulationLfoToVolume => this[GeneratorParameterType.ModulationLfoToVolume] / 10F;

        public float ChorusEffectsSend => this[GeneratorParameterType.ChorusEffectsSend] / 10F;
        public float ReverbEffectsSend => this[GeneratorParameterType.ReverbEffectsSend] / 10F;
        public float Pan => this[GeneratorParameterType.Pan] / 10F;

        public float DelayModulationLfo => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.DelayModulationLfo]);
        public float FrequencyModulationLfo => SoundFontMath.CentsToHertz(this[GeneratorParameterType.FrequencyModulationLfo]);
        public float DelayVibratoLfo => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.DelayVibratoLfo]);
        public float FrequencyVibratoLfo => SoundFontMath.CentsToHertz(this[GeneratorParameterType.FrequencyVibratoLfo]);
        public float DelayModulationEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.DelayModulationEnvelope]);
        public float AttackModulationEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.AttackModulationEnvelope]);
        public float HoldModulationEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.HoldModulationEnvelope]);
        public float DecayModulationEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.DecayModulationEnvelope]);
        public float SustainModulationEnvelope => this[GeneratorParameterType.SustainModulationEnvelope] / 10F;
        public float ReleaseModulationEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.ReleaseModulationEnvelope]);
        public int KeyNumberToModulationEnvelopeHold => this[GeneratorParameterType.KeyNumberToModulationEnvelopeHold];
        public int KeyNumberToModulationEnvelopeDecay => this[GeneratorParameterType.KeyNumberToModulationEnvelopeDecay];
        public float DelayVolumeEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.DelayVolumeEnvelope]);
        public float AttackVolumeEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.AttackVolumeEnvelope]);
        public float HoldVolumeEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.HoldVolumeEnvelope]);
        public float DecayVolumeEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.DecayVolumeEnvelope]);
        public float SustainVolumeEnvelope => this[GeneratorParameterType.SustainVolumeEnvelope] / 10F;
        public float ReleaseVolumeEnvelope => SoundFontMath.TimecentsToSeconds(this[GeneratorParameterType.ReleaseVolumeEnvelope]);
        public int KeyNumberToVolumeEnvelopeHold => this[GeneratorParameterType.KeyNumberToVolumeEnvelopeHold];
        public int KeyNumberToVolumeEnvelopeDecay => this[GeneratorParameterType.KeyNumberToVolumeEnvelopeDecay];

        // public int KeyRangeStart => this[GeneratorParameterType.KeyRange] & 0xFF;
        // public int KeyRangeEnd => (this[GeneratorParameterType.KeyRange] >> 8) & 0xFF;
        // public int VelocityRangeStart => this[GeneratorParameterType.VelocityRange] & 0xFF;
        // public int VelocityRangeEnd => (this[GeneratorParameterType.VelocityRange] >> 8) & 0xFF;

        public float InitialAttenuation => this[GeneratorParameterType.InitialAttenuation] / 10F;

        public int CoarseTune => this[GeneratorParameterType.CoarseTune];
        public int FineTune => this[GeneratorParameterType.FineTune] + instrument.Sample.PitchCorrection;
        public LoopMode SampleModes => instrument.SampleModes;

        public int ScaleTuning => this[GeneratorParameterType.ScaleTuning];
        public int ExclusiveClass => instrument.ExclusiveClass;
        public int RootKey => instrument.RootKey;
    }
}
