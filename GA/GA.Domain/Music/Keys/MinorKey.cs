using System.ComponentModel;

namespace GA.Domain.Music.Keys
{
    public enum MinorKey
    {
        Abm = -7,
        Ebm = -6,
        Bbm = -5,
        Fm = -4,
        Cm = -3,
        Gm = -2,
        Dm = -1,
        Am = 0,
        Em = 1,
        Bm = 2,

        [Description("F#m")]
        FSharpm = 3,

        [Description("C#m")]
        CSharpm = 4,

        [Description("G#m")]
        GSharpm = 5,

        [Description("D#m")]
        DSharpm = 6,

        [Description("A#m")]
        ASharpm = 7
    }
}