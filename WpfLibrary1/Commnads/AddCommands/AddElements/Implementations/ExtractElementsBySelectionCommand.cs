using Autodesk.Revit.UI;
using Insolation.ElementExtractor;

namespace Insolation.Commnads
{
    /// <summary>
    /// Extraction command that collects elements based on explicit user selection.
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class ExtractElementsBySelectionCommand : ElementExtractionCommandBase
    {
        protected override ElementExtractionStrategy Strategy => ElementExtractionStrategy.BySelection;

        /// <inheritdoc/>
        protected override void ShowMessage() => TaskDialog.Show("TaskDialog", "элемент добавлен");
    }
}
