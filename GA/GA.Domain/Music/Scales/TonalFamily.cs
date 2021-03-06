﻿using System.ComponentModel;

namespace GA.Domain.Music.Scales
{
    public enum TonalFamily
    {
        [Description("major")]
        Major,
        [Description("natural minor")]
        NaturalMinor,
        [Description("harmonic minor")]
        HarmonicMinor,
        [Description("melodic minor")]
        MelodicMinor
    }
}