using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Specific implementation of <see cref="IXYZExtractorFactory"/> that produces <see cref="WindowXYZExtractorFactory"/>.
    /// </summary>
    public class WindowXYZExtractorFactory : IXYZExtractorFactory
    {
        private readonly Document document;

        /// <summary>
        /// Creates new instance of <see cref="WindowXYZExtractorFactory"/>.
        /// </summary>
        /// <param name="document">Active Revit document</param>
        public WindowXYZExtractorFactory(Document document) => this.document = document;

        /// <inheritdoc/>
        public IXYZExtractor Create() => new WindowXYZExtractor(document);
    }
}
