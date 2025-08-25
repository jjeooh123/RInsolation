using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Default implementation of <see cref="IXYZExtractorFactory"/> that produces
    /// specific extractor instances based on the provided <see cref="XYZExtractionStrategy"/>.
    /// </summary>
    /// <remarks>
    /// TODO: Consider implementing dedicated factory classes such as:
    /// - WindowXYZExtractorFactory : IXYZExtractorFactory
    /// - DefaultXYZExtractorFactory : IXYZExtractorFactory
    /// </remarks>
    public class XYZExtractorFactory : IXYZExtractorFactory
    {
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance of <see cref="XYZExtractorFactory"/> using the specified Revit document.
        /// </summary>
        public XYZExtractorFactory(Document document) => this.document = document;

        /// <inheritdoc/>
        public IXYZExtractor Create(XYZExtractionStrategy ExtractionStrategy)
            =>  ExtractionStrategy switch
                {
                    XYZExtractionStrategy.WindowXYZExtractor => new WindowXYZExtractor(document),
                    XYZExtractionStrategy.DefaultXYZExtractor => new DefaultXYZExtractor(document),
                    _ => throw new NotSupportedException($"Unsupported XYZextraction strategy: {ExtractionStrategy}")
                };
    }
}
