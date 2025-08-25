using Autodesk.Revit.DB;

namespace Insolation.Rendering
{
    /// <summary>
    /// Abstraction for creating <see cref="SketchPlane"/> instances used when creating model curves.
    /// </summary>
    /// <remarks>
    /// TODO: Extend this interface to support more general plane creation methods 
    /// (e.g., define by three points), and implement current methods 
    /// such as creation via normal as convenience wrappers.
    /// </remarks>
    public interface ISketchPlaneFactory
    {
        /// <summary>
        /// Creates a new <see cref="SketchPlane"/> with the given normal and origin.
        /// </summary>
        /// <param name="normal">The normal vector of the plane.</param>
        /// <param name="origin">The origin point of the plane.</param>
        /// <param name="createdElement">Metadata for the created sketch plane.</param>
        /// <returns>The created <see cref="SketchPlane"/>.</returns>
        SketchPlane Create(XYZ normal, XYZ origin, out CreatedElementsInfo createdElement);
    }
}
