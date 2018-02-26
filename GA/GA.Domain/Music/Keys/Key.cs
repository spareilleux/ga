using System;
using GA.Domain.Extensions;
using GA.Domain.Music.Intervals;
using GA.Domain.Music.Notes;

namespace GA.Domain.Music.Keys
{
    public class Key
    {
        /// <inheritdoc />
        /// <summary>
        /// Creates a key instance (Major key).
        /// </summary>
        /// <param name="majorKey">The <see cref="P:GA.Domain.Music.Keys.Key.MajorKey" />.</param>
        public Key(MajorKey majorKey) :
            this((int)majorKey)
        {
            Mode = KeyMode.Major;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a key instance (Major key).
        /// </summary>
        /// <param name="minorKey">The <see cref="P:GA.Domain.Music.Keys.Key.MinorKey" />.</param>
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
        public AccidentalKind AccidentalKind { get;  }

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
    }
}
