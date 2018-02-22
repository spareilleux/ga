using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for a quality list.
    /// </summary>
    /// <typeparam name="TQuality">The quality type.</typeparam>
    public abstract class QualityListBase<TQuality> : IReadOnlyList<TQuality>
        where TQuality : Quality
    {
        private readonly IReadOnlyList<TQuality> _qualities;

        protected QualityListBase(AbsoluteSemitoneList absoluteSemitones)
        {
            _qualities = absoluteSemitones.Select(semitone => (TQuality) semitone).ToList().AsReadOnly();
        }

        public IEnumerator<TQuality> GetEnumerator()
        {
            return _qualities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _qualities.Count;

        public TQuality this[int index] => _qualities[index];
    }
}
