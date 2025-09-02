using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace Insolation.ElementExtractor
{
    /// <summary>
    /// Defines a contract for extracting elements from a Revit document
    /// based on the current <see cref="Autodesk.Revit.UI.Selection.Selection"/>.
    /// </summary>
    /// <remarks>
    /// TODO: Move <see cref="Document"/> and <see cref="Autodesk.Revit.UI.Selection.Selection"/> 
    /// dependency from method signature to constructor injection.
    /// </remarks>
    public interface IElementExtractor
    {
        /// <summary>
        /// Extracts element identifiers based on the provided selection.
        /// </summary>
        /// <param name="selection">The active Revit selection.</param>
        /// <returns>A collection of element identifiers matching the strategy, defined in implementaion.</returns>
        IEnumerable<ElementId> ExtractElements(Selection selection);
    }
}
