using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <summary>
    /// List of qualities (Flat).
    /// </summary>    
    public class FlatQualityList : QualityListBase<FlatQuality>
    {
        public FlatQualityList(AbsoluteSemitoneList absoluteSemitones) 
            : base(absoluteSemitones)
        {
        }
    }
}
