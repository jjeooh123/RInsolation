using Insolation.XYZExtractor;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Represents the calculated insolation results for a given <see cref="InsolationPoint"/>.
    /// </summary>
    public readonly struct ExecutedInsolationPoint
    {
        /// <summary>
        /// The target point for which insolation is being calculated.
        /// </summary>
        public readonly InsolationPoint InsolationPoint;

        /// <summary>
        /// A sequence of time intervals (as <see cref="DateTime"/>) during which the point is insolated.
        /// </summary>
        public readonly IEnumerable<DateTime> InsolationIntervals;

        /// <summary>
        /// Creates a new instance of <see cref="ExecutedInsolationPoint"/>.
        /// </summary>
        /// <param name="insolationPoint">The point under analysis.</param>
        /// <param name="insolationIntervals">The time intervals of insolation.</param>
        public ExecutedInsolationPoint(InsolationPoint insolationPoint, IEnumerable<DateTime> insolationIntervals)
        {
            InsolationPoint = insolationPoint;
            InsolationIntervals = insolationIntervals;
        }
    }
}
