using System;
using System.Collections.Generic;
using System.Linq;
using GA.Core.Extensions;
using GA.Domain.Music.Keys;
using GA.Domain.Music.Notes;

namespace GA.Domain.Extensions
{
    public static class MajorKeyExtensions
    {
        private static readonly Dictionary<MajorKey, Note> _majorKeyNotes;

        static MajorKeyExtensions()
        {
            _majorKeyNotes = new Dictionary<MajorKey, Note>();
            var majorKeys = Enum.GetValues(typeof(MajorKey)).Cast<MajorKey>();
            foreach (var majorKey in majorKeys)
            {
                var sMajorKey = majorKey.GetFieldDescription();
                var root = Note.Parse(sMajorKey);
                _majorKeyNotes.Add(majorKey, root);
            }
        }

        public static Note GetRoot(this MajorKey majorKey)
        {
            var result = _majorKeyNotes[majorKey];

            return result;
        }
    }
}
