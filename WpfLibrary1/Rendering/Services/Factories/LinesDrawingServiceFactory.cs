using Autodesk.Revit.DB;
using Insolation.InsolationCalculator;

namespace Insolation.Rendering
{
    /// <summary>
    /// Factory for constructing configured <see cref="ILinesDrawingService"/> instances.
    /// </summary>
    /// <remarks>
    /// Wires up all dependencies required to construct a <see cref="LinesDrawingService"/>.
    /// </remarks>
    public class LinesDrawingServiceFactory : ILinesDrawingServiceFactory
    {
        /// <inheritdoc/>
        public ILinesDrawingService Create(Document doc, ElementId insolationPointId, View3D view3D)
        {
            IElementFilterFactory execlusionFilterFactory = new ExeclusionFilterFactory(doc, new[] { insolationPointId });
            ReferenceIntersector referenceIntersector = new ReferenceIntersector(execlusionFilterFactory.CreateFilter(), FindReferenceTarget.Face, view3D);
            IReferenceIntersectionService referencePicker = new ReferenceIntersectionService(referenceIntersector);

            ISketchPlaneFactory sketchPlaneFactory = new NormalBasedSketchPlaneFactory(doc);

            IInsolationLineDrawer lineDrawer = new InsolationLineDrawer(doc, referencePicker, sketchPlaneFactory);
            return new LinesDrawingService(lineDrawer);
        }
    }
}
