using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.Config;
using Insolation.MVVMs;

namespace Insolation.Commnads
{
    /// <summary>
    /// Command that opens the settings WPF window bound to <see cref="Configuration"/>.
    /// </summary>
    /// <remarks>
    /// Execution flow:
    /// 1. Retrieves the current configuration from <see cref="IGlobalContextManager"/>.
    /// 2. Wraps configuration in a <see cref="ConfigurationViewModel"/>.
    /// 3. Displays <see cref="ConfigurationView"/> as a modal dialog.
    /// 
    /// - TODO: Wrap this in a command wrapper that injects ExternalCommandData and dependencies into context manager
    ///         for configurate services outer the command.
    /// </remarks>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class ShowSettingwindowCommand : IExternalCommand
    {
        private Configuration config;

        /// <summary>
        /// Provides factories and shared services needed by the command.
        /// </summary>
        public virtual ShowSettingwindowCommandServiceProviderBase ServiceProvider { get; set; } = new DefaultShowSettingwindowCommandServiceProvider();

        /// <summary>
        /// Standard Revit IExternalCommand entry point.
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Init();
            ConfigurationViewModel viewModel = new ConfigurationViewModel(config);
            ConfigurationView window = new ConfigurationView(viewModel);
            window.ShowDialog();
            return Result.Succeeded;
        }

        /// <summary>
        /// Initializes command dependencies via provider and global context.
        /// </summary>
        private void Init()
        {
            config = ServiceProvider.GetIGlobalContextManager().GetResult<Configuration>(SharedContextKeys.Configuration);
        }
    }
}
