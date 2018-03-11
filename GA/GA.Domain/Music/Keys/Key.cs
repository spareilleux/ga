using System;
using System.Collections.Generic;
using System.Linq;
using GA.Core.Extensions;
using GA.Domain.Extensions;
using GA.Domain.Music.Intervals;
using GA.Domain.Music.Notes;
using GA.Domain.Music.Scales;

namespace GA.Domain.Music.Keys
{
    public class Key
    {
        private static readonly Dictionary<int, IReadOnlyList<Note>> _notesByKey;

        static Key()
        {
            _notesByKey = new Dictionary<int, IReadOnlyList<Note>>();
            foreach (var accidentalCount in Enumerable.Range(-7, 15))
            {
                _notesByKey[accidentalCount] = GetNotes(accidentalCount);
            }            
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a key instance (Major key).
        /// </summary>
        /// <param name="majorKey">The <see cref="MajorKey" />.</param>
        public Key(MajorKey majorKey) :
            this((int)majorKey)
        {
            Mode = KeyMode.Major;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a key instance (Minor key).
        /// </summary>
        /// <param name="minorKey">The <see cref="MinorKey" />.</param>
        public Key(MinorKey minorKey) :
            this((int)minorKey)
        {
            Mode = KeyMode.Minor;
        }

        /// <summary>
        /// Creates a key instance (signed accidental count).
        /// </summary>
        /// <param name="signedAccidentalCount">Positive for sharp, negative for flat.</param>
        public Key(int signedAccidentalCount)
        {
            AccidentalCount = Math.Abs(signedAccidentalCount);
            AccidentalKind = signedAccidentalCount >= 0 ? AccidentalKind.Sharp : AccidentalKind.Flat;
            MajorKey = (MajorKey)signedAccidentalCount;
            MinorKey = (MinorKey)signedAccidentalCount;
        }

        public Note Root => MajorKey.GetRoot();

        /// <summary>
        /// Gets the number of accidentals.
        /// </summary>
        public int AccidentalCount { get; }

        /// <summary>
        /// Gets the <see cref="AccidentalKind"/>.
        /// </summary>
        public AccidentalKind AccidentalKind { get; }

        /// <summary>
        /// Gets the <see cref="KeyMode"/>.
        /// </summary>
        public KeyMode Mode { get; }

        /// <summary>
        /// Gets the <see cref="MajorKey"/>.
        /// </summary>
        public MajorKey MajorKey { get; }

        /// <summary>
        /// Gets the <see cref="MinorKey"/>.
        /// </summary>
        public MinorKey MinorKey { get; }

        private static IReadOnlyList<Note> GetNotes(int accidentalCount)
        {
            var majorKey = (MajorKey)accidentalCount;
            var note = majorKey.GetRoot();
            var notes = new List<Note> { note };
            var diatonicNote = note.DiatonicNote;
            for (var i = 0; i < 6; i++)
            {
                diatonicNote = note.DiatonicNote.Next();
                var diatonicNotesDistance = (diatonicNote - note.DiatonicNote + 12) % 12;
                var accidentalValue = note.Accidental + ScaleDefinition.Major[i] - diatonicNotesDistance;
                var accidental = new Accidental(accidentalValue);
                note = new Note(diatonicNote, accidental);
                notes.Add(note);
            }

            var result = notes.AsReadOnly();

            return result;
        }
    }
}
