using System.Collections.Generic;

namespace GA.Domain.Music.Intervals.Qualities
{
    public class SharpQuality : Quality
    {
        private static readonly Dictionary<int, Quality> _qualityByDistance = new Dictionary<int, Quality>
        {
            [0]  = P1, [1]  = A1,
            [2]  = M2, [3]  = A2,
            [4]  = M3,      
            [5]  = P4, [6]  = A4,
            [7]  = P5, [8]  = A5,
            [9]  = M6, [10] = A6,
            [11] = M7
        };

        private SharpQuality(int distancw)
            : base(distancw)
        {
        }

        /// <summary>
        /// Create a sharp quality from a semitone interval.
        /// </summary>
        /// <param name="semitone">The <see cref="Semitone"/>.</param>
        /// <returns>The <see cref="SharpQuality"/>.</returns>
        public static SharpQuality FromSemitone(Semitone semitone)
        {
            var simpleDistance = semitone.SingleOctaveDistance;
            if (!_qualityByDistance.TryGetValue(simpleDistance, out var quality)) return null;
            var result = new SharpQuality(quality);

            return result;
        }
    }
}