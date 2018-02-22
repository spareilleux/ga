using System;
using System.Collections.Generic;

namespace GA.Domain.Music.Intervals
{
    /// <inheritdoc cref="IEquatable{DiatonicTone}" />
    /// <inheritdoc cref="IComparable{DiatonicTone}" />
    /// <summary>
    /// DiatonicTone number (See http://en.wikipedia.org/wiki/DiatonicTone)
    /// </summary>
    public class DiatonicTone : IEquatable<DiatonicTone>, IComparable<DiatonicTone>
    {
        public DiatonicTone(int distance)
        {
            Distance = distance;
        }

        /// <summary>
        /// Gets the <see cref="AccidentalKind"/>.
        /// </summary>
        public AccidentalKind AccidentalKind => (AccidentalKind)Math.Sign(Distance);

        /// <summary>
        /// Gets the distance in DiatonicTones (Signed).
        /// </summary>
        public virtual int Distance { get; }

        /// <summary>
        /// Gets the absolute distance in DiatonicTones.
        /// </summary>
        public virtual int AbsoluteDistance => Math.Abs(Distance);

        /// <summary>
        /// Gets the distance in DiatonicTones limited to an octave (Signed).
        /// </summary>
        public int SimpleDistance => Distance % 12;

        /// <summary>
        /// True if below one octave.
        /// </summary>
        public bool IsSimple => AbsoluteDistance < 12;

        /// <summary>
        /// True if over one octave.
        /// </summary>
        public bool IsCompound => AbsoluteDistance >= 12;

        public bool Equals(DiatonicTone other)
        {
            return Distance == other.Distance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DiatonicTone diatonicTone && Equals(diatonicTone);
        }

        public override int GetHashCode()
        {
            return Distance;
        }

        public int CompareTo(DiatonicTone other)
        {
            return Comparer<int>.Default.Compare(Distance, other.Distance);
        }

        public static implicit operator DiatonicTone(sbyte value)
        {
            return new DiatonicTone(value);
        }

        public static implicit operator int(DiatonicTone diatonicTone)
        {
            return diatonicTone.Distance;
        }

        /// <summary>
        /// Inverts a <see cref="DiatonicTone" /> object.
        /// </summary>
        /// <param name="tone">The <see cref="DiatonicTone" /></param>
        /// <returns>The inverted <see cref="DiatonicTone" />.</returns>
        public static DiatonicTone operator !(DiatonicTone tone)
        {
            return new DiatonicTone(-tone.Distance);
        }

        /// <summary>
        /// "Is Greater Than" comparison between two <see cref="DiatonicTone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="DiatonicTone" /></param>
        /// <param name="b">The second <see cref="DiatonicTone" /></param>
        /// <returns>True if the <see cref="DiatonicTone" /> a is greater than  the <see cref="DiatonicTone" /> b</returns>
        public static bool operator >(DiatonicTone a, DiatonicTone b)
        {
            return a.Distance > b.Distance;
        }

        /// <summary>
        /// "Is Greater Or Equal" comparison between two <see cref="DiatonicTone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="DiatonicTone" /></param>
        /// <param name="b">The second <see cref="DiatonicTone" /></param>
        /// <returns>True if the <see cref="DiatonicTone" /> a is greater than or equal to the <see cref="DiatonicTone" /> b</returns>
        public static bool operator >=(DiatonicTone a, DiatonicTone b)
        {
            return a.Distance >= b.Distance;
        }

        /// <summary>
        /// "Is Less Than" comparison between two <see cref="DiatonicTone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="DiatonicTone" /></param>
        /// <param name="b">The second <see cref="DiatonicTone" /></param>
        /// <returns>True if the <see cref="DiatonicTone" /> a is less than the <see cref="DiatonicTone" /> b</returns>
        public static bool operator <(DiatonicTone a, DiatonicTone b)
        {
            return a.Distance < b.Distance;
        }

        /// <summary>
        /// "Is Less Or Equal" comparison between two <see cref="DiatonicTone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="DiatonicTone" /></param>
        /// <param name="b">The second <see cref="DiatonicTone" /></param>
        /// <returns>True if the <see cref="DiatonicTone" /> a is less than or equal to the <see cref="DiatonicTone" /> b</returns>
        public static bool operator <=(DiatonicTone a, DiatonicTone b)
        {
            return a.Distance <= b.Distance;
        }

        /// <summary>
        /// Equality test between two <see cref="DiatonicTone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="DiatonicTone" /></param>
        /// <param name="b">The second <see cref="DiatonicTone" /></param>
        /// <returns>True if the <see cref="DiatonicTone" /> a is equal to the <see cref="DiatonicTone" /> b</returns>
        public static bool operator ==(DiatonicTone a, DiatonicTone b)
        {
            if (ReferenceEquals(a, b)) return true;
            return a != null && b != null && a.Distance == b.Distance;
        }

        /// <summary>
        /// Difference test between two <see cref="DiatonicTone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="DiatonicTone" /></param>
        /// <param name="b">The second <see cref="DiatonicTone" /></param>
        /// <returns>True if the <see cref="DiatonicTone" /> a is different than the <see cref="DiatonicTone" /> b</returns>
        public static bool operator !=(DiatonicTone a, DiatonicTone b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Adds two <see cref="DiatonicTone" /> objects
        /// </summary>
        /// <param name="a">The first <see cref="DiatonicTone" /></param>
        /// <param name="b">The second <see cref="DiatonicTone" /></param>
        /// <returns>The sum of the two <see cref="DiatonicTone" /> objects</returns>
        public static DiatonicTone operator +(DiatonicTone a, DiatonicTone b)
        {
            return new DiatonicTone(a.Distance + b.Distance);
        }

        /// <summary>
        /// Subtract an <see cref="DiatonicTone" /> to another one
        /// </summary>
        /// <param name="a">The first <see cref="DiatonicTone" /></param>
        /// <param name="b">The second <see cref="DiatonicTone" /></param>
        /// <returns>The <see cref="DiatonicTone" /> subtraction result</returns>
        public static DiatonicTone operator -(DiatonicTone a, DiatonicTone b)
        {
            return new DiatonicTone(a.Distance - b.Distance);
        }

        /// <summary>
        /// Increments a <see cref="DiatonicTone" /> by one.
        /// </summary>
        /// <param name="diatonicTone">The <see cref="DiatonicTone" /></param>
        /// <returns>The resulting <see cref="DiatonicTone" />.</returns>
        public static DiatonicTone operator ++(DiatonicTone diatonicTone)
        {
            return new DiatonicTone(diatonicTone.Distance + 1);
        }

        /// <summary>
        /// Decrements a <see cref="DiatonicTone" /> by one.
        /// </summary>
        /// <param name="diatonicTone">The <see cref="DiatonicTone" /></param>
        /// <returns>The resulting <see cref="DiatonicTone" />.</returns>
        public static DiatonicTone operator --(DiatonicTone diatonicTone)
        {
            return new DiatonicTone(diatonicTone.Distance - 1);
        }

        /// <summary>
        /// Gets the octave from the current DiatonicTone.
        /// </summary>
        /// <returns>The <see cref="Octave"/>.</returns>
        public Octave ToOctave()
        {
            checked
            {
                var octave = (sbyte)(Distance / 12);

                return new Octave(octave);
            }
        }

        public override string ToString()
        {
            return $"{Distance}";
        }
    }
}