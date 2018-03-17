using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using GA.Core.Extensions;
using GA.Domain.Music.Scales;

namespace GA.Domain.Music.Intervals.Qualities
{
    /// <inheritdoc cref="Semitone" />
    /// <summary>
    /// ChromaticInterval interval.
    /// </summary>
    /// <see href="http://www.theguitarsuite.com/Theory/Intervals.html" />.
    public class Interval : ChromaticInterval, IComparable<Interval>, IEquatable<Interval>
    {
        private static readonly IDictionary<int, Interval> _flatIntervalByDistance;
        private static readonly IDictionary<int, Interval> _sharpIntervalByDistance;

        public static IEqualityComparer<Interval> EnharmonicEqualityComparer => EnharmonicComparer.Instance;

        /// <summary>
        /// Gets the scale definitions indexed by field name.
        /// </summary>
        public static IReadOnlyDictionary<string, Interval> ByFieldName;

        /// <summary>
        /// Gets the qualities indexed by name.
        /// </summary>
        public static IReadOnlyDictionary<string, Interval> ByName;

        /// <summary>
        /// Gets all qualities.
        /// </summary>
        public static IReadOnlyList<Interval> All;

        public static Interval GetFlat(Semitone semitone)
        {
            _flatIntervalByDistance.TryGetValue(semitone, out var result);

            return result;
        }

        public static Interval GetSharp(Semitone semitone)
        {
            _sharpIntervalByDistance.TryGetValue(semitone, out var result);

            return result;
        }

        // ReSharper disable InconsistentNaming
        static Interval()
        {
            var fieldInfos = typeof(Interval)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(fi => typeof(Interval).IsAssignableFrom(fi.FieldType))
                .ToList().AsReadOnly();

            ByFieldName = GetQualitiesByFieldName(fieldInfos);
            ByName = GetQualitiesByName(fieldInfos);
            All = ByName.Values.ToList().AsReadOnly();

            _flatIntervalByDistance = Index(AccidentalKind.Flat);
            _sharpIntervalByDistance = Index(AccidentalKind.Sharp);
        }

        #region Values

        /// <summary>
        /// Perfect unison interval
        /// </summary>
        public new static readonly Interval P1 = new Interval(DiatonicInterval.Unison, Accidental.None);

        /// <summary>
        /// Augmented unison interval
        /// </summary>
        public new static readonly Interval A1 = new Interval(DiatonicInterval.Unison, Accidental.Sharp);

        /// <summary>
        /// Minor second interval
        /// </summary>
        public new static readonly Interval m2 = new Interval(DiatonicInterval.Second, Accidental.Flat);

        /// <summary>
        /// Major second interval
        /// </summary>
        public new static readonly Interval M2 = new Interval(DiatonicInterval.Second);

        /// <summary>
        /// Augmented second interval
        /// </summary>
        public new static readonly Interval A2 = new Interval(DiatonicInterval.Second, Accidental.Sharp);

        /// <summary>
        /// Minor third interval
        /// </summary>
        public new static readonly Interval d3 = new Interval(DiatonicInterval.Third, Accidental.DoubleFlat);

        /// <summary>
        /// Minor third interval
        /// </summary>
        public new static readonly Interval m3 = new Interval(DiatonicInterval.Third, Accidental.Flat);

        /// <summary>
        /// Major thirs interval
        /// </summary>
        public new static readonly Interval M3 = new Interval(DiatonicInterval.Third);

        /// <summary>
        /// Major thirs interval
        /// </summary>
        public new static readonly Interval A3 = new Interval(DiatonicInterval.Third, Accidental.Sharp);

        /// <summary>
        /// Diminished fourth interval
        /// </summary>
        public new static readonly Interval d4 = new Interval(DiatonicInterval.Fourth, Accidental.Flat);

        /// <summary>
        /// Perfect fourth interval
        /// </summary>
        public new static readonly Interval P4 = new Interval(DiatonicInterval.Fourth);

        /// <summary>
        /// Augmented fourth interval
        /// </summary>
        public new static readonly Interval A4 = new Interval(DiatonicInterval.Fourth, Accidental.Sharp);

        /// <summary>
        /// Diminished fifth interval
        /// </summary>
        public new static readonly Interval d5 = new Interval(DiatonicInterval.Fifth, Accidental.Flat);

        /// <summary>
        /// Perfect fifth interval
        /// </summary>
        public new static readonly Interval P5 = new Interval(DiatonicInterval.Fifth);

        /// <summary>
        /// Augmented fifth interval
        /// </summary>
        public new static readonly Interval A5 = new Interval(DiatonicInterval.Fifth, Accidental.Sharp);

