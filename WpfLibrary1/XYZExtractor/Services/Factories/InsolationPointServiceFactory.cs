using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Creates fully initialized instances of <see cref="IInsolationPointService"/>
    /// by assembling the necessary dependencies:
    /// - <see cref="IXYZExtractorFactory"/> for extractor creation
    /// - <see cref="IXYZExtractorProvider"/> for caching/reuse
    /// - <see cref="IXYZExtractionStrategyResolver"/> for strategy selection
    /// </summary>
    public class InsolationPointServiceFactory : IInsolationPointServiceFactory
    {
        /// <inheritdoc/>
        public IInsolationPointService Create(Document document)
        {
            IXYZExtractorFactoryResolver XYZExtractorFactoryResolver = new XYZExtractorFactoryResolver(document);
            IXYZExtractorProvider XYZExtractorProvider = new XYZExtractorCache(XYZExtractorFactoryResolver);
            IXYZExtractionStrategyResolver extractionStrategyResolver = new XYZExtractionStrategyResolver();
            
            return new InsolationPointService(document, XYZExtractorProvider, extractionStrategyResolver);
        }
    }

}
