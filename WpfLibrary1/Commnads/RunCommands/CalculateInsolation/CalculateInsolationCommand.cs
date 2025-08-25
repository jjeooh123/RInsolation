using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.Config;
using Insolation.Helpers;
using Insolation.InsolationCalculator;
using Insolation.MVVMs;
using Insolation.NS_SunPosition;
using Insolation.XYZExtractor;

namespace Insolation.Commnads
{
    /// <summary>
    /// Command that calculates insolation for all elements previously added to the insolation collection.
    /// </summary>
    /// <remarks>
    /// Execution flow:
    /// 1. Determines sun coordinates (strategy based on configuration).
    /// 2. Extracts insolation points from insolation collection, located in the shared context.
    /// 3. Runs insolation calculations against all sun coordinates.
    /// 4. Stores results in <see cref="IGlobalContextManager"/> for use by other commands.
    /// 
    /// Design notes:
    /// - Currently tightly coupled to <see cref="ServiceLocator"/> via provider.
    /// - TODO: Wrap this in a command wrapper that injects ExternalCommandData and dependencies into context manager
    ///         for configurate services outer the command.
    /// - TODO: If no insolation points are found, a message should be shown.
    /// - TODO: DRY: sun-coordinate calculation logic (code repeats in <see cref="DrawSunLinesCommand"/>).
    /// - TODO: SRS: sun-coordinate calculation logic in command class.
    /// </remarks>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class CalculateInsolationCommand : IExternalCommand
    {
        private Configuration config;
        private Document doc;
        private IGlobalContextManager globalContextManager;
        private ISunPositionService sunPositionService;
        private IExecutedInsolationPointService executedInsolationPointService;
        private IInsolationPointService insolationPointService;
        private View3D view3d;

        /// <summary>
        /// Provides factories and shared services needed by the command.
        /// </summary>
        public virtual InsolationCommandServiceProviderBase ServiceProvider { get; set; } = new DefaultInsolationCommandServiceProvider();

        /// <summary>
        /// Standard Revit IExternalCommand entry point.
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Init(commandData);
            if (!IsValid()) return Result.Failed;

            // Acquire sun positions (strategy depending on configuration)
            List<SunCoordinate> sunCoordinates;
            // TODO: Rewrite (Some strategies available in the configuration can only be used when transactions are open).
            if (config.CalcMethod == SunCoorCalcMethod.RevitSunAndShadowSettings) 
                using (Transaction t = new Transaction(doc, $"take sunpositions"))
                {
                    t.Start();
                    sunCoordinates = CalculateSunCoordinates();
                    t.RollBack();
                }
            else
                sunCoordinates = CalculateSunCoordinates();

            // Get elements for insolation
            var insolationPoints = insolationPointService.ExtractInsolationPoints(globalContextManager.GetResult<HashSet<ElementId>>(SharedContextKeys.ElementIds));
            if (insolationPoints.Count() == 0)
            {
                // TODO: Violation of SRS, implement message service
                TaskDialog.Show("TaskDialog", "В инсоляционном списке отсутствуют элементы, добавте элементы через комманду добавить");
                return Result.Failed;
            }

            // Perform insolation calculation
            var executedInsolationPoints = executedInsolationPointService.CalculateAllInsolation(insolationPoints, sunCoordinates).ToList();

            // Share results with other commands
            ShareResult(executedInsolationPoints);

            // Show result window, (better ux)
            // TODO: discard (srs), implement a service for working with windows.
            var window = System.Windows.Application.Current.Windows.OfType<ExecutedInsolationWindowView>().FirstOrDefault();
            if (window == null)
            {
                MainInsolationViewModel mainInsolationViewModel = new MainInsolationViewModel(globalContextManager.GetResult<List<ExecutedInsolationPoint>>(SharedContextKeys.ExecutedInsolation), config, commandData.Application.ActiveUIDocument);
                window = new ExecutedInsolationWindowView(mainInsolationViewModel);
                window.Show();
            }
            else
                window.Activate();

            return Result.Succeeded;
        }

        /// <summary>
        /// Get sun position data for calculations.
        /// </summary>
        /// <remarks>
        /// TODO: refactoring: DRY with <see cref="DrawSunLinesCommand"/> and SRS.
        /// </remarks>
        private List<SunCoordinate> CalculateSunCoordinates() 
        {
            List<SunCoordinate> sunCoordinates = sunPositionService.GetSunPositions().Select(p =>
            {
                var coordinate = CoordinateConverter.SpericalDToCartesian(p.Latitude, p.Longitude);
                return new SunCoordinate(p, new XYZ(coordinate.X, coordinate.Y, coordinate.Z));
            }).ToList();
            return sunCoordinates;
        }

        /// <summary>
        /// Stores executed insolation results in shared context.
        /// </summary>
        private void ShareResult(IEnumerable<ExecutedInsolationPoint> result) => globalContextManager.SubmitResult(SharedContextKeys.ExecutedInsolation, result);

        /// <summary>
        /// Initializes command dependencies via provider and global context.
        /// </summary>
        private void Init(ExternalCommandData commandData) 
        {
            doc = commandData.Application.ActiveUIDocument.Document;
            globalContextManager = ServiceProvider.GetIGlobalContextManager();
            config = globalContextManager.GetResult<Configuration>(SharedContextKeys.Configuration);
            sunPositionService = ServiceProvider.GetSunPositionServiceFactory().Create(config, doc);
            var insolationObjects = globalContextManager.GetResult<HashSet<ElementId>>(SharedContextKeys.ElementIds);
            view3d = View3DHelper.GetView3D(doc);
            executedInsolationPointService = ServiceProvider.GetExecutedInsolationPointServiceFactory().Create(doc, insolationObjects, View3DHelper.GetView3D(doc));
            insolationPointService = ServiceProvider.GetInsolationPointServiceFactory().Create(doc);
        }

        /// <summary>
        /// Basic precondition check to ensure services and 3D view are valid.
        /// </summary>
        private bool IsValid()
        {
            if (executedInsolationPointService == null) return false;
            if (view3d is not View3D) return false;
            return true;
        }
    }
}
