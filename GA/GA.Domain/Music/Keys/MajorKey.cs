using System.ComponentModel;
using GA.Domain.Music.Notes;

namespace GA.Domain.Music.Keys
{
    /// <summary>
    /// Major key (-1 = one flat; 1 = one sharp, etc...)
    /// </summary>
    public enum MajorKey
    {
        Cb = -7,
        Gb = -6,
        Db = -5,
        Ab = -4,
        Eb = -3,
        Bb = -2,
        F = -1,
        C = 0,
        G = 1,
        D = 2,
        A = 3,
        E = 4,
        B = 5,

        [Description("F#")]
        FSharp = 6,

        [Description("C#")]
        CSharp = 7
    }
}