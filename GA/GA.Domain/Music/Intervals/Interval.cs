using System;
using System.Collections.Generic;
using GA.Domain.Music.Intervals.Metadata;
using GA.Domain.Music.Intervals.Qualities;
using GA.Domain.Music.Scales;

namespace GA.Domain.Music.Intervals
{
    /// <inheritdoc />
    /// <summary>
    /// Chromatic interval.
    /// </summary>
    /// <see href="http://en.wikipedia.org/wiki/Chromatic_interval#Intervals" />
    public class Interval : Semitone
    {     
        public static class Perfect
        {
            public static readonly Interval Unison = GetInterval(1);
            public static readonly Interval Fourth = GetInterval(4);
            public static readonly Interval Fifth = GetInterval(5);
            public static readonly Interval Octave = GetInterval(8);
        }

        public static class Imperfect
        {
            public static readonly Interval Second = GetInterval(2);
            public static readonly Interval Third = GetInterval(3);
            public static readonly Interval Sixth = GetInterval(6);
            public static readonly Interval Seventh = GetInterval(7);
        }


        public static class Major
        {
            public static readonly Interval Second = Imperfect.Second;
            public static readonly Interval Third = Imperfect.Third;
            public static readonly Interval Sixth = Imperfect.Sixth;
            public static readonly Interval Seventh = Imperfect.Seventh;
        }

        public static class Minor
        {
            public static readonly Interval Second = (int)Major.Second - 1;
            public static readonly Interval Third = (int)Major.Third - 1;
            public static readonly Interval Sixth = (int)Major.Sixth - 1;
            public static readonly Interval Seventh = (int)Major.Seventh - 1;
        }

        public static class Dim
        {
            public static readonly Interval Second = (int)Minor.Second - 1;
            public static readonly Interval Third = (int)Minor.Third - 1;
            public static readonly Interval Sixth = (int)Minor.Sixth - 1;
            public static readonly Interval Seventh = (int)Minor.Seventh - 1;
        }

        public static class Aug
        {
            public static readonly Interval Second = (int)Major.Second + 1;
            public static readonly Interval Third = (int)Major.Third + 1;
            public static readonly Interval Sixth = (int)Major.Sixth + 1;
            public static readonly Interval Seventh = (int)Major.Seventh + 1;
        }

        // ReSharper disable InconsistentNaming
        public new static readonly Interval Unison = Perfect.Unison;
        public static readonly Interval P1 = Unison;
        public static readonly Interval m2 = Minor.Second;
        public static readonly Interval A1 = 1;
        public static readonly Interval M2 = Major.Second;
        public static readonly Interval d3 = 2;
        public static readonly Interval m3 = Minor.Third;
        public static readonly Interval A2 = 3;
        public static readonly Interval M3 = Major.Third;
        public static readonly Interval d4 = 4;
        public static readonly Interval P4 = Perfect.Fourth;
        public static readonly Interval A3 = 5;
        public static readonly Interval A4 = 6;
        public static readonly Interval d5 = 6;
        public static readonly Interval P5 = Perfect.Fifth;
        public static readonly Interval d6 = 7;
        public static readonly Interval m6 = Minor.Sixth;
        public static readonly Interval A5 = 8;
        public static readonly Interval M6 = Major.Sixth;
        public static readonly Interval d7 = 9;
        public static readonly Interval m7 = Minor.Seventh;
        public static readonly Interval A6 = 10;
        public static readonly Interval M7 = Major.Seventh;
        public static readonly Interval d8 = 11;
        public new static readonly Interval Octave = Perfect.Octave;


        public static readonly Interval m9 = 13;
        public static readonly Interval A8 = 13;
        public static readonly Interval M9 = 14;
        public static readonly Interval d10 = 14;
        public static readonly Interval A9 = 15;
        public static readonly Interval m10 = 15;
        public static readonly Interval M10 = 16;
        public static readonly Interval d11 = 16;
        public static readonly Interval P11 = 17;
        public static readonly Interval A10 = 17;
        public static readonly Interval A11 = 18;
        public static readonly Interval d12 = 18;
        public static readonly Interval P12 = 19;
        public static readonly Interval d13 = 19;
        public static readonly Interval m13 = 20;
        public static readonly Interval A12 = 20;
        public static readonly Interval M13 = 21;
        public static readonly Interval d14 = 21;
        public static readonly Interval m14 = 22;
        public static readonly Interval A13 = 22;
        public static readonly Interval M14 = 23;
        // ReSharper restore InconsistentNaming

        private static readonly string[] _names =
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
        /// Returns the corresponding SemitoneQuality
        /// </summary>
        public SemitoneQuality ToQuality(AccidentalKind accidentalKind)
        {
            var result =
                accidentalKind == AccidentalKind.Flat
                    ? SemitoneQuality.GetFlat(this)
                    : SemitoneQuality.GetSharp(this);

            return result;
        }

        private static Interval GetInterval(int index)
        {
            var distance = ScaleDefinition.Major.Absolute[index - 1];
            var result = new Interval(distance);

            return result;
        }

        public override string ToString()
        {
            return _names[DoubleOctaveDistance];
        }

        public static implicit operator Interval(int distance)
        {
            return new Interval(distance);
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