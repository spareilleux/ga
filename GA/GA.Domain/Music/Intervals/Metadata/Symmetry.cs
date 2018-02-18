using System.Collections.Generic;

namespace GA.Domain.Music.Intervals.Metadata
{
    /// <summary>
    /// Interval symmetry info.
    /// </summary>
    public class Symmetry
    {
        public Symmetry(IReadOnlyList<Semitone> relativesSemitones)
        {
            var count = relativesSemitones.Count;
            BlockSize = 0;
            BlockCount = 0;

            // Not symmetric if interval length is odd 
            if (count % 2 != 0) return;

            for (var blocksize = 1; blocksize < count / 2; blocksize++)
            {
                // Skip block size if needed
                if (count % blocksize != 0) continue;

                var blockcount = count / blocksize;

                // Compare each interval in all blocks
                for (var i = 0; i < blocksize; i++)
                {
                    IsSymmetric = true;
                    var interval = (int)relativesSemitones[i];
                    for (var j = 0; j < blockcount; j++)
                    {
                        if (relativesSemitones[i + j * blocksize] == interval) continue;

                        IsSymmetric = false;
                        break;
                    }

                    if (!IsSymmetric) continue;

                    BlockSize = blocksize;
                    BlockCount = blockcount;
                    break;
                }
            }
        }

        /// <summary>
        /// Gets a flag that indicates  whether the scale is symmetric.
        /// </summary>
        public bool IsSymmetric { get; }

        /// <summary>
        /// Gets the size of the symmetry block.
        /// </summary>
        public int BlockSize { get; }

        /// <summary>
        /// Gets the number of symmetry blocks.
        /// </summary>
        public int BlockCount { get; }

       public override string ToString()
       {
           if (IsSymmetric)
                return $"Symmetric: {BlockCount} blocks of {BlockSize} elements";
           return "Non-symmetric";
       }
    }
}
