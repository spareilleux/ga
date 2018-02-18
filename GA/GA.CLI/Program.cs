using System;
using GA.Domain.Music;

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
        }
    }
}
