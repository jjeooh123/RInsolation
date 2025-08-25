using Autodesk.Revit.DB;

namespace Insolation.Rendering
{
    /// <summary>
    /// Represents metadata about an element created in the Revit document.
    /// </summary>
    /// <remarks>
    /// This information can be used to track and later remove elements
    /// created during the drawing process.
    /// </remarks>
    public readonly struct CreatedElementsInfo 
    {
        /// <summary>
        /// Gets the <see cref="Autodesk.Revit.DB.ElementId"/> of the created element.
        /// </summary>
        public readonly ElementId ElementId;

        /// <summary>
        /// Gets the <see cref="Autodesk.Revit.DB.Element.UniqueId"/> of the created element.
        /// </summary>
        public readonly string UniqueId;

        /// <summary>
        /// Initializes a new instance of <see cref="CreatedElementsInfo"/>.
        /// </summary>
        /// <param name="elementId">The element's Revit identifier.</param>
        /// <param name="uniqueId">The element's unique identifier string.</param>
        public CreatedElementsInfo(ElementId elementId, string uniqueId) 
        {
            ElementId = elementId;
            UniqueId = uniqueId;
        }
    }
}
