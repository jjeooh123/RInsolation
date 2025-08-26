using Autodesk.Revit.DB;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Factory for assembling and configuring <see cref="IExecutedInsolationPointService"/> instances.
    /// </summary>
    public interface IExecutedInsolationPointServiceFactory
    {
        /// <summary>
        /// Creates a new <see cref="IExecutedInsolationPointService"/> instance.
        /// </summary>
        IExecutedInsolationPointService Create();
    }
}
