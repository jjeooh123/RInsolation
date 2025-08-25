using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.Rendering
{
    /// <summary>
    /// Service abstraction for drawing multiple insolation lines in a Revit document.
    /// </summary>
    public interface ILinesDrawingService
    {
        /// <summary>
        /// Draws insolation lines from a insolation point to multiple sun coordinates.
        /// </summary>
        /// <param name="insolationPoint">The origin point of the insolation lines.</param>
        /// <param name="sunCoordinates">The sun coordinates defining line directions.</param>
        /// <returns>A collection of <see cref="CreatedElementsInfo"/> for all created elements.</returns>
        public IEnumerable<CreatedElementsInfo> DrawLines(InsolationPoint insolationPoint, IEnumerable<SunCoordinate> sunCoordinates);
    }
}
