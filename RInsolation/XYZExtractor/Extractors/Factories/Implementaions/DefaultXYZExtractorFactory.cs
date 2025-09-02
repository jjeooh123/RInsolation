using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Specific implementation of <see cref="IXYZExtractorFactory"/> that produces <see cref="DefaultXYZExtractor"/>.
    /// </summary>
    public class DefaultXYZExtractorFactory : IXYZExtractorFactory
    {
        private readonly Document document;

        /// <summary>
        /// Creates new instance of <see cref="DefaultXYZExtractorFactory"/>.
        /// </summary>
        /// <param name="document">Active Revit document</param>
        public DefaultXYZExtractorFactory(Document document) => this.document = document;

        /// <inheritdoc/>
        public IXYZExtractor Create() => new DefaultXYZExtractor(document);
    }
}
