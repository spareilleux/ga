using System;
using GA.Domain.Music.Intervals;
using GA.Domain.Music.Intervals.Scales;

namespace GA.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Guitar Alchemist");

            var s = new Semitone(10);
            var flat = Accidental.Flat;
            var sharp = Accidental.Sharp;

            var a = ScaleDefinition.ByName;
        }
    }
}
