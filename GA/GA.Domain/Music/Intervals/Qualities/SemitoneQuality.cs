using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using GA.Core.Extensions;

namespace GA.Domain.Music.Intervals.Qualities
{
    /// <inheritdoc cref="Semitone" />
    /// <summary>
    /// Interval semitoneQuality.
    /// </summary>
    /// <see href="http://www.theguitarsuite.com/Theory/Intervals.html" />
    public class SemitoneQuality : Semitone, IComparable<SemitoneQuality>, IEquatable<SemitoneQuality>
    {
        private static readonly IDictionary<int, SemitoneQuality> _flatQualityByDistance;
        private static readonly IDictionary<int, SemitoneQuality> _sharpQualityByDistance;

        public static IEqualityComparer<SemitoneQuality> EnharmonicEqualityComparer => EnharmonicComparer.Instance;

        /// <summary>
        /// Gets the scale definitions indexed by field name.
        /// </summary>
        public static IReadOnlyDictionary<string, SemitoneQuality> ByFieldName;

        /// <summary>
        /// Gets the qualities indexed by name.
        /// </summary>
        public static IReadOnlyDictionary<string, SemitoneQuality> ByName;

        /// <summary>
        /// Gets all qualities.
        /// </summary>
        public static IReadOnlyList<SemitoneQuality> All;

        public static SemitoneQuality GetFlat(Semitone semitone)
        {
            _flatQualityByDistance.TryGetValue(semitone, out var result);

            return result;
        }

        public static SemitoneQuality GetSharp(Semitone semitone)
        {
            _sharpQualityByDistance.TryGetValue(semitone, out var result);

            return result;
        }

        // ReSharper disable InconsistentNaming
        static SemitoneQuality()
        {
            var fieldInfos = typeof(SemitoneQuality)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(fi => typeof(SemitoneQuality).IsAssignableFrom(fi.FieldType))
                .ToList().AsReadOnly();

            ByFieldName = GetQualitiesByFieldName(fieldInfos);
            ByName = GetQualitiesByName(fieldInfos);
            All = ByName.Values.ToList().AsReadOnly();

            _flatQualityByDistance = Index(AccidentalKind.Flat);
            _sharpQualityByDistance = Index(AccidentalKind.Sharp);
        }

        #region Values

        /// <summary>
        /// Perfect unison semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality P1 = new SemitoneQuality(DiatonicInterval.Unison, Accidental.None);

        /// <summary>
        /// Augmented unison semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality A1 = new SemitoneQuality(DiatonicInterval.Unison, Accidental.Sharp);

        /// <summary>
        /// Minor second semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality m2 = new SemitoneQuality(DiatonicInterval.Second, Accidental.Flat);

        /// <summary>
        /// Major second semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality M2 = new SemitoneQuality(DiatonicInterval.Second);

        /// <summary>
        /// Augmented second semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality A2 = new SemitoneQuality(DiatonicInterval.Second, Accidental.Sharp);

        /// <summary>
        /// Minor third semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality d3 = new SemitoneQuality(DiatonicInterval.Third, Accidental.DoubleFlat);

        /// <summary>
        /// Minor third semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality m3 = new SemitoneQuality(DiatonicInterval.Third, Accidental.Flat);

        /// <summary>
        /// Major thirs semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality M3 = new SemitoneQuality(DiatonicInterval.Third);

        /// <summary>
        /// Major thirs semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality A3 = new SemitoneQuality(DiatonicInterval.Third, Accidental.Sharp);

        /// <summary>
        /// Diminished fourth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality d4 = new SemitoneQuality(DiatonicInterval.Fourth, Accidental.Flat);

        /// <summary>
        /// Perfect fourth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality P4 = new SemitoneQuality(DiatonicInterval.Fourth);

        /// <summary>
        /// Augmented fourth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality A4 = new SemitoneQuality(DiatonicInterval.Fourth, Accidental.Sharp);

        /// <summary>
        /// Diminished fifth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality d5 = new SemitoneQuality(DiatonicInterval.Fifth, Accidental.Flat);

        /// <summary>
        /// Perfect fifth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality P5 = new SemitoneQuality(DiatonicInterval.Fifth);

