using System;

namespace GA.Domain.Music.Intervals
{
    public class Octave : Semitone, IEquatable<Octave>
    {
        private readonly sbyte _value;

        public Octave(sbyte value)
            : base(value * 12)
        {
            _value = value;
        }

        private Octave(int value) : this((sbyte)value)
        {
        }

        public static readonly Octave SubContraOctave = new Octave(0);
        public static readonly Octave ContraOctave = new Octave(1);
        public static readonly Octave GreatOctave = new Octave(2);
        public static readonly Octave SmallOctave = new Octave(3);
        public static readonly Octave OneLineOctave = new Octave(4);
        public static readonly Octave TwoLineOctave = new Octave(5);
        public static readonly Octave ThreeLineOctave = new Octave(6);
        public static readonly Octave FourLineOctave = new Octave(7);
        public static readonly Octave FiveLineOctave = new Octave(8);

        public bool Equals(Octave other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Octave)obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once ImpureMethodCallOnReadonlyValueField
            return _value.GetHashCode();
        }

        #region Operators

        /// <summary>
        /// Increments a <see cref="Octave" /> by one.
        /// </summary>
        /// <param name="accidental">The <see cref="Octave" /></param>
        /// <returns>The resulting <see cref="Octave" />.</returns>
        public static Octave operator ++(Octave accidental)
        {
            return new Octave(accidental._value + 1);
        }

        /// <summary>
        /// Decrements a <see cref="Octave" /> by one.
        /// </summary>
        /// <param name="accidental">The <see cref="Octave" /></param>
        /// <returns>The resulting <see cref="Octave" />.</returns>
        public static Octave operator --(Octave accidental)
        {
            return new Octave(accidental._value - 1);
        }

        /// <summary>
        /// Equality test between an <see cref="Octave" /> and an <see cref="int"/>.
        /// </summary>
        /// <param name="octave">The <see cref="Octave" /></param>
        /// <param name="i">The <see cref="int" /></param>
        /// <returns>True if the <see cref="Octave" /> is equal to the <see cref="int" /> b</returns>
        public static bool operator ==(Octave octave, int i)
        {
            return octave != null && octave._value == i;
        }

        public static bool operator !=(Octave octave, int i)
        {
            return !(octave == i);
        }

        #endregion
    }
}
