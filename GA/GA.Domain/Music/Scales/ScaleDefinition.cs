using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GA.Domain.Music.Intervals;
using GA.Domain.Music.Intervals.Collections;
using GA.Domain.Music.Intervals.Qualities;
using GA.Domain.Music.Scales.Modes;
using JetBrains.Annotations;

namespace GA.Domain.Music.Scales
{
    /// <inheritdoc />
    /// <summary>
    /// Scale definition.
    /// </summary>
    public class ScaleDefinition : RelativeSemitoneList
    {
        // ReSharper disable InconsistentNaming        
        protected static ScaleDefinition _major = new ScaleDefinition("major", 2, 2, 1, 2, 2, 2, 1);  
        protected static ScaleDefinition _naturalMinor = new ScaleDefinition("natural minor", 2, 1, 2, 2, 1, 2, 2);
        // ReSharper restore InconsistentNaming

        public static ModalScaleDefinition<MajorScaleMode> Major = _major.AsModal<MajorScaleMode>(TonalFamily.Major);
        public static ModalScaleDefinition<NaturalMinorScaleMode> NaturalMinor = _naturalMinor.AsModal<NaturalMinorScaleMode>(TonalFamily.NaturalMinor);
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
        [Description("pentatonic major")]
        public static ScaleDefinition PentatonicMajor = "2-2-3-2-3";
        [Description("pentatonic minor")]
        public static ScaleDefinition PentatonicMinor = "3-2-2-3-2";

        // ReSharper disable PossibleMultipleEnumeration
        public ScaleDefinition(
            IEnumerable<Semitone> relativeSemitones,
            string scaleName = null)             
            : base(relativeSemitones)
        {
            var totalDistance = relativeSemitones.Aggregate(0, (i, semitone) => i + semitone.Distance);
            if (totalDistance != 12) throw new ArgumentException($"Invalid scale definition - the sum of '{nameof(relativeSemitones)}' is {totalDistance} and must be equal to 12", nameof(relativeSemitones));

            ScaleName = scaleName;
            IsMinor = Absolute.IsMinor;
        }
        // ReSharper restore PossibleMultipleEnumeration

        private ScaleDefinition(
            string scaleName,
            params int[] relativeSemitones) : base(relativeSemitones.Select(i => (Semitone)i))
        {
            ScaleName = scaleName;
        }

        /// <summary>
        /// Gets the scale scaleName (Can be null).
        /// </summary>
        [CanBeNull]
        public string ScaleName { get; private set; }

        /// <summary>
        /// Gets <see cref="string"/> that represents scale steps.
        /// </summary>
        public string Steps => base.ToString();

        public IReadOnlyList<Interval> Intervals => GetIntervals();

        /// <summary>
        /// Gets a flag that indicates whether the scale is minor (Contains a minor 3rd).
        /// </summary>
        public bool IsMinor { get; }

        /// <summary>
        /// Gets the scale definitions indexed by scaleName.
        /// </summary>
        public static IReadOnlyDictionary<string, ScaleDefinition> ByName = GetScaleDefinitionByName();

        /// <summary>
        /// Gets all scale definitions.
        /// </summary>
        public static IReadOnlyList<ScaleDefinition> All = ByName.Values.ToList().AsReadOnly();

        public ModalScaleDefinition<TScaleMode> AsModal<TScaleMode>(TonalFamily tonalFamily)
            where TScaleMode : struct
        {
            return new ModalScaleDefinition<TScaleMode>(this, tonalFamily);
        }

        /// <summary>
        /// Converts the string representation of a relative semitones list into its scale definition equivalent.
        /// </summary>
        /// <param paramname="s">The <see cref="string"/> represention of the semitone relative distances.</param>
        /// <returns>The <see cref="ScaleDefinition"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public static ScaleDefinition Parse(string s)
        {
            var relativeSemitones = RelativeSemitoneList.Parse(s);
            var result = new ScaleDefinition(relativeSemitones);

            return result;
        }

        public override string ToString()
        {
            var result = base.ToString();
            if (!string.IsNullOrEmpty(ScaleName))
            {
                result = $"{result} - {ScaleName} scale";
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
        /// Gets scale definitions, indexed by scaleName.
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
                    scaleDefinition.ScaleName = scaleName;
                }
                dict[scaleName] = scaleDefinition;
            }

            var result = new ReadOnlyDictionary<string, ScaleDefinition>(dict);

            return result;
        }

        protected virtual IReadOnlyList<Interval> GetIntervals()
        {
            var accidentalKind = AccidentalKind.Flat;
            var result = new IntervalsList(Absolute, accidentalKind);

            return result;
        }
    }
}
