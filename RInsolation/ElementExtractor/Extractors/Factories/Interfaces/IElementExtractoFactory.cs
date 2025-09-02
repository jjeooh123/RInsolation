using Autodesk.Revit.DB;

namespace Insolation.ElementExtractor
{
    /// <summary>
    /// Factory for creating and configuring <see cref="IElementExtractor"/> instances.
    /// </summary>
    public interface IElementExtractoFactory
    {
        /// <summary>
        /// Creates an element extractor for the given document
        /// determinated in constructor.
        /// </summary>
        /// <returns>A configured <see cref="IElementExtractor"/>.</returns>
        IElementExtractor Create();
    }
}
