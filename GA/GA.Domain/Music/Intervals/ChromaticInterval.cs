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
    public class ChromaticInterval : Semitone
    {     
        public static class Perfect
        {
            public static readonly ChromaticInterval Unison = GetInterval(1);
            public static readonly ChromaticInterval Fourth = GetInterval(4);
            public static readonly ChromaticInterval Fifth = GetInterval(5);
            public static readonly ChromaticInterval Octave = GetInterval(8);
        }

        public static class Imperfect
        {
            public static readonly ChromaticInterval Second = GetInterval(2);
            public static readonly ChromaticInterval Third = GetInterval(3);
            public static readonly ChromaticInterval Sixth = GetInterval(6);
            public static readonly ChromaticInterval Seventh = GetInterval(7);
        }


        public static class Major
        {
            public static readonly ChromaticInterval Second = Imperfect.Second;
            public static readonly ChromaticInterval Third = Imperfect.Third;
            public static readonly ChromaticInterval Sixth = Imperfect.Sixth;
            public static readonly ChromaticInterval Seventh = Imperfect.Seventh;
        }

        public static class Minor
        {
            public static readonly ChromaticInterval Second = (int)Major.Second - 1;
            public static readonly ChromaticInterval Third = (int)Major.Third - 1;
            public static readonly ChromaticInterval Sixth = (int)Major.Sixth - 1;
            public static readonly ChromaticInterval Seventh = (int)Major.Seventh - 1;
        }

        public static class Dim
        {
            public static readonly ChromaticInterval Second = (int)Minor.Second - 1;
            public static readonly ChromaticInterval Third = (int)Minor.Third - 1;
            public static readonly ChromaticInterval Sixth = (int)Minor.Sixth - 1;
            public static readonly ChromaticInterval Seventh = (int)Minor.Seventh - 1;
        }

        public static class Aug
        {
            public static readonly ChromaticInterval Second = (int)Major.Second + 1;
            public static readonly ChromaticInterval Third = (int)Major.Third + 1;
            public static readonly ChromaticInterval Sixth = (int)Major.Sixth + 1;
            public static readonly ChromaticInterval Seventh = (int)Major.Seventh + 1;
        }

        // ReSharper disable InconsistentNaming
        public new static readonly ChromaticInterval Unison = Perfect.Unison;
        public static readonly ChromaticInterval P1 = Unison;
        public static readonly ChromaticInterval m2 = Minor.Second;
        public static readonly ChromaticInterval A1 = 1;
        public static readonly ChromaticInterval M2 = Major.Second;
        public static readonly ChromaticInterval d3 = 2;
        public static readonly ChromaticInterval m3 = Minor.Third;
        public static readonly ChromaticInterval A2 = 3;
        public static readonly ChromaticInterval M3 = Major.Third;
        public static readonly ChromaticInterval d4 = 4;
        public static readonly ChromaticInterval P4 = Perfect.Fourth;
        public static readonly ChromaticInterval A3 = 5;
        public static readonly ChromaticInterval A4 = 6;
        public static readonly ChromaticInterval d5 = 6;
        public static readonly ChromaticInterval P5 = Perfect.Fifth;
        public static readonly ChromaticInterval d6 = 7;
        public static readonly ChromaticInterval m6 = Minor.Sixth;
        public static readonly ChromaticInterval A5 = 8;
        public static readonly ChromaticInterval M6 = Major.Sixth;
        public static readonly ChromaticInterval d7 = 9;
        public static readonly ChromaticInterval m7 = Minor.Seventh;
        public static readonly ChromaticInterval A6 = 10;
        public static readonly ChromaticInterval M7 = Major.Seventh;
        public static readonly ChromaticInterval d8 = 11;
        public new static readonly ChromaticInterval Octave = Perfect.Octave;


        public static readonly ChromaticInterval m9 = 13;
        public static readonly ChromaticInterval A8 = 13;
        public static readonly ChromaticInterval M9 = 14;
        public static readonly ChromaticInterval d10 = 14;
        public static readonly ChromaticInterval A9 = 15;
        public static readonly ChromaticInterval m10 = 15;
        public static readonly ChromaticInterval M10 = 16;
        public static readonly ChromaticInterval d11 = 16;
        public static readonly ChromaticInterval P11 = 17;
        public static readonly ChromaticInterval A10 = 17;
        public static readonly ChromaticInterval A11 = 18;
        public static readonly ChromaticInterval d12 = 18;
        public static readonly ChromaticInterval P12 = 19;
        public static readonly ChromaticInterval d13 = 19;
        public static readonly ChromaticInterval m13 = 20;
        public static readonly ChromaticInterval A12 = 20;
        public static readonly ChromaticInterval M13 = 21;
        public static readonly ChromaticInterval d14 = 21;
        public static readonly ChromaticInterval m14 = 22;
        public static readonly ChromaticInterval A13 = 22;
        public static readonly ChromaticInterval M14 = 23;
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


        public ChromaticInterval(int distance)
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
        public new ChromaticInterval ToSimple()
        {
            return IsCompound ? new ChromaticInterval(Distance % 12) : this;
        }

        /// <summary>
        /// Gets the compound interval.
        /// </summary>
        public new ChromaticInterval ToCompound()
        {
            return IsSimple ? new ChromaticInterval(Distance + 12) : this;
        }

        public static ChromaticInterval operator !(ChromaticInterval inverval)
        {
            var distance = inverval.Distance;
            if (distance < 0 || distance == 0 || distance == 8)
            {
                return inverval;
            }

            return inverval.IsCompound
                ? new ChromaticInterval(24 - distance % 12)
                : new ChromaticInterval(12 - distance);
        }

        /// <summary>
        /// Returns the corresponding Interval
        /// </summary>
        public Interval ToQuality(AccidentalKind accidentalKind)
        {
            var result =
                accidentalKind == AccidentalKind.Flat
                    ? Interval.GetFlat(this)
                    : Interval.GetSharp(this);

            return result;
        }

        private static ChromaticInterval GetInterval(int index)
        {
            var distance = ScaleDefinition.Major.Absolute[index - 1];
            var result = new ChromaticInterval(distance);

            return result;
        }

        public override string ToString()
        {
            return _names[DoubleOctaveDistance];
        }

        public static implicit operator ChromaticInterval(int distance)
        {
            return new ChromaticInterval(distance);
        }

        public static ChromaticInterval operator ++(ChromaticInterval interval)
        {
            if (interval.Distance == 23)
            {
                throw new OverflowException();
            }

            return new ChromaticInterval(interval.Distance + 1);
        }

        public static ChromaticInterval operator --(ChromaticInterval interval)
        {
            if (interval.Distance == 0)
            {
                throw new OverflowException();
            }

            return new ChromaticInterval(interval.Distance - 1);
        }
    }
}