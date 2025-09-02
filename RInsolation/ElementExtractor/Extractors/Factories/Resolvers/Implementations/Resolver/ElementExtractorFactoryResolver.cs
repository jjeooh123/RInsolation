using Autodesk.Revit.DB;

namespace Insolation.ElementExtractor
{

    /// <summary>
    /// Standart implemantation of <see cref="IElementExtractorFactoryResolver"/>,
    /// where active Revit documnet injected via constructor.
    /// </summary>
    public class ElementExtractorFactoryResolver : IElementExtractorFactoryResolver
    {
        private Document document;

        /// <summary>
        /// Creates instance of <see cref="ElementExtractorFactoryResolver"/>.
        /// </summary>
        /// <param name="document">Revit active document.</param>
        public ElementExtractorFactoryResolver(Document document) => this.document = document;

        /// <inheritdoc/>
        public IElementExtractoFactory Resolve(ElementExtractionStrategy strategy)
            => strategy switch
            {
                ElementExtractionStrategy.BySelection => new SelectionElementExtractorFactory(document),
                ElementExtractionStrategy.ByFamilyName => new FamilyNameElementExtractorFactory(document),
                _ => throw new NotSupportedException($"Unsupported element extraction strategy: {strategy}")
            };
    }
}
