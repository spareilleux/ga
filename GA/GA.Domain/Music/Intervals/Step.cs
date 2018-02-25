namespace GA.Domain.Music.Intervals
{
    /// <inheritdoc cref="Semitone" />
    /// <summary>
    /// Step.
    /// </summary>
    public class Step : Semitone
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Ha;f tone.
        /// </summary>
        public static readonly Step H = new Step(1);

        /// <summary>
        /// Whole tone.
        /// </summary>
        public static readonly Step W = new Step(2);

        public Step(int distance)
            : base(distance)
        {
        }

        /// <summary>
        /// Try to convert a string into a step.
        /// </summary>
        /// <param name="s">the <see cref="string"/></param>
        /// <param name="step">
        /// The quality.
        /// </param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool TryParse(string s, out Step step)
        {
            s = s?.ToLower();
            switch (s)
            {
                case "h":
                    step = H;
                    return true;
                case "w":
                    step = W;
                    return true;
                default:
                    step = null;
                    return false;
            }
        }
    }
}