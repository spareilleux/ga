namespace GA.Domain.Music.Intervals
{
    public enum DiatonicInterval
    {
        Unison = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5,
        Sixth = 6,
        Seventh = 7,
        Octave = 8,
        Ninth = 9,
        Tenth = 10,
        Eleventh = 11,
        Twelfth = 12,
        Thirteenth = 13,
        Fourteenth = 14
    }

    ///// <inheritdoc cref="Semitone" />.
    ///// <summary>
    ///// Represents a diatonic interval.
    ///// </summary>
    ///// <see href="http://en.wikipedia.org/wiki/Diatonic_interval#Intervals" />.
    //public class DiatonicInterval : Semitone, IEquatable<DiatonicInterval>, IComparable<DiatonicInterval>
    //{
    //    private readonly string _name;

    //    public new static readonly DiatonicInterval Unison = new DiatonicInterval(0, ChromaticInterval.Unison, nameof(Unison));
    //    public static readonly DiatonicInterval Second = new DiatonicInterval(1, ChromaticInterval.M2, nameof(Second));
    //    public static readonly DiatonicInterval Third = new DiatonicInterval(2, ChromaticInterval.M3, nameof(Third));
    //    public static readonly DiatonicInterval Fourth = new DiatonicInterval(3, ChromaticInterval.P4, nameof(Fourth));
    //    public static readonly DiatonicInterval Fifth = new DiatonicInterval(4, ChromaticInterval.P5, nameof(Fifth));
    //    public static readonly DiatonicInterval Sixth = new DiatonicInterval(5, ChromaticInterval.M6, nameof(Sixth));
    //    public static readonly DiatonicInterval Seventh = new DiatonicInterval(6, ChromaticInterval.M7, nameof(Seventh));
    //    public new static readonly DiatonicInterval Octave = new DiatonicInterval(7, ChromaticInterval.Octave, nameof(Octave));
    //    public static readonly DiatonicInterval Ninth = new DiatonicInterval(8, ChromaticInterval.M9, nameof(Ninth));
    //    public static readonly DiatonicInterval Tenth = new DiatonicInterval(9, ChromaticInterval.M10, nameof(Tenth));
    //    public static readonly DiatonicInterval Eleventh = new DiatonicInterval(10, ChromaticInterval.P11, nameof(Eleventh));
    //    public static readonly DiatonicInterval Twelfth = new DiatonicInterval(11, ChromaticInterval.P12, nameof(Twelfth));
    //    public static readonly DiatonicInterval Thirteenth = new DiatonicInterval(12, ChromaticInterval.M13, nameof(Thirteenth));
    //    public static readonly DiatonicInterval Fourteenth = new DiatonicInterval(13, ChromaticInterval.M14, nameof(Fourteenth));

    //    public static readonly DiatonicInterval[] OneOctaveValues = {
    //                                                                    Unison,
    //                                                                    Second,
    //                                                                    Third,
    //                                                                    Fourth,
    //                                                                    Fifth,
    //                                                                    Sixth,
    //                                                                    Seventh
    //                                                                };

    //    public static readonly DiatonicInterval[] TwoOctavesValues = {
    //                                                                     Unison,
    //                                                                     Second,
    //                                                                     Third,
    //                                                                     Fourth,
    //                                                                     Fifth,
    //                                                                     Sixth,
    //                                                                     Seventh,
    //                                                                     Octave,
    //                                                                     Ninth,
    //                                                                     Tenth,
    //                                                                     Eleventh,
    //                                                                     Twelfth,
    //                                                                     Thirteenth,
    //                                                                     Fourteenth
    //                                                                 };

    //    private static readonly Dictionary<int, DiatonicInterval> _byValue;

    //    static DiatonicInterval()
    //    {
    //        _byValue = TwoOctavesValues.ToDictionary(interval => interval.Value);
    //    }

    //    private DiatonicInterval(
    //        int value,
    //        ChromaticInterval interval,
    //        string name)
    //        : base(interval)
    //    {
    //        Value = value;
    //        _name = name;
    //    }

    //    public int Value { get; }

    //    /// <summary>
    //    /// Tries to parse a <see cref="DiatonicInterval" /> from its string representation
    //    /// </summary>
    //    public static bool TryParse(string value, out DiatonicInterval interval)
    //    {
    //        interval = Unison;

    //        // Remove non-numeric characters
    //        value = Regex.Replace(value, "[^.0-9]", "");
    //        if (!int.TryParse(value, out var i) || i < 0 || i > 13) return false;

    //        interval = i - 1;
    //        return true;
    //    }

    //    /// <summary>
    //    /// Creates a diatonic interval from its string representation
    //    /// </summary>
    //    public new static DiatonicInterval Parse(string value)
    //    {
    //        if (!TryParse(value, out var result)) throw new InvalidOperationException();

    //        return result;
    //    }

    //    /// <summary>
    //    /// Converts a string into a list of diatonic interval
    //    /// </summary>
    //    public static IList<DiatonicInterval> ParseToList(string value)
    //    {
    //        return value.Split(' ', ',', ';').Select(Parse).ToList();
    //    }

    //    /// <summary>
    //    /// Take the corresponding simple interval
    //    /// </summary>
    //    public new DiatonicInterval ToSimple()
    //    {
    //        var newValue = Value % 7;

    //        return newValue;
    //    }

    //    /// <summary>
    //    /// Take the corresponding compound interval
    //    /// </summary>
    //    public new DiatonicInterval ToCompound()
    //    {
    //        var newValue = Value % 7 + 7;

    //        return newValue;
    //    }

    //    /// <summary>
    //    /// Take the interval inversion
    //    /// </summary>
    //    public DiatonicInterval ToInversion()
    //    {
    //        var simple = Value % 7;
    //        if (simple == 0)
    //        {
    //            return this;
    //        }

    //        var newValue = Value > 7
    //                         ? _byValue[14 - simple]
    //                         : _byValue[7 - Value];

    //        return newValue;
    //    }

    //    public override string ToString() => _name;

    //    public int CompareTo(DiatonicInterval other)
    //    {
    //        // ReSharper disable once ImpureMethodCallOnReadonlyValueField
    //        return Value.CompareTo(other.Value);
    //    }

    //    public bool Equals(DiatonicInterval other)
    //    {
    //        return Value == other.Value;
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        if (ReferenceEquals(null, obj)) return false;
    //        if (ReferenceEquals(this, obj)) return true;
    //        if (obj.GetType() != GetType()) return false;
    //        return Equals((DiatonicInterval)obj);
    //    }

    //    public override int GetHashCode()
    //    {
    //        unchecked
    //        {
    //            return (base.GetHashCode() * 397) ^ Value;
    //        }
    //    }

    //    public static bool operator >(DiatonicInterval a, DiatonicInterval b)
    //    {
    //        return a.Distance > b.Distance;
    //    }

    //    public static bool operator >=(DiatonicInterval a, DiatonicInterval b)
    //    {
    //        return a.Distance >= b.Distance;
    //    }

    //    public static bool operator <(DiatonicInterval a, DiatonicInterval b)
    //    {
    //        return a.Distance < b.Distance;
    //    }

    //    public static bool operator <=(DiatonicInterval a, DiatonicInterval b)
    //    {
    //        return a.Distance <= b.Distance;
    //    }

    //    /// <summary>
    //    /// Indicates whether two diatonic intervals are equal
    //    /// </summary>
    //    public static bool operator ==(DiatonicInterval a, DiatonicInterval b)
    //    {
    //        if (ReferenceEquals(a, b)) return true;

    //        return a != null && b != null && a.Distance == b.Distance;
    //    }

    //    /// <summary>
    //    /// Indicates whether two diatonic intervals are different
    //    /// </summary>
    //    public static bool operator !=(DiatonicInterval a, DiatonicInterval b)
    //    {
    //        return !(a == b);
    //    }

    //    public static implicit operator DiatonicInterval(int value)
    //    {
    //        var result = _byValue[value];

    //        return result;
    //    }

    //    public static implicit operator int(DiatonicInterval interval)
    //    {
    //        return interval.Value;
    //    }

    //    public static Interval operator +(DiatonicInterval interval, Accidental accidental)
    //    {
    //        return new Interval(interval, accidental);
    //    }

    //    public static DiatonicInterval operator +(DiatonicInterval a, DiatonicInterval b)
    //    {
    //        var newValue = a.Value + b.Value;
    //        if (newValue < 0 || newValue > 11)
    //        {
    //            throw new OverflowException();
    //        }

    //        return newValue;
    //    }

    //    public static DiatonicInterval operator -(DiatonicInterval a, DiatonicInterval b)
    //    {
    //        var newValue = a.Value - b.Value;
    //        if (newValue < 0 || newValue > 11)
    //        {
    //            throw new OverflowException();
    //        }

    //        return newValue;
    //    }

    //    public static DiatonicInterval operator ++(DiatonicInterval interval)
    //    {
    //        if (interval == Fourteenth)
    //        {
    //            throw new OverflowException();
    //        }

    //        var newValue = interval.Value + 1;

    //        return newValue;
    //    }

    //    public static DiatonicInterval operator --(DiatonicInterval interval)
    //    {
    //        if (interval == Unison)
    //        {
    //            throw new OverflowException();
    //        }

    //        var newValue = interval.Value - 1;

    //        return newValue;
    //    }

    //    public static DiatonicInterval operator !(DiatonicInterval interval)
    //    {
    //        return interval.ToInversion();
    //    }

    //    public static DiatonicInterval operator *(DiatonicInterval interval, AccidentalKind accidentalKind)
    //    {
    //        return accidentalKind == AccidentalKind.Flat
    //                   ? interval.ToInversion()
    //                   : interval;
    //    }
    //}
}