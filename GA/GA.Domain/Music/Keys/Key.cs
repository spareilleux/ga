namespace GA.Domain.Music.Keys
{
    public class Key
    {
        public Key(int accidentals)
        {
            Accidentals = accidentals;
            MajorKey = (MajorKey) accidentals;
            MinorKey = (MinorKey)accidentals;
        }

        public Key(MajorKey majorKey) :
            this((int) majorKey)
        {
            Mode = KeyMode.Major;
        }

        public Key(MinorKey minorKey) :
            this((int)minorKey)
        {
            Mode = KeyMode.Minor;
        }

        /// <summary>
        /// Number of accidentals (Positive = sharps; negative = flats)
        /// </summary>
        public int Accidentals { get; }

        public KeyMode Mode { get; }

        public MajorKey MajorKey { get; }

        public MinorKey MinorKey { get; }
    }
}
