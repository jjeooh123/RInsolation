using Autodesk.Revit.DB;

namespace Insolation.Rendering
{
    /// <summary>
    /// Factory implementation that creates <see cref="SketchPlane"/> instances 
    /// based on a normal vector and an origin point.
    /// </summary>
    /// <remarks>
    /// This factory must be used inside an open Revit transaction.  
    /// TODO: Implement an attribute that verifies a transaction is open before execution.
    /// </remarks>
    public class NormalBasedSketchPlaneFactory : ISketchPlaneFactory
    {
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance of <see cref="NormalBasedSketchPlaneFactory"/>.
        /// </summary>
        /// <param name="document">The active Revit document.</param>
        public NormalBasedSketchPlaneFactory(Document document) => this.document = document;

        /// <inheritdoc/>
        public SketchPlane Create(XYZ normal, XYZ origin, out CreatedElementsInfo createdElement)
        {
            Plane plane = Plane.CreateByNormalAndOrigin(normal, origin);
            SketchPlane sketchPlane = SketchPlane.Create(document, plane); // Создаем SketchPlane на этой плоскости
            createdElement = new CreatedElementsInfo(sketchPlane.Id, sketchPlane.UniqueId);
            return sketchPlane;
        }
    }
}
