using System;
using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Keys;
using GA.Domain.Music.Notes;

namespace GA.Domain.Extensions
{
    public static class NinorKeyExtensions
    {
        private static readonly Dictionary<MinorKey, Note> _minorKeyNotes;

        static NinorKeyExtensions()
        {
            _minorKeyNotes = new Dictionary<MinorKey, Note>();
            var minorKeys = Enum.GetValues(typeof(MinorKey)).Cast<MinorKey>();
            foreach (var minorKey in minorKeys)
            {
                var sMinorKey = minorKey.ToString();
                var root = Note.Parse(sMinorKey);
                _minorKeyNotes.Add(minorKey, root);
            }
        }

        public static Note GetRoot(this MinorKey minorKey)
        {
            var result = _minorKeyNotes[minorKey];

            return result;
        }
    }
}
