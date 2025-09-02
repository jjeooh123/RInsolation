using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// An <see cref="IInsolationStrategy"/> implementation that determines
    /// insolation based on ray intersections using a <see cref="Autodesk.Revit.DB.ReferenceIntersector"/>.
    /// </summary>
    /// <remarks>
    /// Currently delegates to <see cref="IReferenceIntersectionService"/> for intersection checks.
    /// TODO: Consider moving <see cref="IReferenceIntersectionService"/> implementation inside this class.
    /// </remarks>
    public class ReferenceBasedInsolationStrategy : IInsolationStrategy
    {
        private readonly IReferenceIntersectionService referencePicker;

        /// <summary>
        /// Initializes a new instance of <see cref="ReferenceBasedInsolationStrategy"/>.
        /// </summary>
        /// <param name="referencePicker">Service used to detect intersections with geometry.</param>
        public ReferenceBasedInsolationStrategy(IReferenceIntersectionService referencePicker) => this.referencePicker = referencePicker;

        /// <inheritdoc />
        public bool IsInsolated(InsolationPoint insolationPoint, SunCoordinate sunCoordinate) => referencePicker.GetReference(insolationPoint.Postition, sunCoordinate.Position) == null ? true : false;
    }
}
