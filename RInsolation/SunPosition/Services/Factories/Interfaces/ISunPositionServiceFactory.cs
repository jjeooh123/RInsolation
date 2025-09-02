using Autodesk.Revit.DB;
using Insolation.Config;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Factory for creating <see cref="ISunPositionService"/> instances.
    /// </summary>
    /// <remarks>
    /// This interface is designed to be stored in a service locator.  
    /// </remarks>
    public interface ISunPositionServiceFactory
    {
        /// <summary>
        /// Creates a new <see cref="ISunPositionService"/> instance.
        /// </summary>
        /// <returns>A configured sun position service instance.</returns>
        ISunPositionService Create();
    }
}
