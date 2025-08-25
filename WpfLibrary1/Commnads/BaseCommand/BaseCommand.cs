using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.ElementExtractor;

namespace Insolation.Commnads
{
    /// <summary>
    /// A base wrapper class that updates the global context 
    /// before actually executing client logic.
    /// </summary>
    /// <remarks>
    /// All commands must be wrapped in this shell, implement verification (for example, through reflection).
    /// </remarks>
    public abstract class BaseCommand : IExternalCommand
    {
        /// <summary>
        /// Provides access to factories and shared services required by the command.
        /// Default implementation uses <see cref="DefaultElementExtractionCommandServiceProvider"/>.
        /// </summary>
        public virtual BaseCommanderviceProviderBase ServiceProvider { get; set; }
            = new DefaultBaseCommanderviceProvider();

        /// <summary>
        /// Standard Revit IExternalCommand entry point.
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ServiceProvider.GetIGlobalContextManager().SubmitResult(SharedContextKeys.ExternalCommandData, commandData);
            return Logic(commandData, ref message, elements);
        }

        /// <summary>
        /// Client Logic
        /// </summary>
        protected abstract Result Logic(ExternalCommandData commandData, ref string message, ElementSet elements);
    }
}
