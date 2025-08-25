using Autodesk.Revit.DB;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Factory for assembling and configuring <see cref="IExecutedInsolationPointService"/> instances.
    /// </summary>
    /// <remarks>
    /// TODO: Consider parameterless <c>Create()</c> with dependencies injected via constructor.
    /// </remarks>
    public interface IExecutedInsolationPointServiceFactory
    {
        /// <summary>
        /// Creates a new <see cref="IExecutedInsolationPointService"/> instance.
        /// </summary>
        /// <param name="doc">The active Revit document.</param>
        /// <param name="insolationPointIds">Identifiers of insolation points to process.</param>
        /// <param name="view3D">The 3D view used for ray intersections 
        /// (<see cref="Autodesk.Revit.DB.ReferenceIntersector"/> constructor).</param>
        IExecutedInsolationPointService Create(Document doc, IEnumerable<ElementId> insolationPointIds, View3D view3D);
    }
}
