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
            string modeName) 
            : base(relativeSemitones)
        {
            ParentScale = parentScale;
            ModeName = modeName;
        }

        /// <summary>
        /// Gets the <see cref="ModalScaleDefinition"/>
        /// </summary>
        public ModalScaleDefinition ParentScale { get; }

        public string ModeName { get; }

        public override string ToString()
        {
            return $"{base.ToString()} - {ModeName}}}";
        }
    }
}
