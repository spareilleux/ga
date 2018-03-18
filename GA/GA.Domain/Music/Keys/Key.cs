using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GA.Core.Extensions;
using GA.Core.Interfaces;
using GA.Core.Models;
using GA.Domain.Extensions;
using GA.Domain.Music.Intervals;
using GA.Domain.Music.Intervals.Qualities;
using GA.Domain.Music.Notes;
using GA.Domain.Music.Notes.Collections;
using GA.Domain.Music.Scales;

namespace GA.Domain.Music.Keys
{
    public class Key
    {
        private static readonly Dictionary<int, KeyNotesList> _notesByKey;

        static Key()
        {
            // Notes by key
            _notesByKey = new Dictionary<int, KeyNotesList>();
            foreach (var accidentalCount in Enumerable.Range(-7, 15))
            {
                _notesByKey[accidentalCount] = GetNotes(accidentalCount);
            }
        }

        /// <summary>
        /// Gets the <see cref="MajorKeys"/>.
        /// </summary>
        public static MajorKeys Major = new MajorKeys();

        /// <summary>
        /// Gets the <see cref="MinorKeys"/>.
        /// </summary>
        public static MinorKeys Minor = new MinorKeys();

        /// <inheritdoc />
        /// <summary>
        /// Creates a key instance (Major key).
        /// </summary>
        /// <param name="majorKey">The <see cref="MajorKey" />.</param>
        public Key(MajorKey majorKey) :
            this((int)majorKey)
        {
            KeyMode = KeyMode.Major;            
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a key instance (Minor key).
        /// </summary>
        /// <param name="minorKey">The <see cref="MinorKey" />.</param>
        public Key(MinorKey minorKey) :
            this((int)minorKey)
        {
            KeyMode = KeyMode.Minor;
        }

        /// <summary>
        /// Creates a key instance (signed accidental count).
        /// </summary>
        /// <param name="signedAccidentalCount">Positive for sharp, negative for flat.</param>
        public Key(int signedAccidentalCount)
        {
            SignedAccidentalCount = signedAccidentalCount;
            AbsoluteAccidentalCount = Math.Abs(signedAccidentalCount);
            AccidentalKind = signedAccidentalCount >= 0 ? AccidentalKind.Sharp : AccidentalKind.Flat;
            MajorKey = (MajorKey)signedAccidentalCount;
            MinorKey = (MinorKey)signedAccidentalCount;
        }

        public Note Root => MajorKey.GetRoot();

        /// <summary>
        /// Gets the <see cref="KeyNotesList"/>.
        /// </summary>
        public KeyNotesList Notes => _notesByKey[SignedAccidentalCount];

        /// <summary>
        /// Gets the number of accidentals (Signed).
        /// </summary>
        public int SignedAccidentalCount { get; }

        /// <summary>
        /// Gets the number of accidentals (Absolute).
        /// </summary>
        public int AbsoluteAccidentalCount { get; }

        /// <summary>
        /// Gets the <see cref="AccidentalKind"/>.
        /// </summary>
        public AccidentalKind AccidentalKind { get; }

        /// <summary>
        /// Gets the <see cref="Keys.KeyMode"/>.
        /// </summary>
        public KeyMode KeyMode { get; }

        /// <summary>
        /// Gets the <see cref="MajorKey"/>.
        /// </summary>
        public MajorKey MajorKey { get; }

        /// <summary>
        /// Gets the <see cref="MinorKey"/>.
        /// </summary>
        public MinorKey MinorKey { get; }

        public string Name => GetName();

        /// <summary>
        /// Gets the interval from key root.
        /// </summary>
        /// <param name="note"></param>
        /// <returns>The <see cref="Interval"/>.</returns>
        public Interval GetIntervalFromRoot(Note note)
        {
            var result = note - this;

            return result;
        }

        public override string ToString()
        {
            return Name;
        }

        private static KeyNotesList GetNotes(int signedAccidentalCount)
        {
            var majorKey = (MajorKey)signedAccidentalCount;
            var note = majorKey.GetRoot();
            var notes = new List<Note> { note };
            for (var i = 0; i < 6; i++)
            {
                var diatonicNote = note.DiatonicNote.Next();
                var diatonicNotesDistance = (diatonicNote - note.DiatonicNote + 12) % 12;
                var accidentalValue = note.Accidental + ScaleDefinition.Major[i] - diatonicNotesDistance;
                var accidental = new Accidental(accidentalValue);
                note = new Note(diatonicNote, accidental);
                notes.Add(note);
            }

            var result = new KeyNotesList(notes);

            return result;
        }

        private string GetName()
        {
            var key = KeyMode == KeyMode.Minor ? (Enum)MinorKey : MajorKey;
            var result = key.GetFieldDescription();

            return result;
        }

        /// <summary>
        /// Gets the interval between a note and a key.
        /// </summary>
        /// <param name="note">The <see cref="Note"/>.</param>
        /// <param name="key">The <see cref="Key"/>.</param>
        /// <returns>The <see cref="Interval"/>.</returns>
        public static Interval operator -(Note note, Key key)
        {
            var keyRoot = key.Root;
            var keyNote = key.Notes[note.DiatonicNote];
            var diatonicDistance = keyNote.DiatonicNote.DistanceFrom(keyRoot.DiatonicNote);
            diatonicDistance = (diatonicDistance + 7) % 7;
            var diatonicInterval = (DiatonicInterval) diatonicDistance + 1;
            var accidental = note.Accidental - keyNote.Accidental;
            var result = new Interval(diatonicInterval, accidental);

            return result;
        }

        #region Nested Types

        public abstract class KeysBase : IReadOnlyCollection<Key>
        {
            private readonly ILookup<Semitone, Key> _byDistanceFromC;

            protected KeysBase(IReadOnlyCollection<Key> values)
            {
                Values = values. OrderBy(key =>key.SignedAccidentalCount).ToList().AsReadOnly();
                BySignedAccidentalCounIndexer = new Indexer<int, Key>(values.ToDictionary(key => key.SignedAccidentalCount));
                _byDistanceFromC = values.ToLookup(key => key.Root.DistanceFromC);
            }

            public IReadOnlyList<Key> Values { get; }
            public IIndexer<int, Key> BySignedAccidentalCounIndexer { get; }

            public IEnumerator<Key> GetEnumerator()
            {
                return Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)Values).GetEnumerator();
            }