        /// <summary>
        /// Minor sixth interval
        /// </summary>
        public new static readonly Interval m6 = new Interval(DiatonicInterval.Sixth, Accidental.Flat);

        /// <summary>
        /// Major sixth intervalQuality
        /// </summary>
        public new static readonly Interval M6 = new Interval(DiatonicInterval.Sixth);

        /// <summary>
        /// Augmented sixth interval
        /// </summary>
        public new static readonly Interval A6 = new Interval(DiatonicInterval.Sixth, Accidental.Sharp);

        /// <summary>
        /// Diminished seventh interval
        /// </summary>
        public new static readonly Interval d7 = new Interval(DiatonicInterval.Seventh, Accidental.DoubleFlat);

        /// <summary>
        /// Minor seventh interval
        /// </summary>
        public new static readonly Interval m7 = new Interval(DiatonicInterval.Seventh, Accidental.Flat);

        /// <summary>
        /// Major seventh interval
        /// </summary>
        public new static readonly Interval M7 = new Interval(DiatonicInterval.Seventh);

        /// <summary>
        /// Octave interval
        /// </summary>
        public static readonly Interval P8 = new Interval(DiatonicInterval.Octave);

        /// <summary>
        /// Minor ninth interval
        /// </summary>
        public new static readonly Interval m9 = new Interval(DiatonicInterval.Ninth, Accidental.Flat);

        /// <summary>
        /// Major ninth interval
        /// </summary>
        public new static readonly Interval M9 = new Interval(DiatonicInterval.Ninth);

        /// <summary>
        /// Augmented ninth interval
        /// </summary>
        public new static readonly Interval A9 = new Interval(DiatonicInterval.Ninth, Accidental.Sharp);

        /// <summary>
        /// Minor tenth interval
        /// </summary>
        public new static readonly Interval m10 = new Interval(DiatonicInterval.Tenth, Accidental.Flat);

        /// <summary>
        /// Major tenth interval
        /// </summary>
        public new static readonly Interval M10 = new Interval(DiatonicInterval.Tenth);

        /// <summary>
        /// Perfect eleventh interval
        /// </summary>
        public new static readonly Interval P11 = new Interval(DiatonicInterval.Eleventh);

        /// <summary>
        /// Augmented eleventh interval
        /// </summary>
        public new static readonly Interval A11 = new Interval(DiatonicInterval.Eleventh, Accidental.Sharp);

        /// <summary>
        /// Perfect twelfth interval
        /// </summary>
        public new static readonly Interval P12 = new Interval(DiatonicInterval.Twelfth);

        /// <summary>
        /// Minor thirteenth interval
        /// </summary>
        public new static readonly Interval m13 = new Interval(DiatonicInterval.Thirteenth, Accidental.Flat);

        /// <summary>
        /// Major thirteenth interval
        /// </summary>
        public new static readonly Interval M13 = new Interval(DiatonicInterval.Thirteenth);

        /// <summary>
        /// Minor fourteenth interval
        /// </summary>
        public new static readonly Interval m14 = new Interval(DiatonicInterval.Fourteenth, Accidental.Flat);

        /// <summary>
        /// Major fourteenth interval
        /// </summary>
        public new static readonly Interval M14 = new Interval(DiatonicInterval.Fourteenth);

        // ReSharper restore InconsistentNaming

        #endregion

        /// <summary>
        /// Index qualities by distance for the given accidental kind (Flat or sharp).
        /// </summary>
        /// <param name="accidentalKind">The <see cref="AccidentalKind"/>.</param>
        /// <returns></returns>
        private static IDictionary<int, Interval> Index(AccidentalKind accidentalKind)
        {
            var qualities = All.Where(quality => quality.Accidental == null ||
                                                 quality.Accidental.AccidentalKind == accidentalKind &&
                                                 quality.Accidental.AbsoluteDistance <= 1)
                .OrderBy(quality => quality)
                .ThenBy(quality => quality.Accidental.AbsoluteDistance)
                .ToList();

            var result = new Dictionary<int, Interval>();
            var groups = qualities.GroupBy(quality => quality.Distance);
            foreach (var group in groups)
            {
                var elements = group.OrderByDescending(quality => quality.Accidental == null).ToList();
                result[group.Key] = elements.FirstOrDefault();
            }

            return result;
        }

        private static IReadOnlyDictionary<string, Interval> GetQualitiesByFieldName(IEnumerable<FieldInfo> fields)
        {
            var dict = new Dictionary<string, Interval>(StringComparer.Ordinal);
            foreach (var field in fields)
            {
                var quality = (Interval)field.GetValue(null);
                dict[field.Name] = quality; // e.g. "m3"
            }

            var result = new ReadOnlyDictionary<string, Interval>(dict);

            return result;
        }

