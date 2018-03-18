using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GA.Domain.Music.Notes.Collections
{
    public class NotesList : IReadOnlyList<Note>
    {
        private readonly IReadOnlyList<Note> _notes;

        /// <summary>
        /// Converts an list of notes from its string representation.
        /// </summary>
        /// <param name="s">The <see cref="string"/> (e.g. "C D E F").</param>
        /// <param name="separators">The <see cref="IEnumerable{Char}"/> (Optional, ' ' separator is used by default)</param>\
        /// <returns>The <see cref="NotesList"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public static NotesList Parse(
            string s,
            IEnumerable<char> separators = null)
        {
            separators = separators ?? new[] { ' ' };
            var notes = s.Split(separators.ToArray()).Select(Note.Parse);
            var result = new NotesList(notes);

            return result;
        }

        public NotesList(IEnumerable<Note> notes)
        {
            _notes = notes.ToList().AsReadOnly();
        }

        public IEnumerator<Note> GetEnumerator()
        {
            return _notes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _notes).GetEnumerator();
        }

        public int Count => _notes.Count;

        public Note this[int index] => _notes[index];

        public override string ToString()
        {
            var result = string.Join(" ", this);

            return result;
        }
    }
}
