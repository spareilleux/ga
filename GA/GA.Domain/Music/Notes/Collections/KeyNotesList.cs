using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals;
using GA.Domain.Music.Intervals.Collections;

namespace GA.Domain.Music.Notes.Collections
{
    /// <inheritdoc />
    public class KeyNotesList : NotesList
    {
        private readonly Dictionary<DiatonicNote, Note> _keyNotesByDiatonicNote;
        private static readonly IDictionary<Semitone, Note> _flatNotes;
        private static readonly IDictionary<Semitone, Note> _sharpNotes;

        static KeyNotesList()
        {
            _flatNotes = new List<Note>
            {
                Note.C,
                Note.Db, Note.D,
                Note.Eb, Note.E,
                Note.F,
                Note.Gb, Note.G,
                Note.Ab, Note.A,
                Note.Bb, Note.B
            }.ToDictionary(note => note.DistanceFromC);

            _sharpNotes = new List<Note>
            {
                Note.C,  Note.Csharp,
                Note.D,  Note.Dsharp,
                Note.E,
                Note.F,  Note.Fsharp,
                Note.G,  Note.Gsharp,
                Note.A,  Note.Asharp,
                Note.B
            }.ToDictionary(note => note.DistanceFromC);
        }

        public KeyNotesList(IReadOnlyList<Note> keyNotes)
                : base(keyNotes)
        {
            _keyNotesByDiatonicNote = keyNotes.ToDictionary(note => note.DiatonicNote);
        }

        /// <summary>
        /// Gets the key note, given a diatonic note.
        /// </summary>
        /// <param name="diatonicNote">The <see cref="DiatonicNote"/>.</param>
        /// <returns>The key <see cref="Note"/>.</returns>
        public Note this[DiatonicNote diatonicNote] => _keyNotesByDiatonicNote[diatonicNote];

        /// <summary>
        /// Gets notes from a list of absolute semitones.
        /// </summary>
        /// <param name="semitones">Thje <see cref="AbsoluteSemitoneList"/>.</param>
        /// <returns></returns>
        public NotesList GetNotes(AbsoluteSemitoneList semitones)
        {
            var noteBySemitone = semitones.IsMinor ? _flatNotes : _sharpNotes;
            var notes = new List<Note>();
            foreach (var semitone in semitones)
            {
                var note = noteBySemitone[semitone];
                notes.Add(note);
            }
            var result = new NotesList(notes);

            return result;
        }
    }
}