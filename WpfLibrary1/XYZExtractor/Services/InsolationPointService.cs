using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Default implementation of <see cref="IInsolationPointService"/> that uses
    /// XYZ extraction strategies to compute insolation points from Revit elements.
    /// </summary>
    public class InsolationPointService : IInsolationPointService
    {
        private readonly Document document;
        private readonly IXYZExtractorProvider XYZExtractorProvider;
        private readonly IXYZExtractionStrategyResolver extractionStrategyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsolationPointService"/> class.
        /// </summary>
        /// <param name="document">The Revit <see cref="Autodesk.Revit.DB.Document"/>. Typically from <c>ExternalCommandData.Application.ActiveUIDocument.Document</c>.</param>
        /// <param name="XYZExtractorProvider">Provides extractors based on a given strategy.</param>
        /// <param name="extractionStrategyResolver">Resolves the appropriate extraction strategy for each element.</param>
        public InsolationPointService(Document document,
                                    IXYZExtractorProvider XYZExtractorProvider,
                                    IXYZExtractionStrategyResolver extractionStrategyResolver)
        {
            this.document = document;
            this.XYZExtractorProvider = XYZExtractorProvider;
            this.extractionStrategyResolver = extractionStrategyResolver;
        }

        /// <inheritdoc/>
        public IEnumerable<InsolationPoint> ExtractInsolationPoints(IEnumerable<ElementId> ids)
        {
            var result = new List<InsolationPoint>();

            foreach (var id in ids)
            {
                var element = document.GetElement(id);
                var extractor = XYZExtractorProvider.Get(extractionStrategyResolver.GetXYZExtractionStrategy(element));
                var XYZ = extractor.ExtractXYZ(element);
                result.Add(new InsolationPoint(XYZ, id));
            }
            return result;
        }
    }

}