        /// <summary>
        /// Augmented fifth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality A5 = new SemitoneQuality(DiatonicInterval.Fifth, Accidental.Sharp);

        /// <summary>
        /// Minor sixth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality m6 = new SemitoneQuality(DiatonicInterval.Sixth, Accidental.Flat);

        /// <summary>
        /// Major sixth intervalQuality
        /// </summary>
        public static readonly SemitoneQuality M6 = new SemitoneQuality(DiatonicInterval.Sixth);

        /// <summary>
        /// Augmented sixth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality A6 = new SemitoneQuality(DiatonicInterval.Sixth, Accidental.Sharp);

        /// <summary>
        /// Diminished seventh semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality d7 = new SemitoneQuality(DiatonicInterval.Seventh, Accidental.DoubleFlat);

        /// <summary>
        /// Minor seventh semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality m7 = new SemitoneQuality(DiatonicInterval.Seventh, Accidental.Flat);

        /// <summary>
        /// Major seventh semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality M7 = new SemitoneQuality(DiatonicInterval.Seventh);

        /// <summary>
        /// Octave semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality P8 = new SemitoneQuality(DiatonicInterval.Octave);

        /// <summary>
        /// Minor ninth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality m9 = new SemitoneQuality(DiatonicInterval.Ninth, Accidental.Flat);

        /// <summary>
        /// Major ninth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality M9 = new SemitoneQuality(DiatonicInterval.Ninth);

        /// <summary>
        /// Augmented ninth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality A9 = new SemitoneQuality(DiatonicInterval.Ninth, Accidental.Sharp);

        /// <summary>
        /// Minor tenth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality m10 = new SemitoneQuality(DiatonicInterval.Tenth, Accidental.Flat);

        /// <summary>
        /// Major tenth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality M10 = new SemitoneQuality(DiatonicInterval.Tenth);

        /// <summary>
        /// Perfect eleventh semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality P11 = new SemitoneQuality(DiatonicInterval.Eleventh);

        /// <summary>
        /// Augmented eleventh semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality A11 = new SemitoneQuality(DiatonicInterval.Eleventh, Accidental.Sharp);

        /// <summary>
        /// Perfect twelfth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality P12 = new SemitoneQuality(DiatonicInterval.Twelfth);

        /// <summary>
        /// Minor thirteenth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality m13 = new SemitoneQuality(DiatonicInterval.Thirteenth, Accidental.Flat);

        /// <summary>
        /// Major thirteenth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality M13 = new SemitoneQuality(DiatonicInterval.Thirteenth);

        /// <summary>
        /// Minor fourteenth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality m14 = new SemitoneQuality(DiatonicInterval.Fourteenth, Accidental.Flat);

        /// <summary>
        /// Major fourteenth semitoneQuality
        /// </summary>
        public static readonly SemitoneQuality M14 = new SemitoneQuality(DiatonicInterval.Fourteenth);

        // ReSharper restore InconsistentNaming

        #endregion

        /// <summary>
        /// Index qualities by distance for the given accidental kind (Flat or sharp).
        /// </summary>
        /// <param name="accidentalKind">The <see cref="AccidentalKind"/>.</param>
        /// <returns></returns>
        private static IDictionary<int, SemitoneQuality> Index(AccidentalKind accidentalKind)
        {
            var qualities = All.Where(quality => quality.Accidental == null ||
                                                 quality.Accidental.AccidentalKind == accidentalKind &&
                                                 quality.Accidental.AbsoluteDistance <= 1)
                .OrderBy(quality => quality)
                .ThenBy(quality => quality.Accidental.AbsoluteDistance)
                .ToList();

            var result = new Dictionary<int, SemitoneQuality>();
            var groups = qualities.GroupBy(quality => quality.Distance);
            foreach (var group in groups)
            {
                var elements = group.OrderByDescending(quality => quality.Accidental == null).ToList();
                result[group.Key] = elements.FirstOrDefault();
            }

            return result;
        }

        private static IReadOnlyDictionary<string, SemitoneQuality> GetQualitiesByFieldName(IEnumerable<FieldInfo> fields)
        {
            var dict = new Dictionary<string, SemitoneQuality>(StringComparer.Ordinal);
            foreach (var field in fields)
            {
                var quality = (SemitoneQuality)field.GetValue(null);
                dict[field.Name] = quality; // e.g. "m3"
            }

            var result = new ReadOnlyDictionary<string, SemitoneQuality>(dict);

            return result;
        }

