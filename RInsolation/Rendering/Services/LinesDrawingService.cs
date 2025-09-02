using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.Rendering
{
    /// <summary>
    /// Implementation of <see cref="ILinesDrawingService"/> that delegates line drawing 
    /// to an <see cref="IInsolationLineDrawer"/>.
    /// </summary>
    public class LinesDrawingService : ILinesDrawingService
    {
        private readonly IInsolationLineDrawer lineDrawer;

        /// <summary>
        /// Initializes a new instance of <see cref="LinesDrawingService"/>.
        /// </summary>
        /// <param name="linePrinter">The line drawer used to create individual lines.</param>
        public LinesDrawingService(IInsolationLineDrawer linePrinter) => lineDrawer = linePrinter;

        /// <inheritdoc/>
        public IEnumerable<CreatedElementsInfo> DrawLines(InsolationPoint insolationPoint, IEnumerable<SunCoordinate> sunCoordinates)
            => sunCoordinates.SelectMany(p => lineDrawer.DrawLine(insolationPoint, p));
    }
}
