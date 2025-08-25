using Autodesk.Revit.DB;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Service that performs raycasting against the Revit model and retrieves
    /// <see cref="Autodesk.Revit.DB.ReferenceWithContext"/>
    /// </summary>
    /// <remarks>
    /// Currently returns a single <see cref="Autodesk.Revit.DB.ReferenceWithContext"/>.
    /// TODO: Consider make the method more generic and return a collection of objects.
    /// </remarks>
    public interface IReferenceIntersectionService
    {
        /// <summary>
        /// Gets the reference intersected by a ray starting from <paramref name="origin"/>
        /// in the given <paramref name="direction"/>.
        /// </summary>
        /// <param name="origin">The starting point of the ray.</param>
        /// <param name="direction">The ray direction.</param>
        /// <returns>The intersected reference with context (<see cref="Autodesk.Revit.DB.ReferenceWithContext"/>).</returns>
        ReferenceWithContext GetReference(XYZ origin, XYZ direction);
    }
}
