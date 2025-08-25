using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Default implementation of <see cref="IXYZExtractionStrategyResolver"/> that selects
    /// a strategy based on the element's category.
    /// </summary>
    public class XYZExtractionStrategyResolver : IXYZExtractionStrategyResolver
    {
        /// <inheritdoc/>
        public XYZExtractionStrategy GetXYZExtractionStrategy(Element elem)
            => elem.Category?.BuiltInCategory == BuiltInCategory.OST_Windows
                ? XYZExtractionStrategy.WindowXYZExtractor
                : XYZExtractionStrategy.DefaultXYZExtractor;
    }
}
