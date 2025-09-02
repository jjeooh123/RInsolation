using Autodesk.Revit.DB;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// An implementation of <see cref="IReferenceIntersectionService"/>
    /// using Revit's <see cref="Autodesk.Revit.DB.ReferenceIntersector"/> to detect geometry intersections.
    /// </summary>
    public class ReferenceIntersectionService : IReferenceIntersectionService
    {
        private readonly ReferenceIntersector referenceIntersector;

        /// <summary>
        /// Initializes a new instance of <see cref="ReferenceIntersectionService"/>.
        /// </summary>
        /// <param name="referenceIntersector">The Revit <see cref="Autodesk.Revit.DB.ReferenceIntersector"/> instance.</param>
        public ReferenceIntersectionService(ReferenceIntersector referenceIntersector) => this.referenceIntersector = referenceIntersector;

        /// <inheritdoc />
        public ReferenceWithContext GetReference(XYZ origin, XYZ direction) => referenceIntersector.FindNearest(origin, direction);
    }
}
