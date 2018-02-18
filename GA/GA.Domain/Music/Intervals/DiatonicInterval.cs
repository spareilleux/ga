using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals
{
    /// <inheritdoc cref="Semitone" />.
    /// <summary>
    /// Represents a diatonic interval.
    /// </summary>
    /// <see href="http://en.wikipedia.org/wiki/Diatonic_interval#Intervals" />
    public class DiatonicInterval : Semitone, IEquatable<DiatonicInterval>, IComparable<DiatonicInterval>
    {
        private readonly int _value;

        /// <summary>
        /// Derived from Major scale - 2 2 1 2 2 2 1
        /// </summary>
        public static int[] _intervals = {0,  2,  4,  5,  7,  9,  11,
                                          12, 14, 16, 17, 19, 21, 23};
          
        public new static readonly DiatonicInterval Unison = new DiatonicInterval(0);
        public static readonly DiatonicInterval Second = new DiatonicInterval(1);
        public static readonly DiatonicInterval Third = new DiatonicInterval(2);
        public static readonly DiatonicInterval Fourth = new DiatonicInterval(3);
        public static readonly DiatonicInterval Fifth = new DiatonicInterval(4);
        public static readonly DiatonicInterval Sixth = new DiatonicInterval(5);
        public static readonly DiatonicInterval Seventh = new DiatonicInterval(6);
        public new static readonly DiatonicInterval Octave = new DiatonicInterval(7);
        public static readonly DiatonicInterval Ninth = new DiatonicInterval(8);
        public static readonly DiatonicInterval Tenth = new DiatonicInterval(9);
        public static readonly DiatonicInterval Eleventh = new DiatonicInterval(10);
        public static readonly DiatonicInterval Twelfth = new DiatonicInterval(11);
        public static readonly DiatonicInterval Thirteenth = new DiatonicInterval(12);
        public static readonly DiatonicInterval Fourteenth = new DiatonicInterval(13);

        public static readonly DiatonicInterval[] OneOctaveValues = {
                                                                        Unison,
                                                                        Second,
                                                                        Third,
                                                                        Fourth,
                                                                        Fifth,
                                                                        Sixth,
                                                                        Seventh
                                                                    };

        public static readonly DiatonicInterval[] TwoOctavesValues = {
                                                                         Unison,
                                                                         Second,
                                                                         Third,
                                                                         Fourth,
                                                                         Fifth,
                                                                         Sixth,
                                                                         Seventh,
                                                                         Octave,
                                                                         Ninth,
                                                                         Tenth,
                                                                         Eleventh,
                                                                         Twelfth,
                                                                         Thirteenth,
                                                                         Fourteenth
                                                                     };

        public DiatonicInterval(int value) 
            : base(GetDistance(value))
        {
            _value = value;
        }

        private static int GetDistance(int value)
        {
            if (value < 0 || value >= 12) throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} must be between 0 and 11");

            return _intervals[value];
        }

        /// <summary>
        /// Tries to parse a <see cref="DiatonicInterval" /> from its string representation
        /// </summary>
        public static bool TryParse(string value, out DiatonicInterval interval)
        {
            interval = Unison;

            // Remove non-numeric characters
            value = Regex.Replace(value, "[^.0-9]", "");
            if (!int.TryParse(value, out var i) || i < 0 || i > 13) return false;

            interval = i - 1;
            return true;
        }

        /// <summary>
        /// Creates a diatonic interval from its string representation
        /// </summary>
        public new static DiatonicInterval Parse(string value)
        {
            if (!TryParse(value, out var result)) throw new InvalidOperationException();

            return result;
        }

        /// <summary>
        /// Converts a string into a list of diatonic interval
        /// </summary>
        public static IList<DiatonicInterval> ParseToList(string value)
        {
            return value.Split(' ', ',', ';').Select(Parse).ToList();
        }

        /// <summary>
        /// Take the corresponding simple interval
        /// </summary>
        public new DiatonicInterval ToSimple()
        {
            return new DiatonicInterval(_value % 7);
        }

        /// <summary>
        /// Take the corresponding compound interval
        /// </summary>
        public new DiatonicInterval ToCompound()
        {
            return new DiatonicInterval(_value % 7 + 7);
        }

        /// <summary>
        /// Take the interval inversion
        /// </summary>
        public DiatonicInterval ToInversion()
        {
            var simple = _value % 7;
            if (simple == 0)
            {
                return this;
            }

            var result = _value > 7
                             ? (DiatonicInterval)(14 - simple)
                             : (DiatonicInterval)(7 - _value);

            return result;
        }

        public override string ToString()
        {
            return $"{_value + 1}";
        }

        public int CompareTo(DiatonicInterval other)
        {
            return _value.CompareTo(other._value);
        }

        public bool Equals(DiatonicInterval other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DiatonicInterval)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ _value;
            }
        }

        public static bool operator >(DiatonicInterval a, DiatonicInterval b)
        {
            return a.Distance > b.Distance;
        }

        public static bool operator >=(DiatonicInterval a, DiatonicInterval b)
        {
            return a.Distance >= b.Distance;
        }

        public static bool operator <(DiatonicInterval a, DiatonicInterval b)
        {
            return a.Distance < b.Distance;
        }

        public static bool operator <=(DiatonicInterval a, DiatonicInterval b)
        {
            return a.Distance <= b.Distance;
        }

        /// <summary>
        /// Indicates whether two diatonic intervals are equal
        /// </summary>
        public static bool operator ==(DiatonicInterval a, DiatonicInterval b)
        {
            if (ReferenceEquals(a, b)) return true;

            return a != null && b != null && a.Distance == b.Distance;
        }

        /// <summary>
        /// Indicates whether two diatonic intervals are different
        /// </summary>
        public static bool operator !=(DiatonicInterval a, DiatonicInterval b)
        {
            return !(a == b);
        }

        public static implicit operator DiatonicInterval(int value)
        {
            return new DiatonicInterval(value);
        }

        public static implicit operator int(DiatonicInterval interval)
        {
            return interval._value;
        }

        public static Quality operator +(DiatonicInterval interval, Accidental accidental)
        {
            return new Quality(interval, accidental);
        }

        public static DiatonicInterval operator +(DiatonicInterval a, DiatonicInterval b)
        {
            var newValue = a._value + b._value;
            if (newValue < 0 || newValue > 11)
            {
                throw new OverflowException();
            }

            return new DiatonicInterval(newValue);
        }

        public static DiatonicInterval operator -(DiatonicInterval a, DiatonicInterval b)
        {
            var newValue = a._value - b._value;
            if (newValue < 0 || newValue > 11)
            {
                throw new OverflowException();
            }

            return new DiatonicInterval(newValue);
        }

        public static DiatonicInterval operator ++(DiatonicInterval interval)
        {
            if (interval == Fourteenth)
            {
                throw new OverflowException();
            }

            return new DiatonicInterval(interval._value + 1);
        }

        public static DiatonicInterval operator --(DiatonicInterval interval)
        {
            if (interval == Unison)
            {
                throw new OverflowException();
            }

            return new DiatonicInterval(interval._value - 1);
        }

        public static DiatonicInterval operator !(DiatonicInterval interval)
        {
            return interval.ToInversion();
        }

        public static DiatonicInterval operator *(DiatonicInterval interval, Direction direction)
        {
            return direction == Direction.Flat
                       ? interval.ToInversion()
                       : interval;
        }
    }
}