        /// <summary>
        /// Gets qualities definitions, indexed by name.
        /// </summary>
        private static IReadOnlyDictionary<string, Interval> GetQualitiesByName(IEnumerable<FieldInfo> fields)
        {
            var dict = new Dictionary<string, Interval>(StringComparer.Ordinal);
            foreach (var field in fields)
            {
                var quality = (Interval)field.GetValue(null);
                dict[quality.ToString()] = quality; // e.g. "b3"
            }

            var result = new ReadOnlyDictionary<string, Interval>(dict);

            return result;
        }

        private static readonly Dictionary<Interval, string> _fullname =
            new Dictionary<Interval, string>
            {
                {P1, "perfect unison"},
                {A1, "augmented unison"},
                {m2, "minor 2nd"},
                {M2, "major 2nd"},
                {A2, "augmented 2nd"},
                {m3, "minor 3rd"},
                {M3, "major 3rd"},
                {d4, "diminished 4th"},
                {P4, "perfect 4th"},
                {A4, "augmented 4th"},
                {d5, "diminished 5th"},
                {P5, "perfect 5th"},
                {A5, "augmented 5th"},
                {m6, "minor 6th"},
                {M6, "major 6th"},
                {A6, "augmented 6th"},
                {d7, "diminished 7th"},
                {m7, "minor 7th"},
                {M7, "major 7th"},
                {P8, "octave"},
                {m9, "minor 9th"},
                {M9, "major 9th"},
                {A9, "augmented 9th"},
                {M10, "major 10th"},
                {P11, "perfect 11th"},
                {A11, "augmented 11th"},
                {P12, "perfect 12th"},
                {m13, "minor 13th"},
                {M13, "major 13th"},
                {m14, "minor 14th"},
                {M14, "major 14th"}
            };

        private static readonly ILookup<Interval, Interval> _enharmonics = GetEnharmonicsLookup();

        public Interval(int distance)
            : base(distance)
        {
        }

        public Interval(DiatonicInterval diatonicInterval)
            : this(diatonicInterval, Accidental.None)
        {
        }

        public Interval(
            DiatonicInterval diatonicInterval,
            Accidental accidental) : base(GetDistance(diatonicInterval, accidental))
        {
            DiatonicInterval = diatonicInterval;
            Accidental = accidental;
            Name = $"{Accidental}{(int) diatonicInterval}";
        }

        public DiatonicInterval DiatonicInterval { get; }
        public Accidental Accidental { get; }
        public string Name { get; }

        /// <summary>
        /// Gets the enharmonic <see cref="IReadOnlyCollection{Interval}"/>
        /// </summary>
        public IReadOnlyCollection<Interval> Enharmonics => _enharmonics[this].ToList().AsReadOnly();

        /// <summary>
        /// Gets the full name of the <see cref="Interval" />
        /// </summary>
        public string FullName
        {
            get
            {
                if (_fullname.TryGetValue(this, out var s)) return s;
                return null;
            }
        }

        /// <summary>
        /// Try to convert a string into an accidented diatonic interval
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <param name="interval">
        /// The interval.
        /// </param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool TryParse(string s, out Interval interval)
        {
            interval = P1;

            if (ByFieldName.TryGetValue(s, out var fq))
            {
                interval = fq;
                return true;
            }

            if (ByName.TryGetValue(s, out var nq))
            {
                interval = nq;
                return true;
            }

            // Fall back (Defensive)
            s = Regex.Replace(s.Trim(), "[()]", "");
            var accidentalString = Regex.Match(s, "^[^0-9]*").ToString();
            var diatonicIntervalString = Regex.Match(s, "[0-9]*$").ToString();
            if (string.IsNullOrEmpty(diatonicIntervalString))
            {
                return false; // Diatonic interval part found
            }

            if (!Accidental.TryParse(accidentalString, out var accidental) ||
                !Enum.TryParse<DiatonicInterval>(diatonicIntervalString, out var diatonicInterval))
            {
                return false;
            }

            interval = new Interval(diatonicInterval, accidental);

            return true;
        }

        /// <summary>
        /// Converts a string into an accidented diatonic interval
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <returns>
        /// The <see cref="Interval" />.
        /// </returns>
        public new static Interval Parse(string s)
        {
            s = Regex.Replace(s.Trim(), "[()]", string.Empty);
            var accidentalString = Regex.Match(s, "^[^0-9]*").ToString();
            var intervalString = Regex.Match(s, "[0-9]*$").ToString();

            var accidental = Accidental.Parse(accidentalString);
            var interval = (DiatonicInterval)Enum.Parse(typeof(DiatonicInterval), intervalString);
            var result = new Interval(interval, accidental);

            return result;
        }

