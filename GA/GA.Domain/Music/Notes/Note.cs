using System;
using GA.Core.Extensions;
using GA.Domain.Music.Intervals;
using GA.Domain.Music.Intervals.Qualities;
using GA.Domain.Music.Scales;

namespace GA.Domain.Music.Notes
{
    public class Note
    {
        public Note(DiatonicNote diatonicNote)
            : this(diatonicNote, Accidental.None)
        {
        }

        public static Note C = new Note(DiatonicNote.C);
        public static Note D = new Note(DiatonicNote.D);
        public static Note E = new Note(DiatonicNote.E);
        public static Note F = new Note(DiatonicNote.F);
        public static Note G = new Note(DiatonicNote.G);
        public static Note A = new Note(DiatonicNote.A);
        public static Note B = new Note(DiatonicNote.B);
        public static Note Csharp = new Note(DiatonicNote.C, Accidental.Sharp);
        public static Note Dsharp = new Note(DiatonicNote.D, Accidental.Sharp);
        public static Note Fsharp = new Note(DiatonicNote.F, Accidental.Sharp);
        public static Note Gsharp = new Note(DiatonicNote.G, Accidental.Sharp);
        public static Note Asharp = new Note(DiatonicNote.A, Accidental.Sharp);
        public static Note Db = new Note(DiatonicNote.C, Accidental.Flat);
        public static Note Eb = new Note(DiatonicNote.D, Accidental.Flat);
        public static Note Gb = new Note(DiatonicNote.F, Accidental.Flat);
        public static Note Ab = new Note(DiatonicNote.G, Accidental.Flat);
        public static Note Bb = new Note(DiatonicNote.A, Accidental.Flat);


        public Note(DiatonicNote diatonicNote, Accidental accidental)
        {
            DiatonicNote = diatonicNote;
            Accidental = accidental;
        }

        /// <summary>
        /// gets the <see cref="DiatonicNote"/>.
        /// </summary>
        public DiatonicNote DiatonicNote { get; }

        /// <summary>
        /// gets the <see cref="Accidental"/>.
        /// </summary>
        public Accidental Accidental { get; }

        public static Note Parse(string s)
        {
            var sNote = s.Substring(0, 1);

            if (!Enum.TryParse<DiatonicNote>(sNote, out var diatonicNote)) throw new InvalidOperationException();

            if (s.Length == 1)
            {
                var result = new Note(diatonicNote);

                return result;
            }
            else
            {
                var sAccidental = s.Substring(1, 1);
                var accidental = Accidental.Parse(sAccidental);
                var result = new Note(diatonicNote, accidental);

                return result;
            }
        }

        public static Semitone operator -(Note note1, Note note2)
        {
            var distance = (int)note1.DiatonicNote - (int)note2.DiatonicNote + (note1.Accidental - note2.Accidental);
            while (distance < 0)
            {
                distance += 12;
            }
            var result = (Semitone) distance;

            return result;
        }

        /// <summary>
        /// Adds an accidental to a note.
        /// </summary>
        /// <param name="note">The <see cref="Note" /></param>
        /// <param name="accidental">The <see cref="Accidental" /></param>
        /// <returns>The <see cref="Note" />.</returns>
        public static Note operator +(Note note, Accidental accidental)
        {
            return new Note(note.DiatonicNote, note.Accidental + accidental);
        }

        public static implicit operator Semitone(Note note)
        {
            var result = (Semitone)((int) note.DiatonicNote + note.Accidental);

            return result;
        }

        public override string ToString()
        {
            return $"{DiatonicNote}{Accidental}";
        }
    }
}
