using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Defines a service that can extract a 3D coordinate (<see cref="XYZ"/>)
    /// from a given Revit <see cref="Autodesk.Revit.DB.Element"/>.
    /// </summary>
    public interface IXYZExtractor
    {
        /// <summary>
        /// Extracts an XYZ coordinate from the specified <see cref="Autodesk.Revit.DB.Element"/>.
        /// </summary>
        /// <param name="element">The element from which to extract the XYZ point.</param>
        /// <returns>The extracted XYZ coordinate.</returns>
        XYZ ExtractXYZ(Element element);
    }
}
