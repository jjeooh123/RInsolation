using Autodesk.Revit.DB;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// factory for creating <see cref="IExecutedInsolationPointService"/> instances,
    /// assembling dependencies such as filters, intersection services, and calculators.
    /// </summary>
    /// <remarks>
    /// TODO: consider Create(void), moving dependencies into contructor,
    /// consider inject ReferenceIntersector, not creating it.
    /// </remarks>
    public class ExecutedInsolationPointServiceFactory : IExecutedInsolationPointServiceFactory
    {
        /// <inheritdoc />
        public IExecutedInsolationPointService Create(Document doc,
                                                      IEnumerable<ElementId> insolationPointIds,
                                                      View3D view3D)
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
            else return null;
        }
    }
}
