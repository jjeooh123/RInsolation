using Autodesk.Revit.DB;

namespace Insolation.ElementExtractor
{
    /// <summary>
    /// Default implementation of <see cref="IElementExtractoFactory"/>.
    /// Creates <see cref="IElementExtractor"/>s based on the specified strategy.
    /// </summary>
    /// <remarks>
    /// TODO: Split into concrete factories and introduce a resolver
    /// </remarks>
    public class ElementExtractorFactory : IElementExtractoFactory
    {
        /// <inheritdoc />
        public IElementExtractor Create(ElementExtractionStrategy strategy, Document document)
            => strategy switch
                {
                    ElementExtractionStrategy.BySelection => new ElementExtractorBySelection(document),
                    ElementExtractionStrategy.ByFamilyName => new ElementExtractorByFamilyName(document),
                    _ => throw new NotSupportedException($"Unsupported element extraction strategy: {strategy}")
                };
    }
}
