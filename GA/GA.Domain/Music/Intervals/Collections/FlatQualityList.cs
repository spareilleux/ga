using System.Linq;
using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// List of qualities (Flat).
    /// </summary>    
    public class FlatQualityList : QualityListBase<FlatQuality>
    {
        public FlatQualityList(AbsoluteSemitoneList absoluteSemitones) 
            : base(absoluteSemitones.Select(FlatQuality.FromSemitone))
        {
        }
    }
}
