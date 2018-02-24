using System.Collections.Generic;
using GA.Domain.Music.Intervals;

namespace GA.Domain.Music.Scales
{
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
    }
}
