using System;
using System.Collections.Generic;

namespace GA.Domain.Music.Intervals.Qualities
{
    /// <inheritdoc cref="SemitoneQuality" />
    /// <summary>
    /// Interval quality with diatonic comparer.
    /// </summary>
    public class DiatonicSemitoneQualityFilter : SemitoneQuality, IEquatable<DiatonicSemitoneQualityFilter>
    {
        // ReSharper disable InconsistentNaming
        public static readonly DiatonicSemitoneQualityFilter Any1 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Unison);
        public static readonly DiatonicSemitoneQualityFilter Any2 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Second);
        public static readonly DiatonicSemitoneQualityFilter Any3 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Third);
        public static readonly DiatonicSemitoneQualityFilter Any4 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Fourth);
        public static readonly DiatonicSemitoneQualityFilter Any5 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Fifth);
        public static readonly DiatonicSemitoneQualityFilter Any6 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Sixth);
        public static readonly DiatonicSemitoneQualityFilter Any7 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Seventh);
        public static readonly DiatonicSemitoneQualityFilter Any8 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Octave);
        public static readonly DiatonicSemitoneQualityFilter Any9 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Ninth);
        public static readonly DiatonicSemitoneQualityFilter Any10 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Tenth);
        public static readonly DiatonicSemitoneQualityFilter Any11 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Eleventh);
        public static readonly DiatonicSemitoneQualityFilter Any12 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Twelfth);
        public static readonly DiatonicSemitoneQualityFilter Any13 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Thirteenth);
        public static readonly DiatonicSemitoneQualityFilter Any14 = new DiatonicSemitoneQualityFilter(DiatonicInterval.Fourteenth);
        // ReSharper restore InconsistentNaming

        public DiatonicSemitoneQualityFilter(DiatonicInterval diatonicInterval) 
            : base(diatonicInterval)
        {
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DiatonicSemitoneQualityFilter);
        }

        public bool Equals(DiatonicSemitoneQualityFilter other)
        {
            return other != null && DiatonicInterval == other.DiatonicInterval;
        }

        public override int GetHashCode()
        {
            return (int)DiatonicInterval;
        }

        public static bool operator ==(DiatonicSemitoneQualityFilter quality1, DiatonicSemitoneQualityFilter quality2)
        {
            return EqualityComparer<DiatonicSemitoneQualityFilter>.Default.Equals(quality1, quality2);
        }

        public static bool operator !=(DiatonicSemitoneQualityFilter quality1, DiatonicSemitoneQualityFilter quality2)
        {
            return !(quality1 == quality2);
        }
    }
}
