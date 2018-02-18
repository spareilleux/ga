using System.Collections.Generic;
using GA.Domain.Music.Intervals.Metadata;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// List of semitones (Absolute).
    /// </summary>
    public interface ISemitoneList : IReadOnlyList<Semitone>
    {
        /// <summary>
        /// Gets the <see cref="Symmetry"/>.
        /// </summary>
        Symmetry Symmetry { get; }

        /// <summary>
        /// Gets the <see cref="RelativeSemitoneList"/>.
        /// </summary>
        /// <returns></returns>
        RelativeSemitoneList ToRelative();
    }
}