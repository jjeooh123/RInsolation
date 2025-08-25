using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Defines an insolation calculator for a single <see cref="InsolationPoint"/>.
    /// </summary>
    /// <remarks>
    /// Consumes sun coordinates and produces an <see cref="ExecutedInsolationPoint"/>
    /// </remarks>
    public interface IInsolationCalculator
    {
        /// <summary>
        /// Calculates the insolation intervals for a given <see cref="InsolationPoint"/>.
        /// </summary>
        /// <param name="insolationPoint">The target point.</param>
        /// <param name="sunCoordinates">The sun positions over time.</param>
        /// <returns>Executed insolation results for the point.</returns>
        public ExecutedInsolationPoint CalculateInsolation(InsolationPoint insolationPoint, IEnumerable<SunCoordinate> sunCoordinates);
    }
}
