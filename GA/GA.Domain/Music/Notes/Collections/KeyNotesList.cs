using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GA.Domain.Music.Notes.Collections
{
    public class KeyNotesList : IReadOnlyList<Note>
    {
        private readonly IReadOnlyList<Note> _keyNotes;
        private readonly Dictionary<DiatonicNote, Note> _keyNotesByDiatonicNote;

        public KeyNotesList(
            int signedAccidentalCount,
            IReadOnlyList<Note> keyNotes)
        {
            SignedAccidentalCount = signedAccidentalCount;
            _keyNotes = keyNotes;
            _keyNotesByDiatonicNote = keyNotes.ToDictionary(note => note.DiatonicNote);
        }

        public int SignedAccidentalCount { get; }

        public IEnumerator<Note> GetEnumerator()
        {
            return _keyNotes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _keyNotes).GetEnumerator();
        }

        public int Count => _keyNotes.Count;

        public Note this[int index] => _keyNotes[index];

        /// <summary>
        /// Gets the key note, given a diatonic note.
        /// </summary>
        /// <param name="diatonicNote">The <see cref="DiatonicNote"/>.</param>
        /// <returns>The key <see cref="Note"/>.</returns>
        public Note this[DiatonicNote diatonicNote] => _keyNotesByDiatonicNote[diatonicNote];

        public override string ToString()
        {
            var result = string.Join(" ", this);

            return result;
        }
    }
}
