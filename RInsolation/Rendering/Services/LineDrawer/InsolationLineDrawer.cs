using Autodesk.Revit.DB;
using Insolation.InsolationCalculator;
using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.Rendering
{
    /// <summary>
    /// Implementation of <see cref="IInsolationLineDrawer"/> that draw insolation lines.
    /// </summary>
    /// <remarks>
    /// This service must be used inside an open Revit transaction.  
    /// TODO: Implement an attribute that verifies a transaction is open before execution.
    /// </remarks>
    public class InsolationLineDrawer : IInsolationLineDrawer
    {
        private readonly Document document;
        private readonly IReferenceIntersectionService referencePicker;
        private readonly ISketchPlaneFactory sketchPlaneFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="InsolationLineDrawer"/>.
        /// </summary>
        /// <param name="document">The active Revit document.</param>
        /// <param name="referencePicker">Service used to resolve intersections in 3D space.</param>
        /// <param name="sketchPlaneFactory">Factory for creating sketch planes.</param>
        public InsolationLineDrawer (Document document, IReferenceIntersectionService referencePicker, ISketchPlaneFactory sketchPlaneFactory)
        {
            this.document = document;
            this.referencePicker = referencePicker;
            this.sketchPlaneFactory = sketchPlaneFactory;
        }

        /// <inheritdoc/>
        public IEnumerable<CreatedElementsInfo> DrawLine(InsolationPoint insolationPoint, SunCoordinate sunCoordinate)
        {
            CreatedElementsInfo sketchPlaneInfo;

            // Create a sketch plane oriented by the cross product of the insolation point and sun vector
            var sketchPlane = sketchPlaneFactory.Create(insolationPoint.Postition.CrossProduct(sunCoordinate.Position), 
                                                                   sunCoordinate.Position, 
                                                                   out sketchPlaneInfo);

            // Try to find intersection; otherwise fall back to a distant endpoint
            var reference = referencePicker.GetReference(insolationPoint.Postition, sunCoordinate.Position);
            XYZ endpoint = reference == null ? insolationPoint.Postition + sunCoordinate.Position.Multiply(1200) : reference.GetReference().GlobalPoint;

            // Create the line in the document
            Line verticalLine = Line.CreateBound(insolationPoint.Postition, endpoint);
            var createdModelCurve = document.Create.NewModelCurve(verticalLine, sketchPlane);

            return new[]
            {
                sketchPlaneInfo,
                new CreatedElementsInfo(createdModelCurve.Id, createdModelCurve.UniqueId)
            };
        }
    }
}
