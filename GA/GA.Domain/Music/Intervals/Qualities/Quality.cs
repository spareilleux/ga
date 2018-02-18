using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace GA.Domain.Music.Intervals.Qualities
{
    /// <inheritdoc cref="Semitone" />
    /// <summary>
    /// Interval quality.
    /// </summary>
    /// <see href="http://www.theguitarsuite.com/Theory/Intervals.html" />
    public class Quality : Semitone, IComparable<Quality>, IEquatable<Quality>
    {
        private static readonly ILookup<Quality, Quality> _enharmonics = GetEnharmonicsLookup();

        public static IEqualityComparer<Quality> EnharmonicEqualityComparer => EnharmonicComparer.Instance;


        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Perfect unison quality
        /// </summary>
        public static readonly Quality P1 = new Quality(DiatonicInterval.Unison, Accidental.None);

        /// <summary>
        /// Augmented unison quality
        /// </summary>
        public static readonly Quality A1 = new Quality(DiatonicInterval.Unison, Accidental.Sharp);

        /// <summary>
        /// Minor second quality
        /// </summary>
        public static readonly Quality m2 = new Quality(DiatonicInterval.Second, Accidental.Flat);

        /// <summary>
        /// Major second quality
        /// </summary>
        public static readonly Quality M2 = new Quality(DiatonicInterval.Second);

        /// <summary>
        /// Augmented second quality
        /// </summary>
        public static readonly Quality A2 = new Quality(DiatonicInterval.Second, Accidental.Sharp);

        /// <summary>
        /// Minor third quality
        /// </summary>
        public static readonly Quality d3 = new Quality(DiatonicInterval.Third, Accidental.DoubleFlat);

        /// <summary>
        /// Minor third quality
        /// </summary>
        public static readonly Quality m3 = new Quality(DiatonicInterval.Third, Accidental.Flat);

        /// <summary>
        /// Major thirs quality
        /// </summary>
        public static readonly Quality M3 = new Quality(DiatonicInterval.Third);

        /// <summary>
        /// Major thirs quality
        /// </summary>
        public static readonly Quality A3 = new Quality(DiatonicInterval.Third, Accidental.Sharp);

        /// <summary>
        /// Diminished fourth quality
        /// </summary>
        public static readonly Quality d4 = new Quality(DiatonicInterval.Fourth, Accidental.Flat);

        /// <summary>
        /// Perfect fourth quality
        /// </summary>
        public static readonly Quality P4 = new Quality(DiatonicInterval.Fourth);

        /// <summary>
        /// Augmented fourth quality
        /// </summary>
        public static readonly Quality A4 = new Quality(DiatonicInterval.Fourth, Accidental.Sharp);

        /// <summary>
        /// Diminished fifth quality
        /// </summary>
        public static readonly Quality d5 = new Quality(DiatonicInterval.Fifth, Accidental.Flat);

        /// <summary>
        /// Perfect fifth quality
        /// </summary>
        public static readonly Quality P5 = new Quality(DiatonicInterval.Fifth);

        /// <summary>
        /// Augmented fifth quality
        /// </summary>
        public static readonly Quality A5 = new Quality(DiatonicInterval.Fifth, Accidental.Sharp);

        /// <summary>
        /// Minor sixth quality
        /// </summary>
        public static readonly Quality m6 = new Quality(DiatonicInterval.Sixth, Accidental.Flat);

        /// <summary>
        /// Major sixth intervalQuality
        /// </summary>
        public static readonly Quality M6 = new Quality(DiatonicInterval.Sixth);

        /// <summary>
        /// Augmented sixth quality
        /// </summary>
        public static readonly Quality A6 = new Quality(DiatonicInterval.Sixth, Accidental.Sharp);

        /// <summary>
        /// Diminished seventh quality
        /// </summary>
        public static readonly Quality d7 = new Quality(DiatonicInterval.Seventh, Accidental.DoubleFlat);

        /// <summary>
        /// Minor seventh quality
        /// </summary>
        public static readonly Quality m7 = new Quality(DiatonicInterval.Seventh, Accidental.Flat);

        /// <summary>
        /// Major seventh quality
        /// </summary>
        public static readonly Quality M7 = new Quality(DiatonicInterval.Seventh);

        /// <summary>
        /// Octave quality
        /// </summary>
        public static readonly Quality P8 = new Quality(DiatonicInterval.Octave);

        /// <summary>
        /// Minor ninth quality
        /// </summary>
        public static readonly Quality m9 = new Quality(DiatonicInterval.Ninth, Accidental.Flat);

        /// <summary>
        /// Major ninth quality
        /// </summary>
        public static readonly Quality M9 = new Quality(DiatonicInterval.Ninth);

        /// <summary>
        /// Augmented ninth quality
        /// </summary>
        public static readonly Quality A9 = new Quality(DiatonicInterval.Ninth, Accidental.Sharp);

        /// <summary>
        /// Minor tenth quality
        /// </summary>
        public static readonly Quality m10 = new Quality(DiatonicInterval.Tenth, Accidental.Flat);

        /// <summary>
        /// Major tenth quality
        /// </summary>
        public static readonly Quality M10 = new Quality(DiatonicInterval.Tenth);

        /// <summary>
        /// Perfect eleventh quality
        /// </summary>
        public static readonly Quality P11 = new Quality(DiatonicInterval.Eleventh);

        /// <summary>
        /// Augmented eleventh quality
        /// </summary>
        public static readonly Quality A11 = new Quality(DiatonicInterval.Eleventh, Accidental.Sharp);

        /// <summary>
        /// Perfect twelfth quality
        /// </summary>
        public static readonly Quality P12 = new Quality(DiatonicInterval.Twelfth);

        /// <summary>
        /// Minor thirteenth quality
        /// </summary>
        public static readonly Quality m13 = new Quality(DiatonicInterval.Thirteenth, Accidental.Flat);

        /// <summary>
        /// Major thirteenth quality
        /// </summary>
        public static readonly Quality M13 = new Quality(DiatonicInterval.Thirteenth);

        /// <summary>
        /// Minor fourteenth quality
        /// </summary>
        public static readonly Quality m14 = new Quality(DiatonicInterval.Fourteenth, Accidental.Flat);

        /// <summary>
        /// Major fourteenth quality
        /// </summary>
        public static readonly Quality M14 = new Quality(DiatonicInterval.Fourteenth);

        // ReSharper restore InconsistentNaming

        public static readonly Quality[] Values =
            {
                P1, A1,
                m2, M2, A2,
                m3, M3,
                d4, P4, A4,
                d5, P5, A5,
                m6, M6, A6,
                d7, m7, M7,
                P8,
                m9, M9, A9,
                M10,
                P11, A11,
                P12,
                m13, M13,
                m14, M14
            };


        private static readonly Dictionary<Quality, string> _fullname =
            new Dictionary<Quality, string>
                {
                    { P1, "perfect unison" },
                    { A1, "augmented unison" },
                    { m2, "minor 2nd" },
                    { M2, "major 2nd" },
                    { A2, "augmented 2nd" },
                    { m3, "minor 3rd" },
                    { M3, "major 3rd" },
                    { d4, "diminished 4th" },
                    { P4, "perfect 4th" },
                    { A4, "augmented 4th" },
                    { d5, "diminished 5th" },
                    { P5, "perfect 5th" },
                    { A5, "augmented 5th" },
                    { m6, "minor 6th" },
                    { M6, "major 6th" },
                    { A6, "augmented 6th" },
                    { d7, "diminished 7th" },
                    { m7, "minor 7th" },
                    { M7, "major 7th" },
                    { P8, "octave" },
                    { m9, "minor 9th" },
                    { M9, "major 9th" },
                    { A9, "augmented 9th" },
                    { M10, "major 10th" },
                    { P11, "perfect 11th" },
                    { A11, "augmented 11th" },
                    { P12, "perfect 12th" },
                    { m13, "minor 13th" },
                    { M13, "major 13th" },
                    { m14, "minor 14th" },
                    { M14, "major 14th" }
                };


        public Quality(int distance)
            : base(distance)
        {
        }

        public Quality(DiatonicInterval diatonicInterval)
            : this(diatonicInterval, Accidental.None)
        {
        }

        public Quality(
            DiatonicInterval diatonicInterval,
            Accidental accidental)  : base(diatonicInterval + accidental)
        {
            DiatonicInterval = diatonicInterval;
            Accidental = accidental;
            Name = string.Format(CultureInfo.InvariantCulture, "{0}{1}", Accidental, DiatonicInterval);
        }

        public DiatonicInterval DiatonicInterval { get; }
        public Accidental Accidental { get; }
        public string Name { get; }

        /// <summary>
        /// Gets the enharmonic <see cref="IReadOnlyCollection{Quality}"/>
        /// </summary>
        public IReadOnlyCollection<Quality> Enharmonics => _enharmonics[this].ToList().AsReadOnly();

        /// <summary>
        /// Gets the plain text description of the <see cref="Quality" />
        /// </summary>
        public string Description
        {
            get
            {
                if (_fullname.TryGetValue(this, out var s)) return s;
                return null;
            }
        }

        /// <summary>
        /// Try to convert a string into an accidented diatonic quality
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <param name="quality">
        /// The quality.
        /// </param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool TryParse(string s, out Quality quality)
        {
            quality = P1;

            s = Regex.Replace(s.Trim(), "[()]", "");
            var accidentalString = Regex.Match(s, "^[^0-9]*").ToString();
            var diatonicIntervalString = Regex.Match(s, "[0-9]*$").ToString();
            if (string.IsNullOrEmpty(diatonicIntervalString))
            {
                return false; // Diatonic quality part found
            }

            if (!Accidental.TryParse(accidentalString, out var accidental) || !DiatonicInterval.TryParse(diatonicIntervalString, out var diatonicInterval))
            {
                return false;
            }

            quality = new Quality(diatonicInterval, accidental);

            return true;
        }

        /// <summary>
        /// Converts a string into an accidented diatonic quality
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <returns>
        /// The <see cref="Quality" />.
        /// </returns>
        public new static Quality Parse(string s)
        {
            s = Regex.Replace(s.Trim(), "[()]", string.Empty);
            var accidentalString = Regex.Match(s, "^[^0-9]*").ToString();
            var intervalString = Regex.Match(s, "[0-9]*$").ToString();

            var accidental = Accidental.Parse(accidentalString);
            var interval = DiatonicInterval.Parse(intervalString);
            var result = new Quality(interval, accidental);

            return result;
        }

        /// <summary>
        /// Converts a string into a set of <see cref="Quality" /> object
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <returns>
        /// The collection of <see cref="Quality" />.
        /// </returns>
        public static IEnumerable<Quality> ParseToCollection(string s)
        {
            s = s.Trim();

            if (string.IsNullOrEmpty(s) || s.Length == 0)
            {
                return new Quality[] {};
            }

            var result = new List<Quality>(s.Split(' ', ',', ';').Select(Parse));

            return result;
        }

        public bool IsEnharmonicWith(Quality other)
        {
            var result = Distance.Equals(other.Distance);

            return result;
        }

        /*

        /// <summary>
        /// True if enharmonic with any of the accidentedDiatonicIntervals
        /// </summary>
        public bool IsEnharmonicWith(params Quality[] qualities)
        {
            if (qualities.Length == 0)
                throw new ArgumentNullException("qualities");
            var tempThis = this;
            var result = qualities.Any(tempThis.IsEnharmonicWith);

            return result;
        }

        public Quality? ToEnharmonic(Quality quality)
        {
            var result =
                IsEnharmonicWith(quality)
                    ? (Quality?) quality
                    : null;

            return result;
        }

        public Quality? ToEnharmonic(params Quality[] qualities)
        {
            if (qualities.Length == 0)
                throw new ArgumentNullException("qualities");

            var tmpThis = this;
            foreach (var quality in qualities.Where(tmpThis.IsEnharmonicWith))
                return quality;
            return null;
        }

        /// <summary>
        /// Returns a new Quality where explicit natural is replace with natural 
        /// </summary>
        public Quality ToNormalizedNatural()
        {
            if (Accidental != Accidental.Natural)
                return this;

            var result = new Quality(this.quality, Accidental.None);

            return result;
        }

        */

        public Quality ToInversion()
        {
            var result = new Quality(!DiatonicInterval, !Accidental);

            return result;
        }

        public override string ToString()
        {
            var result = string.Format(CultureInfo.InvariantCulture, "{0}{1}", Accidental, DiatonicInterval);

            return result;
        }

        public int CompareTo(Quality other)
        {
            var result = DiatonicInterval.CompareTo(other.DiatonicInterval);

            if (result != 0)
            {
                return result;
            }

            result = Accidental.CompareTo(other.Accidental);

            return result;
        }

        /// <summary>
        /// Indicates whether two accidented diatonic intervala are equal
        /// </summary>
        public static bool operator ==(Quality a, Quality b)
        {
            var result = a.Equals(b);

            return result;
        }

        /// <summary>
        /// Indicates whether two accidented diatonic intervals are different
        /// </summary>
        public static bool operator !=(Quality a, Quality b)
        {
            var result = !(a == b);

            return result;
        }

        public static Quality operator +(Quality quality, Accidental accidental)
        {
            var result = new Quality(quality.DiatonicInterval, quality.Accidental + accidental);

            return result;
        }

        public static Semitone operator -(Quality interval1, Quality interval2)
        {
            var result = (Semitone)interval1 - interval2;

            return result;
        }

        public static Quality operator !(Quality quality)
        {
            var result = quality.ToInversion();

            return result;
        }

        public bool Equals(Quality other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && DiatonicInterval.Equals(other.DiatonicInterval) && Accidental.Equals(other.Accidental);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Quality)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ DiatonicInterval.GetHashCode();
                hashCode = (hashCode * 397) ^ Accidental.GetHashCode();
                return hashCode;
            }
        }

        private class EnharmonicComparer : IEqualityComparer<Quality>
        {
            public static readonly IEqualityComparer<Quality> Instance = new EnharmonicComparer();

            public bool Equals(Quality x, Quality y)
            {
                var result = x.Distance == y.Distance && x != y;

                return result;
            }

            public int GetHashCode(Quality obj)
            {
                return obj.Distance;
            }

            public override string ToString()
            {
                return "Enharmonic";
            }
        }

        private static ILookup<Quality, Quality> GetEnharmonicsLookup()
        {
            var twoOctaveIntervals = (from interval in DiatonicInterval.TwoOctavesValues
                                      from accidental in Accidental.Values
                                      select new Quality(interval, accidental)).ToArray();

            var result = (from i1 in twoOctaveIntervals
                          from i2 in twoOctaveIntervals
                          where i1.IsEnharmonicWith(i2)
                          select new { i1, i2 }).ToLookup(p => p.i1, p => p.i2);

            return result;
        }
    }
}