            public int Count => Values.Count;
            public Key this[int signedAccidentalCount]
            {
                get
                {
                    if (signedAccidentalCount < -7 || signedAccidentalCount > 7)
                    {
                        throw new ArgumentOutOfRangeException($"Parameter '{nameof(signedAccidentalCount)}' must be between -7 and 7", nameof(signedAccidentalCount));
                    }

                    return BySignedAccidentalCounIndexer[signedAccidentalCount];
                }
            }

            public IReadOnlyList<Key> this[Note keyRoot]
            {
                get
                {
                    var distanceFromC = keyRoot.DistanceFromC;
                    var result = _byDistanceFromC[distanceFromC].ToList().AsReadOnly();

                    return result;
                }
            }

            public override string ToString()
            {
                var result = string.Join(" ", Values);

                return result;
            }
        }

        public class MajorKeys : KeysBase
        {
            private static readonly IReadOnlyList<Key> _values = Enum.GetValues(typeof(MajorKey)).Cast<MajorKey>().Select(majorKey => new Key(majorKey)).ToList();
            private readonly Indexer<MajorKey, Key> _byEnumIndexer;

            public MajorKeys() : base(_values)
            {
                _byEnumIndexer = new Indexer<MajorKey, Key>(_values.ToDictionary(key => key.MajorKey));
            }

            public Key this[MajorKey majorKey] => _byEnumIndexer[majorKey];
        }

        public class MinorKeys : KeysBase
        {
            private static readonly IReadOnlyList<Key> _values = Enum.GetValues(typeof(MinorKey)).Cast<MinorKey>().Select(minorKey => new Key(minorKey)).ToList();
            private readonly Indexer<MinorKey, Key> _byEnumIndexer;

            public MinorKeys() 
                : base(_values)
            {
                _byEnumIndexer = new Indexer<MinorKey, Key>(_values.ToDictionary(key => key.MinorKey));
            }

            public Key this[MinorKey minorKey] => _byEnumIndexer[minorKey];
        }

        #endregion
    }
}
