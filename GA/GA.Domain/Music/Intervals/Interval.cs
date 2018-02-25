using System;
using System.Collections.Generic;
using GA.Domain.Music.Intervals.Metadata;
using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals
{
    /// <inheritdoc />
    /// <summary>
    /// Interval (Chronatic)
    /// </summary>
    /// <see href="http://en.wikipedia.org/wiki/Chromatic_interval#Intervals" />
    public class Interval : Semitone
    {
        // ReSharper disable InconsistentNaming
        public static readonly Interval P1 = (Interval)0;
        public static readonly Interval m2 = (Interval)1;
        public static readonly Interval A1 = (Interval)1;
        public static readonly Interval M2 = (Interval)2;
        public static readonly Interval d3 = (Interval)2;
        public static readonly Interval m3 = (Interval)3;
        public static readonly Interval A2 = (Interval)3;
        public static readonly Interval M3 = (Interval)4;
        public static readonly Interval d4 = (Interval)4;
        public static readonly Interval P4 = (Interval)5;
        public static readonly Interval A3 = (Interval)5;
        public static readonly Interval A4 = (Interval)6;
        public static readonly Interval d5 = (Interval)6;
        public static readonly Interval P5 = (Interval)7;
        public static readonly Interval d6 = (Interval)7;
        public static readonly Interval m6 = (Interval)8;
        public static readonly Interval A5 = (Interval)8;
        public static readonly Interval M6 = (Interval)9;
        public static readonly Interval d7 = (Interval)9;
        public static readonly Interval m7 = (Interval)10;
        public static readonly Interval A6 = (Interval)10;
        public static readonly Interval M7 = (Interval)11;
        public static readonly Interval d8 = (Interval)11;

        public static readonly Interval m9 = (Interval)13;
        public static readonly Interval A8 = (Interval)13;
        public static readonly Interval M9 = (Interval)14;
        public static readonly Interval d10 = (Interval)14;
        public static readonly Interval A9 = (Interval)15;
        public static readonly Interval m10 = (Interval)15;
        public static readonly Interval M10 = (Interval)16;
        public static readonly Interval d11 = (Interval)16;
        public static readonly Interval P11 = (Interval)17;
        public static readonly Interval A10 = (Interval)17;
        public static readonly Interval A11 = (Interval)18;
        public static readonly Interval d12 = (Interval)18;
        public static readonly Interval P12 = (Interval)19;
        public static readonly Interval d13 = (Interval)19;
        public static readonly Interval m13 = (Interval)20;
        public static readonly Interval A12 = (Interval)20;
        public static readonly Interval M13 = (Interval)21;
        public static readonly Interval d14 = (Interval)21;
        public static readonly Interval m14 = (Interval)22;
        public static readonly Interval A13 = (Interval)22;
        public static readonly Interval M14 = (Interval)23;
        // ReSharper restore InconsistentNaming

        private static readonly string[] _description =
            {
                "1", "b2", "2", "b3", "3", "4", "b5", "5", "b6", "6", "b7", "7", "8",
                "b9", "9", "#9", "10", "11", "#11", "12", "b13", "13", "b14", "14"
            };

        private static readonly Dictionary<int, Consonance> _consonances =
            new Dictionary<int, Consonance>
            {
                { 0, Consonance.PerfectConsonance },    // 6/6 - 1   +++
                { 1, Consonance.PerfectDissonance },    // 1/6 - b2  ---   
                { 2, Consonance.MediocreDissonance },   // 2/6 - 2   --
                { 3, Consonance.ImperfectConsonance },  // 4/6 - b3  +
                { 4, Consonance.ImperfectConsonance },  // 4/6 - 3   +
                { 5, Consonance.MediocreConsonance },   // 5/6 - 4   ++
                { 6, Consonance.PerfectDissonance },    // 1/6 - #4  ---
                { 7, Consonance.MediocreConsonance },   // 5/6 - 5   ++
                { 8, Consonance.MediocreDissonance },   // 2/6 - b6  --
                { 9, Consonance.ImperfectDissonance },  // 3/6 - 6   -
                { 10, Consonance.ImperfectDissonance }, // 3/6 - b7  -
                { 11, Consonance.PerfectDissonance },   // 1/6 - 7   ---
            };


        public Interval(int distance)
            : base(distance)
        {
        }

        /// <summary>
        /// Gets the interval <see cref="Consonance"/>.
        /// </summary>
        public Consonance Consonance => _consonances[AbsoluteDistance % 12];

        /// <summary>
        /// Gets the simple interval.
        /// </summary>
        public new Interval ToSimple()
        {
            return IsCompound ? new Interval(Distance % 12) : this;
        }

        /// <summary>
        /// Gets the compound interval.
        /// </summary>
        public new Interval ToCompound()
        {
            return IsSimple ? new Interval(Distance + 12) : this;
        }

        public static Interval operator !(Interval inverval)
        {
            var distance = inverval.Distance;
            if (distance < 0 || distance == 0 || distance == 8)
            {
                return inverval;
            }

            return inverval.IsCompound
                ? new Interval(24 - distance % 12)
                : new Interval(12 - distance);
        }

        /// <summary>
        /// Returns the corresponding Quality
        /// </summary>
        public Quality ToQuality(AccidentalKind accidentalKind)
        {
            var result = 
                accidentalKind == Intervals.AccidentalKind.Flat 
                    ? (Quality)FlatQuality.FromSemitone(this)
                    : SharpQuality.FromSemitone(this);

            return result;
        }

        public override string ToString()
        {
            return _description[DoubleOctaveDistance];
        }

        public static Interval operator ++(Interval interval)
        {
            if (interval.Distance == 23)
            {
                throw new OverflowException();
            }

            return new Interval(interval.Distance + 1);
        }

        public static Interval operator --(Interval interval)
        {
            if (interval.Distance == 0)
            {
                throw new OverflowException();
            }

            return new Interval(interval.Distance - 1);
        }
    }
}