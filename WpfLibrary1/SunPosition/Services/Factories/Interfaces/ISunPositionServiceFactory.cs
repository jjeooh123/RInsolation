using Autodesk.Revit.DB;
using Insolation.Config;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Factory for creating <see cref="ISunPositionService"/> instances using a given <see cref="Configuration"/> and Revit document.
    /// </summary>
    /// <remarks>
    /// This interface is designed to be stored in a service locator.  
    /// The actual <see cref="Configuration"/> and <see cref="Document"/> are provided later when <see cref="Create"/> is called.  
    /// In application initialization (e.g., Application.INIT), the configuration and document may not yet be available.
    /// </remarks>
    public interface ISunPositionServiceFactory
    {
        /// <summary>
        /// Creates a new <see cref="ISunPositionService"/> instance for the specified configuration and document.
        /// </summary>
        /// <param name="config">
        /// <see cref="Configuration"/> object — contains latitude, longitude, time zone, 
        /// day of calculation, and other global settings.
        /// </param>
        /// <param name="doc">The active Revit document.</param>
        /// <returns>A configured sun position service instance.</returns>
        /// <remarks>
        /// TODO: Investigate whether a parameterless Create() could be supported if initialization is deferred.
        /// </remarks>
        ISunPositionService Create(Configuration config, Document doc); // TODO Create(void)?
    }
}
