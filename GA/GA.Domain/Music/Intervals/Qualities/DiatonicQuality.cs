using System;
using System.Collections.Generic;

namespace GA.Domain.Music.Intervals.Qualities
{
    /// <inheritdoc cref="Quality" />
    /// <summary>
    /// Interval quality with diatonic comparer.
    /// </summary>
    public class DiatonicQuality : Quality, IEquatable<DiatonicQuality>
    {
        // ReSharper disable InconsistentNaming
        public static readonly DiatonicQuality Any1 = new DiatonicQuality(DiatonicInterval.Unison);
        public static readonly DiatonicQuality Any2 = new DiatonicQuality(DiatonicInterval.Second);
        public static readonly DiatonicQuality Any3 = new DiatonicQuality(DiatonicInterval.Third);
        public static readonly DiatonicQuality Any4 = new DiatonicQuality(DiatonicInterval.Fourth);
        public static readonly DiatonicQuality Any5 = new DiatonicQuality(DiatonicInterval.Fifth);
        public static readonly DiatonicQuality Any6 = new DiatonicQuality(DiatonicInterval.Sixth);
        public static readonly DiatonicQuality Any7 = new DiatonicQuality(DiatonicInterval.Seventh);
        public static readonly DiatonicQuality Any8 = new DiatonicQuality(DiatonicInterval.Octave);
        public static readonly DiatonicQuality Any9 = new DiatonicQuality(DiatonicInterval.Ninth);
        public static readonly DiatonicQuality Any10 = new DiatonicQuality(DiatonicInterval.Tenth);
        public static readonly DiatonicQuality Any11 = new DiatonicQuality(DiatonicInterval.Eleventh);
        public static readonly DiatonicQuality Any12 = new DiatonicQuality(DiatonicInterval.Twelfth);
        public static readonly DiatonicQuality Any13 = new DiatonicQuality(DiatonicInterval.Thirteenth);
        public static readonly DiatonicQuality Any14 = new DiatonicQuality(DiatonicInterval.Fourteenth);
        // ReSharper restore InconsistentNaming

        public DiatonicQuality(DiatonicInterval diatonicInterval) 
            : base(diatonicInterval)
        {
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DiatonicQuality);
        }

        public bool Equals(DiatonicQuality other)
        {
            return other != null && DiatonicInterval == other.DiatonicInterval;
        }

        public override int GetHashCode()
        {
            return DiatonicInterval;
        }

        public static bool operator ==(DiatonicQuality quality1, DiatonicQuality quality2)
        {
            return EqualityComparer<DiatonicQuality>.Default.Equals(quality1, quality2);
        }

        public static bool operator !=(DiatonicQuality quality1, DiatonicQuality quality2)
        {
            return !(quality1 == quality2);
        }
    }
}
