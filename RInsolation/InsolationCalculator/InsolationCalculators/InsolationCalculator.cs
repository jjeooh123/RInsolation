using Insolation.InsolationCalculator;
using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Calculates insolation intervals for a single <see cref="InsolationPoint"/>
    /// using a provided <see cref="IInsolationStrategy"/>.
    /// </summary>
    /// <remarks>
    /// Tracks state transitions between insolated and non-insolated states,
    /// recording time intervals where exposure occurs.
    /// </remarks>
    public class InsolationCalculator : IInsolationCalculator
    {
        private readonly IInsolationStrategy insolationStrategy;

        /// <summary>
        /// Initializes a new instance of <see cref="InsolationCalculator"/>.
        /// </summary>
        /// <param name="insolationStrategy">The strategy used to determine insolation at a point.</param>
        public InsolationCalculator(IInsolationStrategy insolationStrategy) => this.insolationStrategy = insolationStrategy;

        /// <inheritdoc />
        public ExecutedInsolationPoint CalculateInsolation(InsolationPoint insolationPoint, IEnumerable<SunCoordinate> sunCoordinates)
        {
            List<DateTime> timeIntervals = new List<DateTime>();
            bool isInsolated = false;

            // Iterate through sun positions and detect state changes
            foreach (var sunCoord in sunCoordinates)
            {
                var currentState = insolationStrategy.IsInsolated(insolationPoint, sunCoord);
                if (currentState != isInsolated)
                {
                    timeIntervals.Add(sunCoord.SunPosition.Time);
                    isInsolated = currentState;
                }    
            }

            // Ensure closing interval is recorded if still insolated at the last coordinate.
            var last = sunCoordinates.LastOrDefault();
            if (insolationStrategy.IsInsolated(insolationPoint, last) == true) timeIntervals.Add(last.SunPosition.Time);
            
            return new ExecutedInsolationPoint(insolationPoint, timeIntervals);
        }
    }
}
