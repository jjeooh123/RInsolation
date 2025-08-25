using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Standart implemantation of <see cref="IXYZExtractorFactoryResolver"/>,
    /// where active Revit documnet injected via constructor.
    /// </summary>
    public class XYZExtractorFactoryResolver : IXYZExtractorFactoryResolver
    {
        private readonly Document document;

        /// <summary>
        /// Creates instance of <see cref="XYZExtractorFactoryResolver"/>.
        /// </summary>
        /// <param name="document">Revit active document.</param>
        public XYZExtractorFactoryResolver(Document document) => this.document = document;

        ///<inheritdoc/>
        public IXYZExtractorFactory Resolve(XYZExtractionStrategy ExtractionStrategy)
            => ExtractionStrategy switch
                {
                    XYZExtractionStrategy.WindowXYZExtractor => new WindowXYZExtractorFactory(document),
                    XYZExtractionStrategy.DefaultXYZExtractor => new DefaultXYZExtractorFactory(document),
                    _ => throw new NotSupportedException($"Unsupported XYZextraction strategy: {ExtractionStrategy}")
                };
    }
}
