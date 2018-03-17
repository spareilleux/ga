using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GA.Core.Extensions;
using GA.Domain.Music.Intervals.Metadata;
using GA.Domain.Music.Intervals.Qualities;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// List of semitones (Absolute).
    /// </summary>
    public class AbsoluteSemitoneList : ISemitones
    {
        private readonly ISet<Semitone> _absoluteSemitonesSet;
        protected readonly IReadOnlyList<Semitone> AbsoluteSemitones;        

        /// <summary>
        /// Converts an absolute semitones list from its string representation.
        /// </summary>
        /// <param name="s">The <see cref="string"/> represention of the semitone distances.</param>
        /// <param name="separators">The <see cref="IEnumerable{Char}"/> (Optional, ' ' separator is used by default)</param>\
        /// <returns>The <see cref="AbsoluteSemitoneList"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public static AbsoluteSemitoneList Parse(
            string s, 
            IEnumerable<char> separators = null)
        {
            separators = separators ?? new [] {' '};
            var semitones = s.Split(separators.ToArray()).Select(ParseSelector);
            var result = new AbsoluteSemitoneList(semitones);

            return result;
        }

        public AbsoluteSemitoneList(IEnumerable<Semitone> semitones)
            : this(semitones.Select(sb => (int)sb))
        {
        }

        public AbsoluteSemitoneList(params sbyte[] semitones)
            : this(semitones.Select(sb => (int)sb))
        {
        }

        public AbsoluteSemitoneList Rotate(int count)
        {                                                            
            var result = new AbsoluteSemitoneList(CombinatoricsExtensions.Rotate(this, count));

            return result;
        }

        protected AbsoluteSemitoneList(IEnumerable<int> absoluteDistances)
        {
            AbsoluteSemitones = absoluteDistances.Select(d => (Semitone)d).ToList();
            Symmetry = new Symmetry(this);
            _absoluteSemitonesSet = new SortedSet<Semitone>(AbsoluteSemitones).ToImmutableSortedSet();
        }

        /// <summary>
        /// Gets <see cref="Symmetry"/>.
        /// </summary>
        public Symmetry Symmetry { get; }

        public IEnumerator<Semitone> GetEnumerator()
        {
            return AbsoluteSemitones.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => AbsoluteSemitones.Count;

        public Semitone this[int index] => GetAbsoluteSemitone(index);

        public bool Contains(Semitone item)
        {
            var result = _absoluteSemitonesSet.Contains(item);

            return result;
        }

        /// <summary>
        /// Gets the <see cref="RelativeSemitoneList" />.
        /// </summary>
        /// <returns>The <see cref="RelativeSemitoneList"/>.</returns>
        public RelativeSemitoneList ToRelative()
        {
            var relativeSemitones = new List<Semitone>();
            for (var i = 1; i < Count; i++)
            {
                var relativeSemitone = AbsoluteSemitones[i] - AbsoluteSemitones[i - 1];
                relativeSemitones.Add(relativeSemitone);
            }

            var result = new RelativeSemitoneList(relativeSemitones);

            return result;
        }

        public override string ToString()
        {
            var result = string.Join(", ", AbsoluteSemitones.Select(s => $"{s}"));

            return result;
        }

        private Semitone GetAbsoluteSemitone(int index)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException($"'{nameof(index)}' must be positive (= {index})");
            }

            if (index >= Count)
            {
                throw new IndexOutOfRangeException($"'{nameof(index)}' must be less than {Count} (= {index})");
            }

            return AbsoluteSemitones[index];
        }

        private static Semitone ParseSelector(string s)
        {
            s = s?.Trim();
            if (Semitone.TryParse(s, out var semitone)) return semitone;
            if (Interval.TryParse(s, out var quality)) return quality;

            throw new InvalidOperationException($"Failed parsing '{s}' into {nameof(Semitone)}");
        }
    }
}
