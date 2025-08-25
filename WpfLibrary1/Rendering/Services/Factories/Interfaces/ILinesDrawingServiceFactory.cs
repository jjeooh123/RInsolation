using Autodesk.Revit.DB;

namespace Insolation.Rendering
{
    /// <summary>
    /// Factory abstraction for creating <see cref="ILinesDrawingService"/> instances.
    /// Intended for use in client command initialization blocks, where the factory
    /// itself is typically created during the application's main Init method
    /// by default constructor.
    /// </summary>
    public interface ILinesDrawingServiceFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="ILinesDrawingService"/>.
        /// </summary>
        /// <param name="doc">The active Revit document.</param>
        /// <param name="insolationPointId">The identifier of the insolation point to exclude from intersections.</param>
        /// <param name="view3D">The 3D view used for reference intersection.</param>
        /// <returns>A configured <see cref="ILinesDrawingService"/>.</returns>
        /// <remarks>
        /// TODO: consider refactoring to a parameterless
        /// </remarks>
        ILinesDrawingService Create(Document doc, ElementId insolationPointId, View3D view3D);
    }
}