        /// <summary>
        /// Gets qualities definitions, indexed by name.
        /// </summary>
        private static IReadOnlyDictionary<string, SemitoneQuality> GetQualitiesByName(IEnumerable<FieldInfo> fields)
        {
            var dict = new Dictionary<string, SemitoneQuality>(StringComparer.Ordinal);
            foreach (var field in fields)
            {
                var quality = (SemitoneQuality)field.GetValue(null);
                dict[quality.ToString()] = quality; // e.g. "b3"
            }

            var result = new ReadOnlyDictionary<string, SemitoneQuality>(dict);

            return result;
        }

        private static readonly Dictionary<SemitoneQuality, string> _fullname =
            new Dictionary<SemitoneQuality, string>
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

        private static readonly ILookup<SemitoneQuality, SemitoneQuality> _enharmonics = GetEnharmonicsLookup();

        public SemitoneQuality(int distance)
            : base(distance)
        {
        }

        public SemitoneQuality(DiatonicInterval diatonicInterval)
            : this(diatonicInterval, Accidental.None)
        {
        }

        public SemitoneQuality(
            DiatonicInterval diatonicInterval,
            Accidental accidental) : base((int)diatonicInterval + accidental.Distance)
        {
            DiatonicInterval = diatonicInterval;
            Accidental = accidental;
            Name = string.Format(CultureInfo.InvariantCulture, "{0}{1}", Accidental, (int)DiatonicInterval);
        }

        public DiatonicInterval DiatonicInterval { get; }
        public Accidental Accidental { get; }
        public string Name { get; }

        /// <summary>
        /// Gets the enharmonic <see cref="IReadOnlyCollection{SemitoneQuality}"/>
        /// </summary>
        public IReadOnlyCollection<SemitoneQuality> Enharmonics => _enharmonics[this].ToList().AsReadOnly();

        /// <summary>
        /// Gets the full name of the <see cref="SemitoneQuality" />
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
        /// Try to convert a string into an accidented diatonic semitoneQuality
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <param name="semitoneQuality">
        /// The semitoneQuality.
        /// </param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool TryParse(string s, out SemitoneQuality semitoneQuality)
        {
            semitoneQuality = P1;

            if (ByFieldName.TryGetValue(s, out var fq))
            {
                semitoneQuality = fq;
                return true;
            }

            if (ByName.TryGetValue(s, out var nq))
            {
                semitoneQuality = nq;
                return true;
            }

            // Fall back (Defensive)
            s = Regex.Replace(s.Trim(), "[()]", "");
            var accidentalString = Regex.Match(s, "^[^0-9]*").ToString();
            var diatonicIntervalString = Regex.Match(s, "[0-9]*$").ToString();
            if (string.IsNullOrEmpty(diatonicIntervalString))
            {
                return false; // Diatonic semitoneQuality part found
            }

            if (!Accidental.TryParse(accidentalString, out var accidental) || 
                !Enum.TryParse<DiatonicInterval>(diatonicIntervalString, out var diatonicInterval))
            {
                return false;
            }

            semitoneQuality = new SemitoneQuality(diatonicInterval, accidental);

            return true;
        }

        /// <summary>
        /// Converts a string into an accidented diatonic semitoneQuality
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <returns>
        /// The <see cref="SemitoneQuality" />.
        /// </returns>
        public new static SemitoneQuality Parse(string s)
        {
            s = Regex.Replace(s.Trim(), "[()]", string.Empty);
            var accidentalString = Regex.Match(s, "^[^0-9]*").ToString();
            var intervalString = Regex.Match(s, "[0-9]*$").ToString();

            var accidental = Accidental.Parse(accidentalString);
            var interval = (DiatonicInterval)Enum.Parse(typeof(DiatonicInterval), intervalString);
            var result = new SemitoneQuality(interval, accidental);

            return result;
        }

        /// <summary>
        /// Converts a string into a set of <see cref="SemitoneQuality" /> object
        /// </summary>
        /// <param name="s">
        /// The string.
        /// </param>
        /// <returns>
        /// The collection of <see cref="SemitoneQuality" />.
        /// </returns>
        public static IEnumerable<SemitoneQuality> ParseToCollection(string s)
        {
            s = s.Trim();

            if (string.IsNullOrEmpty(s) || s.Length == 0)
            {
                return new SemitoneQuality[] { };
            }

            var result = new List<SemitoneQuality>(s.Split(' ', ',', ';').Select(Parse));

            return result;
        }

