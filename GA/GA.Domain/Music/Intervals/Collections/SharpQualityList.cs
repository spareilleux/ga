using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc/>
    /// <summary>
    /// List of qualities (Sharp).
    /// </summary>    
    public class SharpQualityList : QualityListBase<SharpQuality>
    {
        public SharpQualityList(AbsoluteSemitoneList absoluteSemitones) 
            : base(absoluteSemitones)
        {
        }
    }
}
