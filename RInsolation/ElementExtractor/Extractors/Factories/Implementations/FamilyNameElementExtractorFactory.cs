using Autodesk.Revit.DB;

namespace Insolation.ElementExtractor
{
    /// <summary>
    /// Concrete implementation of <see cref="IElementExtractoFactory"/>.
    /// Creates <see cref="ElementExtractorByFamilyName"/>'ss 
    /// </summary>
    public class FamilyNameElementExtractorFactory : IElementExtractoFactory
    {
        Document document;

        /// <summary>
        /// Creates instance of <see cref="FamilyNameElementExtractorFactory"/>.
        /// </summary>
        /// <param name="document">Revit active document.</param>
        public FamilyNameElementExtractorFactory(Document document) => this.document = document;

        /// <inheritdoc />
        public IElementExtractor Create() => new ElementExtractorByFamilyName(document);
    }
}
