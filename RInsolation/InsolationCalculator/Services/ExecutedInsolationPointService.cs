using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Service for executing insolation calculations across multiple <see cref="InsolationPoint"/>s.
    /// </summary>
    /// <remarks>
    /// Delegates per-point calculations to <see cref="IInsolationCalculator"/>.
    /// </remarks>
    public class ExecutedInsolationPointService : IExecutedInsolationPointService
    {
        private readonly IInsolationCalculator insolationCalculator;

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutedInsolationPointService"/>.
        /// </summary>
        /// <param name="insolationCalculator">The calculator responsible for per-point insolation calculation.</param>
        public ExecutedInsolationPointService(IInsolationCalculator insolationCalculator) => this.insolationCalculator = insolationCalculator;

        /// <inheritdoc />
        public IEnumerable<ExecutedInsolationPoint> CalculateAllInsolation(IEnumerable<InsolationPoint> insolationPoints,
                                                                           IEnumerable<SunCoordinate> sunCoordinates)
            => insolationPoints.Select(p => insolationCalculator.CalculateInsolation(p, sunCoordinates));
    }
}
