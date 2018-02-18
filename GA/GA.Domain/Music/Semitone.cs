using System;
using System.Collections.Generic;

namespace GA.Domain.Music
{
    /// <inheritdoc cref="IEquatable{Semitone}" />
    /// <inheritdoc cref="IComparable{Semitone}" />
    /// <summary>
    /// Semitone number (See http://en.wikipedia.org/wiki/Semitone)
    /// </summary>
    public class Semitone : IEquatable<Semitone>, IComparable<Semitone>
    {
        protected Semitone(sbyte value)
        {
            Distance = value;
        }

        protected Semitone(int value)
        {
            checked
            {
                Distance = (sbyte)value;
            }
        }

        /// <summary>
        /// Gets the <see cref="Direction"/>.
        /// </summary>
        public Direction Direction => (Direction)Math.Sign(Distance);

        /// <summary>
        /// Gets the distance in semitones (Signed).
        /// </summary>
        public virtual int Distance { get; }

        /// <summary>
        /// Gets the absolute distance in semitones.
        /// </summary>
        public virtual int AbsoluteDistance => Math.Abs(Distance);

        public bool Equals(Semitone other)
        {
            return Distance == other.Distance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Semitone semitone && Equals(semitone);
        }

        public override int GetHashCode()
        {
            return Distance;
        }

        public int CompareTo(Semitone other)
        {
            return Comparer<int>.Default.Compare(Distance, other.Distance);
        }

        public static implicit operator Semitone(sbyte value)
        {
            return new Semitone(value);
        }

        public static implicit operator int(Semitone semitone)
        {
            return semitone.Distance;
        }

        /// <summary>
        /// Inverts a <see cref="Semitone" /> object.
        /// </summary>
        /// <param name="semitone">The <see cref="Semitone" /></param>
        /// <returns>The inverted <see cref="Semitone" />.</returns>
        public static Semitone operator !(Semitone semitone)
        {
            return new Semitone(-semitone.Distance);
        }

        /// <summary>
        /// "Is Greater Than" comparison between two <see cref="Semitone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="Semitone" /></param>
        /// <param name="b">The second <see cref="Semitone" /></param>
        /// <returns>True if the <see cref="Semitone" /> a is greater than to the <see cref="Semitone" /> b</returns>
        public static bool operator >(Semitone a, Semitone b)
        {
            return a.Distance >= b.Distance;
        }

        /// <summary>
        /// "Is Greater Or Equal" comparison between two <see cref="Semitone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="Semitone" /></param>
        /// <param name="b">The second <see cref="Semitone" /></param>
        /// <returns>True if the <see cref="Semitone" /> a is greater than or equal to the <see cref="Semitone" /> b</returns>
        public static bool operator >=(Semitone a, Semitone b)
        {
            return a.Distance >= b.Distance;
        }

        /// <summary>
        /// "Is Less Than" comparison between two <see cref="Semitone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="Semitone" /></param>
        /// <param name="b">The second <see cref="Semitone" /></param>
        /// <returns>True if the <see cref="Semitone" /> a is less than the <see cref="Semitone" /> b</returns>
        public static bool operator <(Semitone a, Semitone b)
        {
            return a.Distance < b.Distance;
        }

        /// <summary>
        /// "Is Less Or Equal" comparison between two <see cref="Semitone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="Semitone" /></param>
        /// <param name="b">The second <see cref="Semitone" /></param>
        /// <returns>True if the <see cref="Semitone" /> a is less than or equal to the <see cref="Semitone" /> b</returns>
        public static bool operator <=(Semitone a, Semitone b)
        {
            return a.Distance <= b.Distance;
        }

        /// <summary>
        /// Equality test between two <see cref="Semitone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="Semitone" /></param>
        /// <param name="b">The second <see cref="Semitone" /></param>
        /// <returns>True if the <see cref="Semitone" /> a is equal to the <see cref="Semitone" /> b</returns>
        public static bool operator ==(Semitone a, Semitone b)
        {
            if (ReferenceEquals(a, b)) return true;
            return a != null && b != null && a.Distance == b.Distance;
        }

        /// <summary>
        /// Difference test between two <see cref="Semitone" /> objects.
        /// </summary>
        /// <param name="a">The first <see cref="Semitone" /></param>
        /// <param name="b">The second <see cref="Semitone" /></param>
        /// <returns>True if the <see cref="Semitone" /> a is different than the <see cref="Semitone" /> b</returns>
        public static bool operator !=(Semitone a, Semitone b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Adds two <see cref="Semitone" /> objects
        /// </summary>
        /// <param name="a">The first <see cref="Semitone" /></param>
        /// <param name="b">The second <see cref="Semitone" /></param>
        /// <returns>The sum of the two <see cref="Semitone" /> objects</returns>
        public static Semitone operator +(Semitone a, Semitone b)
        {
            return new Semitone(a.Distance + b.Distance);
        }

        /// <summary>
        /// Subtract an <see cref="Semitone" /> to another one
        /// </summary>
        /// <param name="a">The first <see cref="Semitone" /></param>
        /// <param name="b">The second <see cref="Semitone" /></param>
        /// <returns>The <see cref="Semitone" /> subtraction result</returns>
        public static Semitone operator -(Semitone a, Semitone b)
        {
            return new Semitone(a.Distance - b.Distance);
        }
    }
}