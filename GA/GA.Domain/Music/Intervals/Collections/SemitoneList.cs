using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals.Metadata;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    public class SemitoneList : ISemitoneList
    {
        protected readonly IReadOnlyList<Semitone> Semitones;

        /// <summary>
        /// Converts the string representation of a semitones to its semitones collection equivalent.
        /// </summary>
        /// <param name="distances">The <see cref="string"/> represention on the semitone distances (int separated by space character or ';' or ',')</param>
        /// <returns>The <see cref="SemitoneList"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public static SemitoneList Parse(string distances)
        {
            var semitones =
                distances.Split(' ', ';', ',')
                    .Select(Semitone.Parse);

            var result = new SemitoneList(semitones);

            return result;
        }

        public SemitoneList(params sbyte[] semitones)
            : this(semitones.Select(sb => (int)sb))
        {
        }

        protected SemitoneList(IEnumerable<int> distances)
        {
            Semitones = distances.Select(d => (Semitone)d).ToList();
            Symmetry = new Symmetry(this);
        }

        public SemitoneList(IEnumerable<Semitone> semitones)
        {
            Semitones = semitones.ToList();
        }


        public IEnumerator<Semitone> GetEnumerator()
        {
            return Semitones.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => Semitones.Count;

        public Semitone this[int index] => Semitones[index];

        public Symmetry Symmetry { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the <see cref="T:GA.Domain.Music.Intervals.Collections.RelativeSemitoneList" />.
        /// </summary>
        /// <returns>The <see cref="RelativeSemitoneList"/>.</returns>
        public RelativeSemitoneList ToRelative()
        {
            var relativeSemitones = new List<Semitone>();
            for (var i = 1; i < Count; i++)
            {
                var relativeSemitone = Semitones[i] - Semitones[i - 1];
                relativeSemitones.Add(relativeSemitone);
            }

            var result = new RelativeSemitoneList(relativeSemitones);

            return result;
        }

        public override string ToString()
        {
            var result = string.Join(", ", Semitones.Select(s => $"{s}"));

            return result;
        }
    }
}
