using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals.Metadata;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <summary>
    /// Collection of semitones (Relative).
    /// </summary>
    public class RelativeSemitoneList
    {
        public RelativeSemitoneList(IEnumerable<Semitone> relativeSemitones)
        {
            Relative = new SemitoneList(relativeSemitones);
        }

        /// <summary>
        /// Gets the relative <see cref="SemitoneList"/>.
        /// </summary>
        public SemitoneList Relative { get; }

        /// <summary>
        /// Gets the <see cref="Symmetry"/>.
        /// </summary>
        public Symmetry Symmetry => new Symmetry(Relative);

        /// <summary>
        /// Gets the absolute <see cref="SemitoneList"/>.
        /// </summary>
        /// <returns></returns>
        public SemitoneList ToAbsolute()
        {
            var semitone = Semitone.Unison;
            var semitones = new List<Semitone>();
            foreach (var incremengt in Relative)
            {
                semitones.Add(semitone);
                semitone += incremengt;
            }
            semitones.Add(semitone);

            var result = new SemitoneList(semitones);

            return result;
        }

        /// <summary>
        /// Converts the string representation of a semitones to its semitones collection equivalent (Relative).
        /// </summary>
        /// <param name="relativeDistances">The <see cref="string"/> represention on the semitone relative distances (int separated by space character or ';' or ',')</param>
        /// <returns>The <see cref="SemitoneList"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public static RelativeSemitoneList Parse(string relativeDistances)
        {
            var relativeSemitones = SemitoneList.Parse(relativeDistances);
            var result = new RelativeSemitoneList(relativeSemitones);

            return result;
        }

        public override string ToString()
        {
            var result = string.Join("-", Relative.Select(s => $"{s}"));

            return result;
        }
    }
}
