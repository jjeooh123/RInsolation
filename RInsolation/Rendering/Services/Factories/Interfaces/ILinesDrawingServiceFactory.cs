using Autodesk.Revit.DB;

namespace Insolation.Rendering
{
    /// <summary>
    /// Factory abstraction for creating <see cref="ILinesDrawingService"/> instances.
    /// </summary>
    public interface ILinesDrawingServiceFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="ILinesDrawingService"/>.
        /// </summary>
        /// <returns>A configured <see cref="ILinesDrawingService"/>.</returns>
        ILinesDrawingService Create();
    }
}
