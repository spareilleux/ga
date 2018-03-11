using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for a semitoneQuality list.
    /// </summary>
    public class QualityList : IReadOnlyList<SemitoneQuality>
    {
        private readonly IReadOnlyList<SemitoneQuality> _qualities;

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
                ? absoluteSemitones.Select(SemitoneQuality.GetFlat)
                : absoluteSemitones.Select(SemitoneQuality.GetSharp);
            var result = new QualityList(qualities);

            return result;
        }

        protected QualityList(IEnumerable<SemitoneQuality> qualities)
        {
            _qualities = qualities.ToList().AsReadOnly();
        }

        public IEnumerator<SemitoneQuality> GetEnumerator()
        {
            return _qualities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _qualities.Count;

        public SemitoneQuality this[int index] => _qualities[index];

        public override string ToString()
        {
            var result = string.Join(" ", this.Select(q => q.ToString()));

            return result;
        }
    }
}
