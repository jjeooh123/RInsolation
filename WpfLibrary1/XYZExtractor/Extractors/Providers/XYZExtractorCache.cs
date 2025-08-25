namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Caches and provides <see cref="IXYZExtractor"/> instances to avoid redundant instantiation.
    /// </summary>
    public class XYZExtractorCache : IXYZExtractorProvider
    {
        private readonly IXYZExtractorFactoryResolver XYZExtractorFactoryResolver;

        // Holds cached extractor instances keyed by strategy.
        private Dictionary<XYZExtractionStrategy, IXYZExtractor> extractors = new Dictionary<XYZExtractionStrategy, IXYZExtractor>();

        /// <summary>
        /// Initializes a new instance of <see cref="XYZExtractorCache"/>
        /// </summary>
        public XYZExtractorCache(IXYZExtractorFactoryResolver XYZExtractorFactoryResolver)
            => this.XYZExtractorFactoryResolver = XYZExtractorFactoryResolver;

        /// <inheritdoc/>
        public IXYZExtractor Get(XYZExtractionStrategy ExtractionStrategy)
        {
            // If the extractor is already cached, return it. Otherwise, create and cache it.
            if (!extractors.TryGetValue(ExtractionStrategy, out var extractor))
            {
                extractor = XYZExtractorFactoryResolver.Resolve(ExtractionStrategy).Create();
                extractors[ExtractionStrategy] = extractor;
            }
            return extractor;
        }
    }
}
