using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.Config;
using Insolation.InsolationCalculator;
using Insolation.MVVMs;

namespace Insolation.Commnads
{
    /// <summary>
    /// Command that displays the "Executed Insolation Results" WPF window.
    /// </summary>
    /// <remarks>
    /// Execution flow:
    /// 1. Checks if results window is already open.
    /// 2. If not -> creates <see cref="MainInsolationViewModel"/> and opens <see cref="ExecutedInsolationWindowView"/>.
    /// 3. Otherwise → activates existing window.
    /// 
    /// - TODO: Wrap this in a command wrapper that injects ExternalCommandData and dependencies into context manager
    ///         for configurate services outer the command.
    /// </remarks>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class ShowResultCommand : IExternalCommand
    {
        /// <summary>
        /// Provides factories and shared services needed by the command.
        /// </summary>
        public static ShowResultCommandServiceProviderBase ServiceProvider {  get; set; } = new DefaultShowResultCommandServiceProvider();

        private IGlobalContextManager globalContextManager;
        private Configuration config;
        private UIDocument uidoc;

        /// <summary>
        /// Standard Revit IExternalCommand entry point.
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Init(commandData);

            var window = System.Windows.Application.Current.Windows.OfType<ExecutedInsolationWindowView>().FirstOrDefault();
            if (window == null)
            {
                MainInsolationViewModel mainInsolationViewModel = new MainInsolationViewModel(globalContextManager.GetResult<List<ExecutedInsolationPoint>>(SharedContextKeys.ExecutedInsolation), config, uidoc);
                window = new ExecutedInsolationWindowView(mainInsolationViewModel);
                window.Show();
            }
            else
                window.Activate();
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
