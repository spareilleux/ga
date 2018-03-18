using System.Collections.Generic;
using System.Linq;
using GA.Core.Extensions;
using GA.Domain.Music.Intervals.Collections;
using GA.Domain.Music.Intervals.Qualities;
using GA.Domain.Music.Keys;
using GA.Domain.Music.Notes;

namespace GA.Domain.Music.Scales
{
    using Intervals;

    /// <inheritdoc />
    /// <summary>
    /// Mode definition.
    /// </summary>
    public class ModeDefinition : ScaleDefinition
    {
        public ModeDefinition(            
            IEnumerable<Semitone> relativeSemitones,
            ModalScaleDefinition parentScale,
            Semitone distanceFromParentScale,
            string modeName,
            int modeIndex) 
            : base(relativeSemitones)
        {
            ParentScale = parentScale;
            DistanceFromParentScale = distanceFromParentScale;
            ModeName = modeName;
            ModeIndex = modeIndex;
        }

        /// <summary>
        /// Gets the <see cref="ModalScaleDefinition"/>
        /// </summary>
        public ModalScaleDefinition ParentScale { get; }

        /// <summary>
        /// Gets the <see cref="Semitone"/> distance from parent scale,
        /// </summary>
        public Semitone DistanceFromParentScale { get; }

        /// <summary>
        /// Gets the mode name.
        /// </summary>
        public string ModeName { get; }

        /// <summary>
        /// Gets the mode index (0-based).
        /// </summary>
        public int ModeIndex { get; }

        public override string ToString()
        {
            return $"{base.ToString()} - {ModeName}}}";
        }

        protected override IReadOnlyList<Interval> GetIntervals()
        {
            var modeNotes = GetModeNotes();
            var modeRoot = modeNotes.First();
            var intervals = new List<Interval>();

            var keys = Key.Major[modeRoot];
            var key = keys.First();
            foreach (var modeNote in modeNotes)
            {
                var interval = key.GetIntervalFromRoot(modeNote);
                intervals.Add(interval);
            }            
            var result = new IntervalsList(intervals);

            return result;
        }

        private List<Note> GetModeNotes()
        {
            var cmajNotes = Key.Major[MajorKey.C].Notes;
            var absoluteSemitones  = new AbsoluteSemitoneList(ParentScale.Absolute.Take(7));
            var parentScaleNotes = cmajNotes.GetNotes(absoluteSemitones);
            var result = parentScaleNotes.Rotate(ModeIndex).ToList();

            return result;
        }
    }
}
