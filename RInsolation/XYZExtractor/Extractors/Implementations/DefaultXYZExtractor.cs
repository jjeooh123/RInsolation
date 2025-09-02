using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Extracts an XYZ point from an element based on its <see cref="BoundingBoxXYZ"/>..
    /// </summary>
    public class DefaultXYZExtractor : IXYZExtractor
    {
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultXYZExtractor"/>.
        /// </summary>
        public DefaultXYZExtractor(Document document) => this.document = document;

        /// <inheritdoc/>
        public XYZ ExtractXYZ(Element element)
        {
            // TODO: Refactor to pass the view in via the constructor instead of relying on ActiveView.
            var BoundingBox = element.get_BoundingBox(document.ActiveView);

            // Compute the center in X and Y, and slightly above the minimum Z
            // so the insolation point is guaranteed to be "inside" the element
            // and does not intersect or "fall" into a lower element.
            return new XYZ((BoundingBox.Min.X + BoundingBox.Max.X) / 2.0,
                           (BoundingBox.Min.Y + BoundingBox.Max.Y) / 2.0,
                            BoundingBox.Min.Z + 0.01 * (BoundingBox.Max.Z - BoundingBox.Min.Z));
        }
    }
}
