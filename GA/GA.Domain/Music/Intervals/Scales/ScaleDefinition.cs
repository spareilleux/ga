using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GA.Domain.Music.Intervals.Collections;
using GA.Domain.Music.Intervals.Qualities;
using GA.Domain.Music.Intervals.Scales.Modes;
using JetBrains.Annotations;

namespace GA.Domain.Music.Intervals.Scales
{
    /// <inheritdoc />
    /// <summary>
    /// Scale definition.
    /// </summary>
    public class ScaleDefinition : RelativeSemitoneList
    {
        public static ModalScaleDefinition<MajorScaleMode> Major = Parse("2-2-1-2-2-2-1").AsModal<MajorScaleMode>(TonalFamily.Major);
        public static ModalScaleDefinition<NaturalMinorScaleMode> NaturalMinor = Parse("2-1-2-2-1-2-2").AsModal<NaturalMinorScaleMode>(TonalFamily.NaturalMinor);
        public static ModalScaleDefinition<HarmonicMinorScaleMode> HarmonicMinor = Parse("2-1-2-2-1-3-1").AsModal<HarmonicMinorScaleMode>(TonalFamily.HarmonicMinor);
        public static ModalScaleDefinition<MelodicMinorScaleMode> MelodicMinor = Parse("2-1-2-2-2-2-1").AsModal<MelodicMinorScaleMode>(TonalFamily.MelodicMinor);

        [Description("augmented")]
        public static ScaleDefinition Aug = "3-1-3-1-3-1";
        [Description("diminished (Half,whole)")]
        public static ScaleDefinition DimHalfWhole = "1-2-1-2-1-2-1-2";
        [Description("diminished (Whole,half)")]
        public static ScaleDefinition DimWholeHalf = "2-1-2-1-2-1-2-1";
        [Description("whole tone")]
        public static ScaleDefinition WholeTone = "2-2-2-2-2-2";
        [Description("pentatonic minor")]
        public static ScaleDefinition PentatonicMajor = "2-2-3-2-3";
        [Description("pentatonic minor")]
        public static ScaleDefinition PentatonicMinor = "2-2-3-2-3";

        private static readonly Semitone _m3 = Quality.m3;

        public ScaleDefinition(
            IEnumerable<Semitone> relativeSemitones,
            string name = null) 
            : base(relativeSemitones)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the scale name (Can be null).
        /// </summary>
        [CanBeNull]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the distances <see cref="string"/>.
        /// </summary>
        public string Distances => base.ToString();

        /// <summary>
        /// Gets a flag that indicates whether the scale is minor (Contains a minor 3rd).
        /// </summary>
        public bool IsMinor => Absolute.Contains(_m3);

        /// <summary>
        /// Gets the scale definitions indexed by name.
        /// </summary>
        public static IReadOnlyDictionary<string, ScaleDefinition> ByName = GetScaleDefinitionByName();

        /// <summary>
        /// Gets all scale definitions.
        /// </summary>
        public static IReadOnlyList<ScaleDefinition> All = ByName.Values.ToList().AsReadOnly();

        public ScaleDefinition AsModal(TonalFamily tonalFamily)
        {
            return new ModalScaleDefinition(this, tonalFamily);
        }

        public ModalScaleDefinition<TScaleMode> AsModal<TScaleMode>(TonalFamily tonalFamily)
            where TScaleMode : struct
        {
            return new ModalScaleDefinition<TScaleMode>(this, tonalFamily);
        }

        /// <summary>
        /// Converts the string representation of a semitones to its scale definition equivalent (Relative).
        /// </summary>
        /// <param name="distances">The <see cref="string"/> represention on the semitone relative distances (int separated by space character or ';' or ',')</param>
        /// <returns>The <see cref="ScaleDefinition"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public new static ScaleDefinition Parse(string distances)
        {
            var relativeSemitones = RelativeSemitoneList.Parse(distances);
            var result = new ScaleDefinition(relativeSemitones);

            return result;
        }

        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrEmpty(Name))
            {
                result = $"{result} - {Name} scale";
            }

            if (Symmetry.IsSymmetric)
            {
                result = $"{result} ({Symmetry}";
            }

            return result;
        }

        /// <summary>
        /// Converrts a distances string into a scale definition.
        /// </summary>
        /// <param name="distances">The distance <see cref="string" /></param>
        /// <returns>The <see cref="ScaleDefinition" /> objects</returns>
        public static implicit operator ScaleDefinition(string distances)
        {
            return Parse(distances);
        }

        /// <summary>
        /// Gets scale definitions, indexed by name.
        /// </summary>
        private static IReadOnlyDictionary<string, ScaleDefinition> GetScaleDefinitionByName()
        {
            var dict = new Dictionary<string, ScaleDefinition>(StringComparer.OrdinalIgnoreCase);
            var fields =
                typeof(ScaleDefinition).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(fi => typeof(ScaleDefinition).IsAssignableFrom(fi.FieldType))
                    .ToList();
            foreach (var field in fields)
            {                
                var scaleDefinition = (ScaleDefinition)field.GetValue(null);
                string scaleName;
                if (scaleDefinition is ModalScaleDefinition modalScaleDefinition)
                {
                    scaleName = modalScaleDefinition.ScaleName;
                }
                else
                {
                    scaleName = field.GetCustomAttribute<DescriptionAttribute>().Description;
                    scaleDefinition.Name = scaleName;
                }
                dict[scaleName] = scaleDefinition;
            }

            var result = new ReadOnlyDictionary<string, ScaleDefinition>(dict);

            return result;
        }
    }
}
