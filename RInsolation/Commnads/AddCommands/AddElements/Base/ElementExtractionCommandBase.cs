using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Insolation.ElementExtractor;
using Insolation.Helpers;

namespace Insolation.Commnads
{
    /// <summary>
    /// Base class for element extraction commands.
    /// Responsible for extracting elements from the active document and sharing results
    /// across commands through <see cref="IGlobalContextManager"/>.
    /// </summary>
    /// <remarks>
    /// Design notes:
    /// - Each concrete command defines its extraction <see cref="ElementExtractionStrategy"/>.
    /// - Dependencies are resolved via <see cref="ElementExtractionCommandServiceProviderBase"/> (service-locator style by defaut).
    /// </remarks>
    public abstract class ElementExtractionCommandBase : BaseCommand
    {
        private Document doc;
        private UIDocument uidoc;
        private IGlobalContextManager globalContextManager;
        private IElementExtractor elementExtractor;
        private Selection selection;

        /// <summary>
        /// Provides access to factories and shared services required by the command.
        /// Default implementation uses <see cref="DefaultElementExtractionCommandServiceProvider"/>.
        /// </summary>
        public virtual ElementExtractionCommandServiceProviderBase ServiceProvider { get; set; } 
            = new DefaultElementExtractionCommandServiceProvider();

        /// <summary>
        /// Strategy defining how elements should be extracted (e.g., by family, by selection).
        /// Must be implemented by derived classes.
        /// </summary>
        protected abstract ElementExtractionStrategy Strategy { get; }

        /// <summary>
        /// Standard Revit IExternalCommand entry point.
        /// </summary>
        protected override Result Logic(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Init(commandData);
            if (!IsValid()) return Result.Failed;
            ShareResult();

            // TODO: discard
            ShowMessage();

            return Result.Succeeded;
        }

        /// <summary>
        /// Reports the result of the command execution to the user.
        /// </summary>
        /// <remarks>
        /// TODO: Violation of SRS, implement a service for working with messages
        /// </remarks>
        protected abstract void ShowMessage();

        /// <summary>
        /// Writes the extracted element IDs into the shared context, allowing other commands to use results.
        /// </summary>
        private void ShareResult()
        {
            var preresult = globalContextManager.GetResult<HashSet<ElementId>>(SharedContextKeys.ElementIds);
            preresult.UnionWith(elementExtractor.ExtractElements(selection));
            globalContextManager.SubmitResult(SharedContextKeys.ElementIds, preresult);
        }

        /// <summary>
        /// Initializes services via the provider.
        /// </summary>
        private void Init(ExternalCommandData commandData)
        {
            uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;
            selection = SelectionHelper.GetSelection(uidoc);
            elementExtractor = ServiceProvider.GetElementExtractorFactoryResolver().Resolve(Strategy).Create();
            globalContextManager = ServiceProvider.GetIGlobalContextManager();
        }

        /// <summary>
        /// Basic validation before execution.
        /// Ensures a valid selection is available.
        /// </summary>
        private bool IsValid()
        {
            if (selection != null) return true;
            else return false;
        }
    }
}
