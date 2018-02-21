using System.Collections.Generic;

namespace GA.Domain.Music.Intervals.Collections
{
    /// <inheritdoc />
    /// <summary>
    /// Read-only list of semitones.
    /// </summary>
    public interface ISemitoneList : IReadOnlyList<Semitone>
    {
    }
}