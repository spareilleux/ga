using System;
using System.Collections.Generic;
using System.Linq;
using GA.Core.Extensions;
using GA.Domain.Music.Intervals;

namespace GA.Domain.Music.Scales
{
    /// <inheritdoc />
    /// <summary>
    /// Modal scale definition. (int mode index).
    /// </summary>
    public class ModalScaleDefinition : ScaleDefinition
    {
        public ModalScaleDefinition(
            IEnumerable<Semitone> relativeSemitones,
            TonalFamily tonalFamilty)
            : base(relativeSemitones, tonalFamilty.GetFieldDescription())
        {
            TonalFamily = tonalFamilty;
            Modes = Enumerable.Range(0, 7).Select(GetMode).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the <see cref="TonalFamily"/>.
        /// </summary>
        public TonalFamily TonalFamily { get; }

        public IReadOnlyList<ModeDefinition> Modes { get; protected set; }

        /// <summary>
        /// Gets the mode definition.
        /// </summary>
        /// <param name="modeIndex">The mode index (0-based).</param>
        /// <returns>The <see cref="ModeDefinition"/>.</returns>
        private ModeDefinition GetMode(int modeIndex)
        {
            var modeName = $"Mode #{modeIndex + 1} of {ScaleName}";
            var relativeSemitones = this.Rotate(modeIndex);
            var sum = (Semitone)this.Take(modeIndex).Sum(s => s);
            var result = new ModeDefinition(
                relativeSemitones,
                this,
                sum,
                modeName,
                modeIndex);

            return result;
        }

        public override string ToString()
        {
            return $"{base.ToString()} - {ScaleName} scale (modal)";
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Modal scale definition. (<see cref="TScaleMode"/>).
    /// </summary>
    public class ModalScaleDefinition<TScaleMode> : ModalScaleDefinition
        where TScaleMode : struct
    {
        public ModalScaleDefinition(
            IEnumerable<Semitone> relativeSemitones,
            TonalFamily tonalFamily)
            : base(relativeSemitones, tonalFamily)
        {
            var scaleModes = Enum.GetValues(typeof(TScaleMode)).Cast<TScaleMode>();
            Modes = scaleModes.Select(GetMode).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the mode definition.
        /// </summary>
        /// <param name="mode">The <see cref="TScaleMode"/>.</param>
        /// <returns>The <see cref="ModeDefinition"/>.</returns>
        public ModeDefinition this[TScaleMode mode] => GetMode(mode);

        /// <summary>
        /// Gets the mode definition.
        /// </summary>
        /// <param name="mode">The <see cref="TScaleMode"/>.</param>
        /// <returns>The <see cref="ModeDefinition"/>.</returns>
        public ModeDefinition GetMode(TScaleMode mode)
        {
            var scaleMode = (Enum)Enum.Parse(typeof(TScaleMode), mode.ToString());
            var modeIndex = (int)Convert.ChangeType(scaleMode, TypeCode.Int32) - 1;
            var modeName = $"{scaleMode.GetFieldDescription()} mode (Mode #{modeIndex + 1} of {ScaleName} scale)";
            var relativeSemitones = this.Rotate(modeIndex);
            var sum = (Semitone)this.Take(modeIndex).Sum(s => s);
            var result = new ModeDefinition(
                relativeSemitones,
                this,
                sum,
                modeName,
                modeIndex);

            return result;
        }
    }
}