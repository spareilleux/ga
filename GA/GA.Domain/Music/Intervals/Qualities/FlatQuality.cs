using System.Collections.Generic;
using System.Linq;

namespace GA.Domain.Music.Intervals.Qualities
{
    public class FlatQuality : Quality
    {
        static FlatQuality()
        {
            _qualityByDistance = GetQualityByDistance();
        }
        
        private static readonly Dictionary<int, Quality> _qualityByDistance;
        
        private FlatQuality(int distance)
            : base(distance)
        {
        }

        /// <summary>
        /// Create a flat quality from a semitone interval.
        /// </summary>
        /// <param name="semitone">The <see cref="Semitone"/>.</param>
        /// <returns>The <see cref="FlatQuality"/>.</returns>
        public static FlatQuality FromSemitone(Semitone semitone)
        {
            var simpleDistance = semitone.SingleOctaveDistance;
            if (_qualityByDistance.TryGetValue(simpleDistance, out var quality)) return null;
            var result = new FlatQuality(quality.Distance);

            return result;
        }

        private static Dictionary<int, Quality> GetQualityByDistance()
        {
            var qualities = Find(Intervals.AccidentalKind.Flat).Distinct();            
            var result = qualities.ToDictionary(quality => quality.Distance);

            return result;
        }
    }
}