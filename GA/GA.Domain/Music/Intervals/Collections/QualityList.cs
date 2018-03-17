using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for a interval list.
    /// </summary>
    public class QualityList : IReadOnlyList<Interval>
    {
        private readonly IReadOnlyList<Interval> _qualities;

        public QualityList(
            IEnumerable<Semitone> absoluteSemitones,
            AccidentalKind accidentalKind) : this(FromSemitones(absoluteSemitones, accidentalKind))
        {
        }

        private static QualityList FromSemitones(
            IEnumerable<Semitone> absoluteSemitones, 
            AccidentalKind accidentalKind)
        {
            var qualities = accidentalKind == AccidentalKind.Flat
                ? absoluteSemitones.Select(Interval.GetFlat)
                : absoluteSemitones.Select(Interval.GetSharp);
            var result = new QualityList(qualities);

            return result;
        }

        protected QualityList(IEnumerable<Interval> qualities)
        {
            _qualities = qualities.ToList().AsReadOnly();
        }

        public IEnumerator<Interval> GetEnumerator()
        {
            return _qualities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _qualities.Count;

        public Interval this[int index] => _qualities[index];

        public override string ToString()
        {
            var result = string.Join(" ", this.Select(q => q.ToString()));

            return result;
        }
    }
}
