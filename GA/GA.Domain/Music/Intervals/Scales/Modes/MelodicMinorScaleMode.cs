using System.ComponentModel;

namespace GA.Domain.Music.Intervals.Scales.Modes
{
    public enum MelodicMinorScaleMode
    {
        [Description("Melodic minor")]
        MelodicMinor = 1,

        [Description("Dorian \u266D2")]
        DorianFlatSecond = 2,

        [Description("Lydian \u266F5")]
        LydianAugmented = 3,

        [Description("Lydian dominant")]
        LydianDominant = 4,

        [Description("Mixolydian \u266D6")]
        MixolydianFlatSixth = 5,

        [Description("Locrian \u266E2")]
        LocrianNaturalSecond = 6,

        [Description("Altered")]
        Altered = 7
    }
}
