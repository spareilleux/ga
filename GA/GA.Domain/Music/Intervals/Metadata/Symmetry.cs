using System.Collections.Generic;
using System.Linq;
using GA.Domain.Music.Intervals.Collections;

namespace GA.Domain.Music.Intervals.Metadata
{
    /// <summary>
    /// Interval symmetry info.
    /// </summary>
    public class Symmetry
    {
        public Symmetry(IReadOnlyList<Semitone> relativesIntervals)
        {
            var count = relativesIntervals.Count;
            if (count % 2 != 0) return; // Ensure count divides by 2

            bool CheckIntervals(int candidateBlockSize)
            {
                if (count % candidateBlockSize != 0) return false; // Ensure count divides by blocksize
                var blockCount = count / candidateBlockSize;

                for (var i = 0; i < candidateBlockSize; i++)
                {
                    var interval = relativesIntervals[i];
                    for (var j = 1; j < blockCount; j++)
                    {
                        var index = i + j * candidateBlockSize;
                        if (relativesIntervals[index] != interval) return false;
                    }
                }

                return true;
            }

            var blockSize = Enumerable.Range(1, count / 2).FirstOrDefault(CheckIntervals);
            if (blockSize < 0) return;

            // Intervals are symmetric
            Block = new RelativeSemitoneList(relativesIntervals.Take(blockSize));
            BlockCount = count / blockSize;
        }

        /// <summary>
        /// Gets a flag that indicates whether the intervals are symmetric.
        /// </summary>
        public bool IsSymmetric => BlockCount > 0;

        /// <summary>
        /// Gets the <see cref="RelativeSemitoneList "/> symmetry block.
        /// </summary>
        public RelativeSemitoneList Block { get; }

        /// <summary>
        /// Gets the number of symmetry blocks.
        /// </summary>
        public int BlockCount { get; }

        public override string ToString()
        {
            if (IsSymmetric)
            {
                return $"{BlockCount} x {Block}";
            }

            return "Non-symmetric";
        }
    }
}