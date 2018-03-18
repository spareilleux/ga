using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// List of intervals.
    /// </summary>
    public class IntervalsList : IReadOnlyList<Interval>
    {
        private readonly IReadOnlyList<Interval> _intervals;

        public IntervalsList(IEnumerable<Interval> intervals)
        {
            _intervals = intervals.ToList().AsReadOnly();
        }

        public IntervalsList(
            IEnumerable<Semitone> absoluteSemitones,
            AccidentalKind accidentalKind) : this(FromSemitones(absoluteSemitones, accidentalKind))
        {
        }

        public IEnumerator<Interval> GetEnumerator()
        {
            return _intervals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _intervals.Count;

        public Interval this[int index] => _intervals[index];

        public override string ToString()
        {
            var result = string.Join(" ", this.Select(i => i.ToString()));

            return result;
        }

        private static IntervalsList FromSemitones(
            IEnumerable<Semitone> absoluteSemitones,
            AccidentalKind accidentalKind)
        {
            var qualities = accidentalKind == AccidentalKind.Flat
                ? absoluteSemitones.Select(Interval.GetFlat)
                : absoluteSemitones.Select(Interval.GetSharp);
            var result = new IntervalsList(qualities);

            return result;
        }
    }
}
