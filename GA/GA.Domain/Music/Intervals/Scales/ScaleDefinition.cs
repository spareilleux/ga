using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GA.Domain.Music.Intervals.Collections;
using GA.Domain.Music.Intervals.Metadata;

namespace GA.Domain.Music.Intervals.Scales
{
    /// <inheritdoc />
    /// <summary>
    /// Scale definition.
    /// </summary>
    public class ScaleDefinition : ISemitoneList
    {
        [Description("major")]
        public static ScaleDefinition Major = Parse("2 2 1 2 2 2 1");
        [Description("natural minor")]
        public static ScaleDefinition NaturalMinor = Parse("2 1 2 2 1 2 2");
        [Description("harmonic minor")]
        public static ScaleDefinition HarmonicMinor = Parse("2 1 2 2 1 3 1");
        [Description("melodic minor")]
        public static ScaleDefinition MelodicMinor = Parse("2 1 2 2 2 2 1");
        [Description("augmented")]
        public static ScaleDefinition Aug = Parse("3 1 3 1 3 1");
        [Description("diminished (Half,whole)")]
        public static ScaleDefinition DimHalfWhole = Parse("1 2 1 2 1 2 1 2");
        [Description("diminished (Whole,half)")]
        public static ScaleDefinition DimWholeHalf = Parse("2 1 2 1 2 1 2 1");
        [Description("whole tone")]
        public static ScaleDefinition WholeTone = Parse("2 2 2 2 2 2");
        [Description("pentatonic minor")]
        public static ScaleDefinition PentatonicMajor = Parse("2 2 3 2 3");
        [Description("pentatonic minor")]
        public static ScaleDefinition PentatonicMinor = Parse("2 2 3 2 3");

        private readonly SemitoneList _semitoneList;

        public ScaleDefinition(IEnumerable<Semitone> relativeSemitones)
        {
            Relative = new RelativeSemitoneList(relativeSemitones);
            _semitoneList = Relative.ToAbsolute();
            Symmetry = Relative.Symmetry;
        }

        /// <summary>
        /// Gets the scale definitions indexed by name.
        /// </summary>
        public static IDictionary<string, ScaleDefinition> ByName = GetScaleDefinitionByName();

        /// <summary>
        /// Gets the <see cref="RelativeSemitoneList"/>.
        /// </summary>
        public RelativeSemitoneList Relative { get; }
        
        /// <inheritdoc />
        public Symmetry Symmetry { get; }

        /// <inheritdoc />
        public RelativeSemitoneList ToRelative()
        {
            return Relative;
        }

        /// <inheritdoc />
        public IEnumerator<Semitone> GetEnumerator()
        {
            return _semitoneList.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public int Count => _semitoneList.Count;

        /// <inheritdoc />
        public Semitone this[int index] => _semitoneList[index];

        /// <summary>
        /// Converts the string representation of a semitones to its scale definition equivalent (Relative).
        /// </summary>
        /// <param name="distances">The <see cref="string"/> represention on the semitone relative distances (int separated by space character or ';' or ',')</param>
        /// <returns>The <see cref="ScaleDefinition"/>.</returns>
        /// <exception cref="System.FormatException">Throw if the format is incorrect,</exception>
        public static ScaleDefinition Parse(string distances)
        {
            var relativeSemitones = RelativeSemitoneList.Parse(distances);
            var result = new ScaleDefinition(relativeSemitones.Relative);

            return result;
        }

        public override string ToString()
        {
            var result = Relative.ToString();

            if (Symmetry.IsSymmetric)
            {
                result = $"{result} ({Symmetry}";
            }

            return result;
        }

        private static IDictionary<string, ScaleDefinition> GetScaleDefinitionByName()
        {
            var dict = new Dictionary<string, ScaleDefinition>(StringComparer.OrdinalIgnoreCase);
            var fields =
                typeof(ScaleDefinition).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(fi => fi.FieldType == typeof(ScaleDefinition))
                    .ToList();
            foreach (var field in fields)
            {
                var scaleName = field.GetCustomAttribute<DescriptionAttribute>().Description;
                var scaleDefinition = (ScaleDefinition)field.GetValue(null);
                dict[scaleName] = scaleDefinition;
            }

            var result = new ReadOnlyDictionary<string, ScaleDefinition>(dict);

            return result;
        }
    }
}