using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.Config;
using Insolation.Helpers;
using Insolation.InsolationCalculator;
using Insolation.MVVMs;

namespace Insolation.Commnads
{
    /// <summary>
    /// Command that focuses on the currently selected element in the "Executed Insolation" results WPF window.
    /// </summary>
    /// <remarks>
    /// Execution flow:
    /// 1. Gets current Revit selection.
    /// 2. If result window exists:
    ///     - Activates it and instructs ViewModel to focus on the selected element.
    /// 3. Otherwise:
    ///     - Creates a new result window with <see cref="MainInsolationViewModel"/>.
    ///     - Displays it and highlights the selected element.
    ///     
    /// TODO: Window manager.
    /// </remarks>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class FocusOnElementInResultCommand : BaseCommand
    {
        /// <summary>
        /// Provides factories and shared services needed by the command.
        /// </summary>
        public virtual FocusOnElementCommandServiceProviderBase ServiceProvider { get; set; } = new DefaultFocusOnElementCommandServiceProvider();

        private IGlobalContextManager globalContextManager;
        private Configuration config;
        private UIDocument uidoc;

        /// <summary>
        /// Standard Revit IExternalCommand entry point.
        /// </summary>
        protected override Result Logic(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Init(commandData);
            var selection = SelectionHelper.GetSelection(uidoc);
            if (selection == null) return Result.Failed;

            // try get existing  result window
            var window = System.Windows.Application.Current.Windows.OfType<ExecutedInsolationWindowView>().FirstOrDefault();
            if (window == null)
            {
                // if window not exist -> crete and focus on element
                MainInsolationViewModel mainInsolationViewModel = new MainInsolationViewModel(globalContextManager.GetResult<List<ExecutedInsolationPoint>>(SharedContextKeys.ExecutedInsolation), config, uidoc);
                window = new ExecutedInsolationWindowView(mainInsolationViewModel);
                window.Show();
                mainInsolationViewModel.SelectRowByElementId(selection.GetElementIds().FirstOrDefault());
            }
            else {
                window.Activate();
                (window.DataContext as MainInsolationViewModel).SelectRowByElementId(selection.GetElementIds().FirstOrDefault());
            }
            return Result.Succeeded;
        }

        /// <summary>
        /// Initializes command dependencies via provider and global context.
        /// </summary>
        private void Init(ExternalCommandData commandData) 
        {
            globalContextManager = ServiceProvider.GetIGlobalContextManager();
            config = globalContextManager.GetResult<Configuration>(SharedContextKeys.Configuration);
            uidoc = commandData.Application.ActiveUIDocument;
        }
    }
}
