using System;
using System.Collections.Generic;

namespace GA.Domain.Music
{
    using Core.Attributes;

    /// <inheritdoc cref="Semitone" />
    /// <summary>
    /// Note or interval accidental.
    /// </summary>
    /// <see href="http://en.wikipedia.org/wiki/Accidental_(music)" />
    public class Accidental : Semitone, IEquatable<Accidental>
    {
        private readonly sbyte? _value;

        [Descriptions("bbb", "\u266D\u266D\u266D")]
        public static readonly Accidental TripleFlat = new Accidental(-3);

        [Descriptions("bb", "\u266D\u266D")]
        public static readonly Accidental DoubleFlat = new Accidental(-2);

        [Descriptions("b", "\u266D")]
        public static readonly Accidental Flat = new Accidental(-1);

        [Descriptions("")]
        public static readonly Accidental None = new Accidental(0);

        [Descriptions("\u266E", "n")]
        public static readonly Accidental Natural = new Accidental(null);

        [Descriptions("#", "\u266F")]
        public static readonly Accidental Sharp = new Accidental(1);

        [Descriptions("x", "\u266F\u266F")]
        public static readonly Accidental DoubleSharp = new Accidental(2);

        public static readonly IReadOnlyCollection<Accidental> Values = new List<Accidental>
        {
            TripleFlat, DoubleFlat, Flat, None, Natural, Sharp, DoubleSharp
        }.AsReadOnly();

        private Accidental(sbyte? value)
            : base(value ?? 0)
        {
            if (value.HasValue && (value.Value < -3 || value.Value > 2)) throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} value must be between -3 and 2, or null");

            _value = value;
        }

        public override int Distance => _value ?? 0;

        /// <summary>
        /// Tries to convert a string into an accidental
        /// </summary>
        /// <param name="s">The input string</param>
        /// <param name="accidental">The <see cref="Accidental" /></param>
        /// <returns>True if succeeded</returns>
        public static bool TryParse(string s, out Accidental accidental)
        {
            // TODO: Use DescriptionAttributes

            switch (s)
            {
                case "\u266D\u266D\u266D":
                case "bbb":
                    accidental = TripleFlat;
                    return true;

                case "\u266D\u266D":
                case "bb":
                    accidental = DoubleFlat;
                    return true;

                case "\u266D":
                case "b":
                    accidental = Flat;
                    return true;

                case "":
                    accidental = None;
                    return true;

                case "\u266F":
                case "#":
                case "S":
                    accidental = Sharp;
                    return true;

                case "x":
                    accidental = DoubleSharp;
                    return true;

                case "\u266E":
                    accidental = Natural;
                    return true;

                default:
                    accidental = None;
                    return false;
            }
        }

        /// <summary>
        /// Converts a string into an accidental
        /// </summary>
        /// <param name="s">The input string</param>
        /// <returns>The <see cref="Accidental" /> parsed from the string</returns>
        public static Accidental Parse(string s)
        {
            if (!TryParse(s, out var result))
            {
                throw new InvalidOperationException("Accidental parsing error");
            }
            return result;
        }

        public static implicit operator Accidental(Direction direction)
        {
            return new Accidental((sbyte)direction);
        }

        public override string ToString()
        {
            // TODO: Use DescriptionAttributes

            switch (_value)
            {
                case -3:
                    return "bbb"; // Double flat signs
                case -2:
                    return "bb"; // Double flat signs
                case -1:
                    return "b"; // Flat sign
                case 0:
                    return string.Empty;
                case null:
                    return "\u266E"; // Natural sign
                case 1:
                    return "#"; // Sharp sign
                case 2:
                    return "x";
                case 3:
                    return "???";
                default:
                    return string.Empty;
            }
        }

        public bool Equals(Accidental other)
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
            return Equals((Accidental) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable once ImpureMethodCallOnReadonlyValueField
                return (base.GetHashCode() * 397) ^ _value.GetHashCode();
            }
        }
    }
}