        /// <summary>
        /// Converts a string into a set of <see cref="Interval" /> object
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <returns>
        /// The collection of <see cref="Interval" />.
        /// </returns>
        public static IEnumerable<Interval> ParseToCollection(string s)
        {
            s = s.Trim();

            if (string.IsNullOrEmpty(s) || s.Length == 0)
            {
                return new Interval[] { };
            }

            var result = new List<Interval>(s.Split(' ', ',', ';').Select(Parse));

            return result;
        }

        public bool IsEnharmonicWith(Interval other)
        {
            var result = Distance.Equals(other.Distance);

            return result;
        }

        /*

        /// <summary>
        /// True if enharmonic with any of the accidentedDiatonicIntervals
        /// </summary>
        public bool IsEnharmonicWith(params Interval[] qualities)
        {
            if (qualities.Length == 0)
                throw new ArgumentNullException("qualities");
            var tempThis = this;
            var result = qualities.Any(tempThis.IsEnharmonicWith);

            return result;
        }

        public Interval? ToEnharmonic(Interval interval)
        {
            var result =
                IsEnharmonicWith(interval)
                    ? (Interval?) interval
                    : null;

            return result;
        }

        public Interval? ToEnharmonic(params Interval[] qualities)
        {
            if (qualities.Length == 0)
                throw new ArgumentNullException("qualities");

            var tmpThis = this;
            foreach (var interval in qualities.Where(tmpThis.IsEnharmonicWith))
                return interval;
            return null;
        }

        /// <summary>
        /// Returns a new Interval where explicit natural is replace with natural 
        /// </summary>
        public Interval ToNormalizedNatural()
        {
            if (Accidental != Accidental.Natural)
                return this;

            var result = new Interval(this.interval, Accidental.None);

            return result;
        }

        */

        public Interval ToInversion()
        {
            var invertedDiatonicInterval = DiatonicInterval.Invert();
            var result = new Interval(invertedDiatonicInterval, !Accidental);

            return result;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(Interval other)
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
        public static bool operator ==(Interval a, Interval b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(null, a)) return false;
            var result = a.Equals(b);

            return result;
        }

        /// <summary>
        /// Indicates whether two accidented diatonic intervals are different
        /// </summary>
        public static bool operator !=(Interval a, Interval b)
        {
            var result = !(a == b);

            return result;
        }

        public static Interval operator +(Interval interval, Accidental accidental)
        {
            var result = new Interval(interval.DiatonicInterval, interval.Accidental + accidental);

            return result;
        }

        public static Semitone operator -(Interval interval1, Interval interval2)
        {
            var result = (Semitone)interval1 - interval2;

            return result;
        }

        public static Interval operator !(Interval interval)
        {
            var result = interval.ToInversion();

            return result;
        }

        public bool Equals(Interval other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && DiatonicInterval.Equals(other.DiatonicInterval) && Accidental.Equals(other.Accidental);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Interval)obj);
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

        private static int GetDistance(
            DiatonicInterval diatonicInterval, 
            Accidental accidental)
        {
            var diatonicDistance = (int) diatonicInterval - 1;
            var octave = 0;
            if (diatonicDistance >= 8)
            {
                diatonicDistance -= 8;
                octave = 1;
            }
            if (diatonicDistance < 0)
            {
                diatonicDistance += 8;
                octave = -1;
            }

            var result = ScaleDefinition.Major.Absolute[diatonicDistance] + accidental.Distance + octave * 8;

            return result;
        }

        private class EnharmonicComparer : IEqualityComparer<Interval>
        {
            public static readonly IEqualityComparer<Interval> Instance = new EnharmonicComparer();

            public bool Equals(Interval x, Interval y)
            {
                var result = x.Distance == y.Distance && x != y;

                return result;
            }

            public int GetHashCode(Interval obj)
            {
                return obj.Distance;
            }

            public override string ToString()
            {
                return "Enharmonic";
            }
        }

        private static ILookup<Interval, Interval> GetEnharmonicsLookup()
        {
            var twoOctaveIntervals = (from interval in Enumerable.Range(0, 14).Select(i => (DiatonicInterval)i)
                                      from accidental in Accidental.Values
                                      select new Interval(interval, accidental)).ToArray();

            var result = (from i1 in twoOctaveIntervals
                          from i2 in twoOctaveIntervals
                          where i1.IsEnharmonicWith(i2) && !ReferenceEquals(i1, i2)
                          select new { i1, i2 }).ToLookup(p => p.i1, p => p.i2);

            return result;
        }
    }
}