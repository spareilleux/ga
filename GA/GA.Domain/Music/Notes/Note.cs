using System;
using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals;
using GA.Domain.Music.Intervals.Qualities;
using GA.Domain.Music.Keys;

namespace GA.Domain.Music.Notes
{
    public class Note : IEquatable<Note>, IComparable<Note>
    {
        private static readonly ILookup<Note, Note> _enharmonics = GetEnharmonicsLookup();
        public static IEqualityComparer<Note> EnharmonicEqualityComparer => EnharmonicComparer.Instance;

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
        public static Note Esharp = new Note(DiatonicNote.E, Accidental.Sharp);
        public static Note Fsharp = new Note(DiatonicNote.F, Accidental.Sharp);
        public static Note Gsharp = new Note(DiatonicNote.G, Accidental.Sharp);
        public static Note Asharp = new Note(DiatonicNote.A, Accidental.Sharp);
        public static Note Bsharp = new Note(DiatonicNote.B, Accidental.Sharp);
        public static Note Cb = new Note(DiatonicNote.C, Accidental.Flat);
        public static Note Db = new Note(DiatonicNote.D, Accidental.Flat);
        public static Note Eb = new Note(DiatonicNote.E, Accidental.Flat);
        public static Note Fb = new Note(DiatonicNote.F, Accidental.Flat);
        public static Note Gb = new Note(DiatonicNote.G, Accidental.Flat);
        public static Note Ab = new Note(DiatonicNote.A, Accidental.Flat);
        public static Note Bb = new Note(DiatonicNote.B, Accidental.Flat);


        public Note(
            DiatonicNote diatonicNote,
            Accidental accidental)
        {
            DiatonicNote = diatonicNote;
            Accidental = accidental;
            DistanceFromC = new Semitone((int)DiatonicNote + Accidental);
        }

        /// <summary>
        /// Gets the <see cref="DiatonicNote"/>.
        /// </summary>
        public DiatonicNote DiatonicNote { get; }

        /// <summary>
        /// Gets the <see cref="Accidental"/>.
        /// </summary>
        public Accidental Accidental { get; }

        /// <summary>
        /// Get the <see cref="Semitone"/> distance from C.
        /// </summary>
        public Semitone DistanceFromC { get; }

        /// <summary>
        /// Gets the enharmonic <see cref="IReadOnlyCollection{Note}"/>
        /// </summary>
        public IReadOnlyCollection<Note> Enharmonics => _enharmonics[this].ToList().AsReadOnly();

        public bool IsEnharmonicWith(Note other)
        {
            var result = AreEnharmonics(this, other);

            return result;
        }

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

        /// <summary>
        /// Gets the interval between two notes.
        /// </summary>
        /// <param name="note1">The first <see cref="Note"/>.</param>
        /// <param name="note2">The second <see cref="Note"/>.</param>
        /// <returns></returns>
        public static Interval operator -(Note note1, Note note2)
        {
            var keys = Key.Major[note2];
            var key = keys.First();
            var result = note2 - key;

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
            var result = (Semitone)((int)note.DiatonicNote + note.Accidental);

            return result;
        }

        public override string ToString()
        {
            return $"{DiatonicNote}{Accidental}";
        }

        private static bool AreEnharmonics(Note note1, Note note2)
        {
            var distance1 = (int)note1.DiatonicNote + note1.Accidental;
            var distance2 = (int)note2.DiatonicNote + note2.Accidental;

            var result = distance1 == distance2;

            return result;
        }

        private static ILookup<Note, Note> GetEnharmonicsLookup()
        {
            var oneOctaveNotes = (from diatonicNote in Enumerable.Range(0, 7).Select(i => (DiatonicNote)i)
                                  from accidental in Accidental.Values
                                  select new Note(diatonicNote, accidental)).ToArray();

            var result = (from n1 in oneOctaveNotes
                          from n2 in oneOctaveNotes
                          where n1.IsEnharmonicWith(n2) && !ReferenceEquals(n1, n2)
                          select new { n1, n2 }).ToLookup(p => p.n1, p => p.n2);

            return result;
        }

        public bool Equals(Note other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DiatonicNote == other.DiatonicNote && Accidental.Equals(other.Accidental);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Note)obj);
        }

        public override int GetHashCode()
        {
            var result = (int)DistanceFromC;

            return result;
        }

        private class EnharmonicComparer : IEqualityComparer<Note>
        {
            public static readonly IEqualityComparer<Note> Instance = new EnharmonicComparer();

            public bool Equals(Note x, Note y)
            {
                var result = AreEnharmonics(x, y);

                return result;
            }

            public int GetHashCode(Note obj)
            {
                return obj.GetHashCode();
            }

            public override string ToString()
            {
                return "Enharmonic";
            }
        }

        public int CompareTo(Note other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var diatonicNoteResult = DiatonicNote.CompareTo(other.DiatonicNote);
            if (diatonicNoteResult != 0) return diatonicNoteResult;

            var accidentalResult = Accidental.CompareTo(other.Accidental);
            return accidentalResult;
        }
    }
}
