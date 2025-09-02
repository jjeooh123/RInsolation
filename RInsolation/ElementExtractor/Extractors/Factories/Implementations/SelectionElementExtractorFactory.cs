using Autodesk.Revit.DB;

namespace Insolation.ElementExtractor
{
    /// <summary>
    /// Concrete implementation of <see cref="IElementExtractoFactory"/>.
    /// Creates <see cref="ElementExtractorBySelection"/>'ss 
    /// </summary>
    public class SelectionElementExtractorFactory : IElementExtractoFactory
    {
        Document document;

        /// <summary>
        /// Creates instance of <see cref="SelectionElementExtractorFactory"/>.
        /// </summary>
        /// <param name="document">Revit active document.</param>
        public SelectionElementExtractorFactory(Document document) => this.document = document;

        /// <inheritdoc />
        public IElementExtractor Create() => new ElementExtractorBySelection(document);
    }
}
