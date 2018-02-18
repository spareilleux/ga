using System.Collections.Generic;

namespace GA.Domain.Music.Intervals.Qualities
{
    public class FlatQuality : Quality
    {
        //private static readonly Dictionary<int, Quality> flatQualities =
        private static readonly Dictionary<int, Quality> _qualityByDistance = new Dictionary<int, Quality>
        {
            [0]  = P1,
            [1]  = m2,  [2] = M2,
            [3]  = m3,  [4] = M3,
            [5]  = P4,
            [6]  = d5,  [7] = P5,
            [8]  = m6,  [9] = M6,
            [10] = m7,  [11] = M7
        };

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
            if (!_qualityByDistance.TryGetValue(simpleDistance, out var quality)) return null;
            var result = new FlatQuality(quality);

            return result;
        }
    }
}