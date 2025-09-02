using Autodesk.Revit.UI;
using Insolation.ElementExtractor;

namespace Insolation.Commnads
{
    /// <summary>
    /// Extraction command that collects elements by family name.
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class ExtractElementsByFamilyNameCommand : ElementExtractionCommandBase
    {
        protected override ElementExtractionStrategy Strategy => ElementExtractionStrategy.ByFamilyName;

        /// <inheritdoc/>
        protected override void ShowMessage() => TaskDialog.Show("TaskDialog", "семейство добавленно");
    }
}
