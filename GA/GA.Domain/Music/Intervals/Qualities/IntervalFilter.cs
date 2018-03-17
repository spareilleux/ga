using System;
using System.Collections.Generic;

namespace GA.Domain.Music.Intervals.Qualities
{
    /// <inheritdoc cref="Interval" />
    /// <summary>
    /// Interval filter.
    /// </summary>
    public class IntervalFilter : Interval, IEquatable<IntervalFilter>
    {
        // ReSharper disable InconsistentNaming
        public static readonly IntervalFilter Any1 = new IntervalFilter(DiatonicInterval.Unison);
        public static readonly IntervalFilter Any2 = new IntervalFilter(DiatonicInterval.Second);
        public static readonly IntervalFilter Any3 = new IntervalFilter(DiatonicInterval.Third);
        public static readonly IntervalFilter Any4 = new IntervalFilter(DiatonicInterval.Fourth);
        public static readonly IntervalFilter Any5 = new IntervalFilter(DiatonicInterval.Fifth);
        public static readonly IntervalFilter Any6 = new IntervalFilter(DiatonicInterval.Sixth);
        public static readonly IntervalFilter Any7 = new IntervalFilter(DiatonicInterval.Seventh);
        public static readonly IntervalFilter Any8 = new IntervalFilter(DiatonicInterval.Octave);
        public static readonly IntervalFilter Any9 = new IntervalFilter(DiatonicInterval.Ninth);
        public static readonly IntervalFilter Any10 = new IntervalFilter(DiatonicInterval.Tenth);
        public static readonly IntervalFilter Any11 = new IntervalFilter(DiatonicInterval.Eleventh);
        public static readonly IntervalFilter Any12 = new IntervalFilter(DiatonicInterval.Twelfth);
        public static readonly IntervalFilter Any13 = new IntervalFilter(DiatonicInterval.Thirteenth);
        public static readonly IntervalFilter Any14 = new IntervalFilter(DiatonicInterval.Fourteenth);
        // ReSharper restore InconsistentNaming

        public IntervalFilter(DiatonicInterval diatonicInterval) 
            : base(diatonicInterval)
        {
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IntervalFilter);
        }

        public bool Equals(IntervalFilter other)
        {
            return other != null && DiatonicInterval == other.DiatonicInterval;
        }

        public override int GetHashCode()
        {
            return (int)DiatonicInterval;
        }

        public static bool operator ==(IntervalFilter quality1, IntervalFilter quality2)
        {
            return EqualityComparer<IntervalFilter>.Default.Equals(quality1, quality2);
        }

        public static bool operator !=(IntervalFilter quality1, IntervalFilter quality2)
        {
            return !(quality1 == quality2);
        }
    }
}
