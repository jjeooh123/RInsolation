using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace Insolation.ElementExtractor
{
    /// <summary>
    /// An element extractor that simply returns the currently selected elements.
    /// </summary>
    public class ElementExtractorBySelection : IElementExtractor
    {
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance of <see cref="ElementExtractorBySelection"/>.
        /// </summary>
        /// <param name="document">The active Revit document.</param>
        public ElementExtractorBySelection(Document document) => this.document = document;

        /// <inheritdoc />
        public IEnumerable<ElementId> ExtractElements(Selection selection) => selection.GetElementIds();
    }
}
