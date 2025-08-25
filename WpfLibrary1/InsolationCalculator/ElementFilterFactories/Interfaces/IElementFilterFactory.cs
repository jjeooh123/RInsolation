using Autodesk.Revit.DB;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Factory interface for creating <see cref="Autodesk.Revit.DB.ElementFilter"/> instances,
    /// typically used in <see cref="Autodesk.Revit.DB.ReferenceIntersector"/> 
    /// by <see cref="ReferenceBasedInsolationStrategy"/>.
    /// </summary>
    /// <remarks>
    /// Implementations allow customization of which elements are considered
    /// during intersection calculations.
    /// </remarks>
    public interface IElementFilterFactory
    {
        /// <summary>
        /// Creates and returns a configured <see cref="Autodesk.Revit.DB.ElementFilter"/>.
        /// </summary>
        ElementFilter CreateFilter();
    }
}
