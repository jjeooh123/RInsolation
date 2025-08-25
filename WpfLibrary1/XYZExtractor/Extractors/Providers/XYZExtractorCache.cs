namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Caches and provides <see cref="IXYZExtractor"/> instances to avoid redundant instantiation.
    /// </summary>
    public class XYZExtractorCache : IXYZExtractorProvider
    {
        private readonly IXYZExtractorFactory XYZExtractorFactory;

        // Holds cached extractor instances keyed by strategy.
        private Dictionary<XYZExtractionStrategy, IXYZExtractor> extractors = new Dictionary<XYZExtractionStrategy, IXYZExtractor>();

        /// <summary>
        /// Initializes a new instance of <see cref="XYZExtractorCache"/>
        /// </summary>
        public XYZExtractorCache(IXYZExtractorFactory XYZExtractorFactory) => this.XYZExtractorFactory = XYZExtractorFactory;

        /// <inheritdoc/>
        public IXYZExtractor Get(XYZExtractionStrategy ExtractionStrategy)
        {
            // If the extractor is already cached, return it. Otherwise, create and cache it.
            if (!extractors.TryGetValue(ExtractionStrategy, out var extractor))
            {
                extractor = XYZExtractorFactory.Create(ExtractionStrategy);
                extractors[ExtractionStrategy] = extractor;
            }
            return extractor;
        }
    }
}
