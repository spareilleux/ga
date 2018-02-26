using System;
using GA.Domain.Music.Intervals;

namespace GA.Domain.Music.Notes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NoteAttribute : Attribute
    {
        public NoteAttribute(DiatonicNote diatonicNote, Accidental accidental)
        {
            DiatonicNote = diatonicNote;
            Accidental = accidental;
        }

        public DiatonicNote DiatonicNote { get; }
        public Accidental Accidental { get; }

    }
}
