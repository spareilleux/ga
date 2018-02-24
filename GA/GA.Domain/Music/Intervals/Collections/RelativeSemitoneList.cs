using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals.Metadata;
using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// List of semitones (Relative).
    /// </summary>
    public class RelativeSemitoneList : ISemitones
    {
        private readonly IReadOnlyList<Semitone> _relativeSemitones;

        public RelativeSemitoneList(IEnumerable<Semitone> relativeSemitones)
        {
            _relativeSemitones = relativeSemitones.ToList().AsReadOnly();
            Absolute = ToAbsolute();
        }

        /// <summary>
        /// Gets the <see cref="Symmetry"/>.
        /// </summary>
        public Symmetry Symmetry => new Symmetry(_relativeSemitones);

        /// <summary>
        /// Gets the <see cref="AbsoluteSemitoneList"/>
        /// </summary>
        public AbsoluteSemitoneList Absolute { get; }

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
        /// Converts a string representation of a list of relative semitones into a relative semitones list.
        /// </summary>
        /// <param name="s">The <see cref="string"/> represention of the semitone relative distances.</param>
        /// <param name="separators">The <see cref="IEnumerable{Char}"/> (Optional, '-' separator is used by default)</param>\
        /// <returns>The <see cref="RelativeSemitoneList"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public static RelativeSemitoneList Parse(
            string s,
            IEnumerable<char> separators = null)
        {
            separators = separators ?? new[] { '-' };
            var semitones = s.Split(separators.ToArray()).Select(ParseSelector);
            var result = new RelativeSemitoneList(semitones);

            return result;
        }

        public IEnumerator<Semitone> GetEnumerator()
        {
            return _relativeSemitones.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _relativeSemitones.Count;

        public Semitone this[int index] => _relativeSemitones[index];
        public bool Contains(Semitone item)
        {
            var result = Absolute.Contains(item);

            return result;
        }

        private static Semitone ParseSelector(string s)
        {
            s = s?.Trim();

            if (Semitone.TryParse(s, out var semitone)) return semitone;
            if (Step.TryParse(s, out var step)) return step;
            if (Quality.TryParse(s, out var quality)) return quality;

            throw new InvalidOperationException($"Failed parsing '{s}' into {nameof(Semitone)}");
        }

        public override string ToString()
        {
            var result = string.Join("-", _relativeSemitones.Select(s => $"{s.Distance}"));

            return result;
        }
    }
}
