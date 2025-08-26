using Autodesk.Revit.DB;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Factory for assembling and configuring <see cref="IExecutedInsolationPointService"/> instances.
    /// </summary>
    public interface IExecutedInsolationPointServiceFactory
    {
        /// <summary>
        /// Creates of <see cref="IExecutedInsolationPointService"/> instance.
        /// </summary>
        /// <returns>A configurated <see cref="IExecutedInsolationPointService"/></returns>
        IExecutedInsolationPointService Create();
    }
}
