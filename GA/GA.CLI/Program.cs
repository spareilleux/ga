using System;
using GA.Domain.Music.Intervals;
using GA.Domain.Music.Keys;
using GA.Domain.Music.Notes;
using GA.Domain.Music.Scales;
using GA.Domain.Music.Scales.Modes;

namespace GA.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Guitar Alchemist");

            var keyOfF = Key.Major[MajorKey.F];
            var b = keyOfF.GetIntervalFromRoot(Note.B);

            var key = Key.Major[-6];
            var Fb = new Note(DiatonicNote.F, Accidental.Flat);
            var Eb = new Note(DiatonicNote.E, Accidental.Flat);
            var d = Fb - Eb;

            var s = new Semitone(10);
            var flat = Accidental.Flat;
            var sharp = Accidental.Sharp;

            var allScales = ScaleDefinition.All;
            var major = ScaleDefinition.Major;
            var lydian = major[MajorScaleMode.Lydian];
            var col = lydian.ColorTones;

            //var a = major.Absolute;
            //var b = lydian.Absolute;
            //var colorTones = b.Except(a).ToList();
            //var colorQualities = new QualityList(colorTones, AccidentalKind.Sharp);
        }
    }
}
