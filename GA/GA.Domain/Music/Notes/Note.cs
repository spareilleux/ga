using System;
using GA.Domain.Music.Intervals;

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

            if (Enum.TryParse<DiatonicNote>(sNote, out var diatonicNote))
            {
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

            throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return $"{DiatonicNote}{Accidental}";
        }
    }
}
