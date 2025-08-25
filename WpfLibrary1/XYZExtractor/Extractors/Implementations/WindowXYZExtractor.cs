using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Extracts an XYZ point from an element based on its <see cref="LocationPoint"/>.
    /// </summary>
    public class WindowXYZExtractor : IXYZExtractor
    {
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance of <see cref="WindowXYZExtractor"/>.
        /// </summary>
        public WindowXYZExtractor(Document document) => this.document = document;

        /// <inheritdoc/>
        public XYZ ExtractXYZ(Element element)
        {
            // TODO: Refactor to pass the view in via the constructor instead of relying on ActiveView.
            var BoundingBox = element.get_BoundingBox(document.ActiveView);

            // Ensure the element has a valid location point.
            // TODO: Throw a more specific exception type instead of a generic Exception.
            if (element.Location is not Autodesk.Revit.DB.LocationPoint) throw new Exception();

            var LocationPoint = element.Location as LocationPoint;

            // Compute the center in X and Y, and slightly above the minimum Z
            // so the insolation point is guaranteed to be "inside" the element
            // and does not intersect or "fall" into a lower element.
            return new XYZ(LocationPoint.Point.X,
                           LocationPoint.Point.Y,
                           BoundingBox.Min.Z + 0.01 * ((BoundingBox.Max.Z - BoundingBox.Min.Z)));
        }
    }
}
