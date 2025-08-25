using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Determines the appropriate <see cref="XYZExtractionStrategy"/> for a given <see cref="Autodesk.Revit.DB.Element"/>.
    /// </summary>
    public interface IXYZExtractionStrategyResolver
    {
        /// <summary>
        /// Resolves the extraction strategy to be used for the specified <see cref="Autodesk.Revit.DB.Element"/>.
        /// </summary>
        /// <param name="elem">The element to analyze.</param>
        /// <returns>The resolved <see cref="XYZExtractionStrategy"/>.</returns>
        XYZExtractionStrategy GetXYZExtractionStrategy(Element elem);
    }
}
