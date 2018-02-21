using System.ComponentModel;

namespace GA.Domain.Music.Intervals.Scales.Modes
{
    public enum HarmonicMinorScaleMode
    {
        /// <summary>
        /// First mode of harmonic minor scale
        /// </summary>
        [Description("harmonic minor")]
        HarmonicMinor = 1,

        /// <summary>
        /// Second mode of harmonic minor scale
        /// </summary>
        [Description("locrian \u266E6")]
        LocrianNaturalSixth = 2,

        /// <summary>
        /// Third mode of harmonic minor scale
        /// </summary>
        [Description("ionian augmented")]
        IonianAugmented = 3,

        /// <summary>
        /// Fourth mode of harmonic minor scale
        /// </summary>
        [Description("dorian \u266F4")]
        DorianSharpFourth = 4,

        /// <summary>
        /// Fifth mode of harmonic minor scale
        /// </summary>
        [Description("phrygian dominant")]
        PhrygianDominant = 5,

        /// <summary>
        /// Sixth mode of harmonic minor scale
        /// </summary>
        [Description("lydian \u266F2")]
        LydianSharpSecond = 6,

        /// <summary>
        /// Seventh mode of harmonic minor scale
        /// </summary>
        /// s
        [Description("altered bb7")]
        Alteredd7 = 7
    }
}