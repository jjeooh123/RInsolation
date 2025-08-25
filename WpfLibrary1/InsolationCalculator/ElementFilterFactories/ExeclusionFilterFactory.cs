using Autodesk.Revit.DB;

namespace Insolation.InsolationCalculator
{
    // filter for ReferenceIntersector, ethat excludes elements associated with InsolationPoint and hosted elements
    public class ExeclusionFilterFactory : IElementFilterFactory
    {
        private readonly IEnumerable<ElementId> insolationPointsIds;
        private readonly Document document;

        public ExeclusionFilterFactory(Document document, IEnumerable<ElementId> insolationPointsIds)
        {
            this.insolationPointsIds = insolationPointsIds;
            this.document = document;
        }

        public ElementFilter CreateFilter()
        {
            // execlude insolationPointsIds elements (so that the insolation rays do not hit the insolation object itself)
            List<ElementId> filtredElements = new List<ElementId>(insolationPointsIds); // TODO ref?

            // exclude hosted elements so that insolation rays do not cut into fragments of the element (imposts, system panels, ...)
            var collector = new FilteredElementCollector(document).WhereElementIsNotElementType();
            var elements = collector.Where(e => e is FamilyInstance fi && fi.Host is not null && insolationPointsIds.Contains(fi.Host.Id));

            if (elements.Any()) filtredElements.AddRange(elements.Select(e => e.Id));
            ExclusionFilter filter = new ExclusionFilter(filtredElements);
            return filter;
        }
    }
}
