using Autodesk.Revit.DB;
using System.Diagnostics.CodeAnalysis;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Represents a point in 3D space associated with a specific Revit element
    /// that participates in insolation calculations.
    /// </summary>
    public readonly struct InsolationPoint
    {
        /// <summary>
        /// The spatial position of the point.
        /// </summary>
        public readonly XYZ Postition;

        /// <summary>
        /// The identifier of the element associated with this point.
        /// </summary>
        public readonly ElementId ElementId;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsolationPoint"/> struct.
        /// </summary>
        /// <param name="postition">The spatial position of the point.</param>
        /// <param name="elementId">The identifier of the associated element.</param>
        public InsolationPoint(XYZ postition, ElementId elementId)
        {
            Postition = postition;
            ElementId = elementId;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => this.ElementId.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals([NotNullWhen(true)] object? obj) =>
            obj is InsolationPoint ms
                && this.ElementId == ms.ElementId;
    }
}
