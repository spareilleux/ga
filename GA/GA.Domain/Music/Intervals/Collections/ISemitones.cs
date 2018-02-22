using System.Collections.Generic;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// List of semitones.
    /// </summary>
    public interface ISemitones : IReadOnlyList<Semitone>
    {
        bool Contains(Semitone item);
    }
}