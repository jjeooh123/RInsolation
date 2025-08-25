using Autodesk.Revit.DB;

namespace Insolation.ElementExtractor
{
    /// <summary>
    /// Factory for creating and configuring <see cref="IElementExtractor"/> instances.
    /// </summary>
    /// <remarks>
    /// TODO: Consider providing a concrete factory per strategy to simplify extensibility.
    /// TODO: Prefer constructor injection of <see cref="Document"/> instead of passing it to Create().
    /// </remarks>
    public interface IElementExtractoFactory
    {
        /// <summary>
        /// Creates an element extractor for the given strategy and document.
        /// </summary>
        /// <param name="strategy">The extraction strategy to use.</param>
        /// <param name="document">The active Revit document.</param>
        /// <returns>A configured <see cref="IElementExtractor"/>.</returns>
        IElementExtractor Create(ElementExtractionStrategy strategy, Document document);
    }
}
