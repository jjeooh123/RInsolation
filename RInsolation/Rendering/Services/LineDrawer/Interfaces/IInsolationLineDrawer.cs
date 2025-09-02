using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.Rendering
{
    /// <summary>
    /// Service abstraction for drawing a single insolation line in the document.
    /// </summary>
    public interface IInsolationLineDrawer 
    {
        /// <summary>
        /// Draws a line from the insolation point to the sun coordinate.
        /// </summary>
        /// <param name="insolationPoint">The origin point of the line.</param>
        /// <param name="sunCoordinate">The target sun coordinate.</param>
        /// <returns>A collection of <see cref="CreatedElementsInfo"/> for the created line and supporting elements.</returns>
        public IEnumerable<CreatedElementsInfo> DrawLine(InsolationPoint insolationPoint, SunCoordinate sunCoordinate);
    }
}