        public bool IsEnharmonicWith(SemitoneQuality other)
        {
            var result = Distance.Equals(other.Distance);

            return result;
        }

        /*

        /// <summary>
        /// True if enharmonic with any of the accidentedDiatonicIntervals
        /// </summary>
        public bool IsEnharmonicWith(params SemitoneQuality[] qualities)
        {
            if (qualities.Length == 0)
                throw new ArgumentNullException("qualities");
            var tempThis = this;
            var result = qualities.Any(tempThis.IsEnharmonicWith);

            return result;
        }

        public SemitoneQuality? ToEnharmonic(SemitoneQuality semitoneQuality)
        {
            var result =
                IsEnharmonicWith(semitoneQuality)
                    ? (SemitoneQuality?) semitoneQuality
                    : null;

            return result;
        }

        public SemitoneQuality? ToEnharmonic(params SemitoneQuality[] qualities)
        {
            if (qualities.Length == 0)
                throw new ArgumentNullException("qualities");

            var tmpThis = this;
            foreach (var semitoneQuality in qualities.Where(tmpThis.IsEnharmonicWith))
                return semitoneQuality;
            return null;
        }

        /// <summary>
        /// Returns a new SemitoneQuality where explicit natural is replace with natural 
        /// </summary>
        public SemitoneQuality ToNormalizedNatural()
        {
            if (Accidental != Accidental.Natural)
                return this;

            var result = new SemitoneQuality(this.semitoneQuality, Accidental.None);

            return result;
        }

        */

        public SemitoneQuality ToInversion()
        {
            var invertedDiatonicInterval = DiatonicInterval.Invert();
            var result = new SemitoneQuality(invertedDiatonicInterval, !Accidental);

            return result;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(SemitoneQuality other)
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
        public static bool operator ==(SemitoneQuality a, SemitoneQuality b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(null, a)) return false;
            var result = a.Equals(b);

            return result;
        }

        /// <summary>
        /// Indicates whether two accidented diatonic intervals are different
        /// </summary>
        public static bool operator !=(SemitoneQuality a, SemitoneQuality b)
        {
            var result = !(a == b);

            return result;
        }

        public static SemitoneQuality operator +(SemitoneQuality semitoneQuality, Accidental accidental)
        {
            var result = new SemitoneQuality(semitoneQuality.DiatonicInterval, semitoneQuality.Accidental + accidental);

            return result;
        }

        public static Semitone operator -(SemitoneQuality interval1, SemitoneQuality interval2)
        {
            var result = (Semitone)interval1 - interval2;

            return result;
        }

        public static SemitoneQuality operator !(SemitoneQuality semitoneQuality)
        {
            var result = semitoneQuality.ToInversion();

            return result;
        }

        public bool Equals(SemitoneQuality other)
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
            return Equals((SemitoneQuality)obj);
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

        private class EnharmonicComparer : IEqualityComparer<SemitoneQuality>
        {
            public static readonly IEqualityComparer<SemitoneQuality> Instance = new EnharmonicComparer();

            public bool Equals(SemitoneQuality x, SemitoneQuality y)
            {
                var result = x.Distance == y.Distance && x != y;

                return result;
            }

            public int GetHashCode(SemitoneQuality obj)
            {
                return obj.Distance;
            }

            public override string ToString()
            {
                return "Enharmonic";
            }
        }

        private static ILookup<SemitoneQuality, SemitoneQuality> GetEnharmonicsLookup()
        {
            var twoOctaveIntervals = (from interval in Enumerable.Range(0, 14).Select(i => (DiatonicInterval)i)
                                      from accidental in Accidental.Values
                                      select new SemitoneQuality(interval, accidental)).ToArray();

            var result = (from i1 in twoOctaveIntervals
                          from i2 in twoOctaveIntervals    
                          where i1.IsEnharmonicWith(i2) && !ReferenceEquals(i1, i2)
                          select new { i1, i2 }).ToLookup(p => p.i1, p => p.i2);

            return result;
        }
    }
}