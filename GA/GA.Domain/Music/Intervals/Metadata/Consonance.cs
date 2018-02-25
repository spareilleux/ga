using System.ComponentModel;

namespace GA.Domain.Music.Intervals.Metadata
{
    /// <summary>
    /// Interval consonance (See http://www.thegearpage.net/board/showthread.php?t=681018)
    /// </summary>
    public enum Consonance
    {
        [Description("---")]
        PerfectDissonance = -3,
        [Description("--")]
        MediocreDissonance = -2,
        [Description("-")]
        ImperfectDissonance = -1,
        [Description("+")]
        ImperfectConsonance = 1,
        [Description("++")]
        MediocreConsonance = 2,
        [Description("+++")]
        PerfectConsonance = 3,
    }
}
