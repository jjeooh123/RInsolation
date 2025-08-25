using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Strategy interface for determining whether a given <see cref="InsolationPoint"/>
    /// is insolated under specific <see cref="SunCoordinate"/>.
    /// </summary>
    public interface IInsolationStrategy
    {
        /// <summary>
        /// Determines whether the point is insolated under the given sun position.
        /// </summary>
        /// <param name="insolationPoint">The target point.</param>
        /// <param name="sunCoordinate">The sun’s position (XYZ + time).</param>
        /// <returns><c>true</c> if insolated; otherwise, <c>false</c>.</returns>
        bool IsInsolated(InsolationPoint insolationPoint, SunCoordinate sunCoordinate);
    }
}
