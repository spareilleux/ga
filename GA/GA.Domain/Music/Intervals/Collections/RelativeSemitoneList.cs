using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GA.Domain.Music.Intervals.Metadata;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// List  of semitones (Relative).
    /// </summary>
    public class RelativeSemitoneList : ISemitones
    {
        private readonly IReadOnlyList<Semitone> _relativeSemitones;

        public RelativeSemitoneList(IEnumerable<Semitone> relativeSemitones)
        {
            _relativeSemitones = relativeSemitones.ToList().AsReadOnly();            
        }

        /// <summary>
        /// Gets the <see cref="Symmetry"/>.
        /// </summary>
        public Symmetry Symmetry => new Symmetry(_relativeSemitones);

        /// <summary>
        /// Gets the <see cref="AbsoluteSemitoneList"/>
        /// </summary>
        public AbsoluteSemitoneList Absolute => ToAbsolute();

        /// <summary>
        /// Gets the <see cref="AbsoluteSemitoneList"/> (Absolute).
        /// </summary>
        /// <returns>The <see cref="AbsoluteSemitoneList"/></returns>
        public AbsoluteSemitoneList ToAbsolute()
        {
            var semitone = Semitone.Unison;
            var semitones = new List<Semitone>();
            foreach (var increment in _relativeSemitones)
            {
                semitones.Add(semitone);
                semitone += increment;
            }
            semitones.Add(semitone);

            var result = new AbsoluteSemitoneList(semitones);

            return result;
        }

        /// <summary>
        /// Converts the string representation of a semitones to its semitones collection equivalent (Relative).
        /// </summary>
        /// <param name="relativeDistances">The <see cref="string"/> represention on the semitone relative distances (int separated by a '-' character)</param>
        /// <returns>The <see cref="AbsoluteSemitoneList"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public static RelativeSemitoneList Parse(string relativeDistances)
        {
            var relativeSemitones = AbsoluteSemitoneList.Parse(relativeDistances, new [] {'-'});
            var result = new RelativeSemitoneList(relativeSemitones);

            return result;
        }

        public IEnumerator<Semitone> GetEnumerator()
        {
            return _relativeSemitones.GetEnumerator();
        }

        public override string ToString()
        {
            var result = string.Join("-", _relativeSemitones.Select(s => $"{s}"));

            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _relativeSemitones.Count;

        public Semitone this[int index] => _relativeSemitones[index];
    }
}
