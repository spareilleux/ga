﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals.Metadata;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// Collection of semitones (Absolute).
    /// </summary>
    public class AbsoluteSemitoneList : ISemitoneList
    {
        protected readonly IReadOnlyList<Semitone> AbsoluteSemitones;

        /// <summary>
        /// Converts the string representation of a semitones to its semitones collection equivalent.
        /// </summary>
        /// <param name="distances">The <see cref="string"/> represention on the semitone distances.</param>\
        /// <param name="separators">The <see cref="System.array{char}"/> (Optional, space separator is used by default)</param>\
        /// <returns>The <see cref="AbsoluteSemitoneList"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public static AbsoluteSemitoneList Parse(
            string distances, 
            char[] separators = null)
        {
            separators = separators ?? new [] {' '};
            var semitones = distances.Split(separators).Select(Semitone.Parse);
            var result = new AbsoluteSemitoneList(semitones);

            return result;
        }

        public AbsoluteSemitoneList(params sbyte[] semitones)
            : this(semitones.Select(sb => (int)sb))
        {
        }

        protected AbsoluteSemitoneList(IEnumerable<int> distances)
        {
            AbsoluteSemitones = distances.Select(d => (Semitone)d).ToList();
            Symmetry = new Symmetry(this);
        }

        public AbsoluteSemitoneList(IEnumerable<Semitone> semitones)
        {
            AbsoluteSemitones = semitones.ToList().AsReadOnly();
        }

        public IEnumerator<Semitone> GetEnumerator()
        {
            return AbsoluteSemitones.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => AbsoluteSemitones.Count;

        public Semitone this[int index] => AbsoluteSemitones[index];

        /// <summary>
        /// Gets <see cref="Symmetry"/>.
        /// </summary>
        public Symmetry Symmetry { get; }

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
    }
}