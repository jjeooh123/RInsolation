using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Service representing the XYZ extraction module that converts a list of <see cref="Autodesk.Revit.DB.ElementId"/> into a collection
    /// of <see cref="InsolationPoint"/> instances using the configured extraction strategy.
    /// </summary>
    /// <remarks>
    /// This service is configured via an <see cref="IInsolationPointServiceFactory"/> (or its implementation),
    /// which wires together the necessary dependencies.
    /// </remarks>
    public interface IInsolationPointService // TODO rename? IXYZExtractorSerice?
    {
        /// <summary>
        /// Extracts insolation points from collection of <see cref="Autodesk.Revit.DB.ElementId"/>.
        /// </summary>
        /// <param name="ids">The identifiers of the elements to process.</param>
        /// <returns>A collection of <see cref="InsolationPoint"/> objects.</returns>
        public IEnumerable<InsolationPoint> ExtractInsolationPoints(IEnumerable<ElementId> ids);
    }

}
