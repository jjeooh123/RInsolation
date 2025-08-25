using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace Insolation.Helpers
{
    /// <summary>
    /// Provides helper methods for working with Revit <see cref="Autodesk.Revit.UI.Selection.Selection"/>.
    /// </summary>
    /// <remarks>
    /// Ensures a non-empty selection is returned, prompting the user if necessary.
    /// TODO: Fix issue where executing any transaction resets the selection.
    /// </remarks>
    public static class SelectionHelper
    {
        /// <summary>
        /// Retrieves the current selection from the given <see cref="Autodesk.Revit.UI.UIDocument"/>.
        /// If the selection is empty, uses PickObject method.
        /// </summary>
        /// <param name="uidoc">The active Revit UI document.</param>
        /// <returns>
        /// The <see cref="Autodesk.Revit.UI.Selection.Selection"/> if available or created by user input; otherwise <c>null</c>.
        /// </returns>
        public static Selection GetSelection(UIDocument uidoc)
        {
            var selection = uidoc.Selection.GetElementIds();
            if (selection.Count == 0)
                try
                {
                    // if method called without selected elements, use PickObject,
                    // that promt user to pick element (also change cursor icon to "+" on pickable elements)
                    var pickedRefs = uidoc.Selection.PickObject(ObjectType.Element, "Выберите элемент");
                    if (pickedRefs != null)
                    {
                        uidoc.Selection.SetReferences(new[] { pickedRefs });
                        return uidoc.Selection;
                    }
                    else return null;
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException) 
                {
                    // User canceled selection -> return null.
                    return null; 
                }

            // TODO: consider returng collection of ElementId's instead of selection
            uidoc.Selection.SetElementIds(selection);
            return uidoc.Selection;
        }
    }
}
