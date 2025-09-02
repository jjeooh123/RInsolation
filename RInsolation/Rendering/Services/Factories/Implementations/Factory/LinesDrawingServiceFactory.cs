using Autodesk.Revit.DB;
using Insolation.InsolationCalculator;
using Insolation.NS_SunPosition;

namespace Insolation.Rendering
{
    /// <summary>
    /// Factory for constructing configured <see cref="ILinesDrawingService"/> instances.
    /// </summary>
    /// <remarks>
    /// Wires up all dependencies required to construct a <see cref="LinesDrawingService"/>.
    /// TODO: now use all insolation Point for creating <c>ExeclusionFilter</c> for <c>ReferenceIntersector</c>
    /// consider using only one point (or not using it at all) and inject this during the service call
    /// </remarks>
    public class LinesDrawingServiceFactory : ILinesDrawingServiceFactory
    {
        private Document doc;
        private IEnumerable<ElementId> insolationPointIds;
        private View3D view3D;

        /// <summary>
        /// Creates instance of <see cref="LinesDrawingServiceFactory"/>
        /// </summary>
        /// <param name="doc">Active Revit document.</param>
        /// <param name="insolationPointIds">Insolation Element collection for creating <c>ExeclusionFilter</c></param>
        /// <param name="view3D"><c>View3D</c> for <c>ReferenceIntersector</c></param>
        public LinesDrawingServiceFactory(Document doc,
                                          IEnumerable<ElementId> insolationPointIds,
                                          View3D view3D)
        {
            this.doc = doc;
            this.insolationPointIds = insolationPointIds;
            this.view3D = view3D;
        }

        /// <inheritdoc />
        /// <exception cref="NullReferenceException">Thrown when active Revit document is <c>null</c>, 
        /// insolation point's collection is empty or view3d is null</exception>
        public ILinesDrawingService Create()
        {
            // Validate input before constructing service dependencies.
            if (doc != null && insolationPointIds.Count() != 0 && view3D != null)
            {
                IElementFilterFactory execlusionFilterFactory = new ExeclusionFilterFactory(doc, insolationPointIds);
                ReferenceIntersector referenceIntersector = new ReferenceIntersector(execlusionFilterFactory.CreateFilter(), FindReferenceTarget.Face, view3D);
                IReferenceIntersectionService referencePicker = new ReferenceIntersectionService(referenceIntersector);

                ISketchPlaneFactory sketchPlaneFactory = new NormalBasedSketchPlaneFactory(doc);

                IInsolationLineDrawer lineDrawer = new InsolationLineDrawer(doc, referencePicker, sketchPlaneFactory);
                return new LinesDrawingService(lineDrawer);
            }
            else throw new NullReferenceException();
        }
    }
}
