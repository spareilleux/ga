using System.Collections.Generic;

namespace GA.Domain.Music.Intervals.Scales
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
            string modeName,
            int modeIndex) 
            : base(relativeSemitones)
        {
            ParentScale = parentScale;
            ModeName = modeName;
            ModeIndex = modeIndex;
        }

        /// <summary>
        /// Gets the <see cref="ModalScaleDefinition"/>
        /// </summary>
        public ModalScaleDefinition ParentScale { get; }

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
