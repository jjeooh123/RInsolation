using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Represents the insolation calculator module, 
    /// calculating insolation for a collection of <see cref="InsolationPoint"/>'s.
    /// </summary>
    public interface IExecutedInsolationPointService
    {
        /// <summary>
        /// Calculates insolation for all provided insolation points.
        /// </summary>
        /// <param name="insolationPoints">The points under analysis.</param>
        /// <param name="sunCoordinates">The sun positions to evaluate against.</param>
        /// <returns>A collection of <see cref="ExecutedInsolationPoint"/>'s.</returns>
        public IEnumerable<ExecutedInsolationPoint> CalculateAllInsolation(IEnumerable<InsolationPoint> insolationPoints,
                                                                           IEnumerable<SunCoordinate> sunCoordinates);
    }
}
