using Autodesk.Revit.DB;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// factory for creating <see cref="IExecutedInsolationPointService"/> instances,
    /// assembling dependencies such as filters, intersection services, and calculators.
    /// </summary>
    public class ExecutedInsolationPointServiceFactory : IExecutedInsolationPointServiceFactory
    {
        private Document doc;
        private IEnumerable<ElementId> insolationPointIds;
        private View3D view3D;

        public ExecutedInsolationPointServiceFactory(Document doc,
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
        public IExecutedInsolationPointService Create()
        {
            // Validate input before constructing service dependencies.
            if (doc != null && insolationPointIds.Count() != 0 && view3D != null)
            {
                IElementFilterFactory execlusionFilterFactory = new ExeclusionFilterFactory(doc, insolationPointIds);
                ReferenceIntersector referenceIntersector = new ReferenceIntersector(
                    execlusionFilterFactory.CreateFilter(),
                    FindReferenceTarget.Face,
                    view3D);
                IReferenceIntersectionService referencePicker = new ReferenceIntersectionService(referenceIntersector);
                ReferenceBasedInsolationStrategy insolationStrategy = new ReferenceBasedInsolationStrategy(referencePicker);
                IInsolationCalculator insolationCalculator = new InsolationCalculator(insolationStrategy);
                return new ExecutedInsolationPointService(insolationCalculator);
            }
            else throw new NullReferenceException();
        }
    }
}
