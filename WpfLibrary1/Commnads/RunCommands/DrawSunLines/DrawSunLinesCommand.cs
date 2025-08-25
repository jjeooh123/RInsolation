using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Insolation.Config;
using Insolation.Helpers;
using Insolation.NS_SunPosition;
using Insolation.Rendering;
using Insolation.XYZExtractor;

namespace Insolation.Commnads
{
    /// <summary>
    /// Command that draws sun/insolation lines for a selected element in the active document.
    /// </summary>
    /// <remarks>
    /// Execution flow:
    /// 1. Acquires sun positions (strategy based on configuration).
    /// 2. Extracts insolation points from insolation collection, located in the shared context.
    /// 3. Draw insolation line.
    /// 4. Stores created elements (<see cref="CreatedElementsInfo"/>) 
    ///    into <see cref="IGlobalContextManager"/> for later usage.
    ///    
    /// Design notes:
    /// - Currently tightly coupled to <see cref="ServiceLocator"/> via provider.
    /// - TODO: Wrap this in a command wrapper that injects ExternalCommandData and dependencies into context manager
    ///         for configurate services outer the command.
    /// - TODO: DRY: sun-coordinate calculation logic (code repeats in <see cref="CalculateInsolationCommand"/>).
    /// </remarks>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class DrawSunLinesCommand : IExternalCommand
    {
        private Configuration config;

        private Document doc;
        private UIDocument uidoc;
        private IGlobalContextManager globalContextManager;
        private ISunPositionService sunPositionService;
        private IInsolationPointService insolationPointService;
        private ILinesDrawingService lineDrawingService;

        private View3D view3d;
        private Selection selection;

        /// <summary>
        /// Provides factories and shared services needed by this command.
        /// </summary>
        public static DrawSunLinesCommandServiceProviderBase ServiceProvider { get; set; } = new DefaultDrawSunLinesCommandServiceProvider();

        /// <summary>
        /// Standard Revit IExternalCommand entry point.
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Init(commandData);
            if (!IsValid()) return Result.Failed;

            // Extract insolation point from the currently selected element.
            var insolationPoints = insolationPointService.ExtractInsolationPoints(selection.GetElementIds());

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

            // Initialize line drawing service
            lineDrawingService = ServiceProvider.GetLinesDrawingServiceFactory().Create(doc, insolationPoints.First().ElementId, view3d);

            // Create lines in the document
            List<CreatedElementsInfo> createdElements;
            using (Transaction t = new Transaction(doc, $"print lines"))
            {
                t.Start();
                createdElements = lineDrawingService.DrawLines(insolationPoints.First(), sunCoordinates).ToList();
                t.Commit();
            }

            // Share created elements info's with other commands
            ShareResult(createdElements);

            return Result.Succeeded;
        }

        /// <summary>
        /// Get sun position data for calculations.
        /// </summary>
        /// <remarks>
        /// TODO: refactoring: DRY with <see cref="CalculateInsolationCommand"/> and SRS.
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
        /// Adds created elements info into the shared context for other commands.
        /// </summary>
        /// <remarks>
        /// Mainly for <see cref="DeleteCreatedElementsCommand"/> for erasing.
        /// </remarks>
        private void ShareResult(IEnumerable<CreatedElementsInfo> createdElements)
        {
            List<CreatedElementsInfo> preresult = globalContextManager.GetResult<List<CreatedElementsInfo>>(SharedContextKeys.CreatedElementsId);
            preresult.AddRange(createdElements);
            globalContextManager.SubmitResult(SharedContextKeys.CreatedElementsId, preresult);
        }

        /// <summary>
        /// Initializes command dependencies via provider and global context.
        /// </summary>
        public void Init(ExternalCommandData commandData)
        {
            uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;
            view3d = View3DHelper.GetView3D(doc);
            globalContextManager = ServiceProvider.GetIGlobalContextManager();
            config = globalContextManager.GetResult<Configuration>(SharedContextKeys.Configuration);
            sunPositionService = ServiceProvider.GetSunPositionServiceFactory().Create(config, doc);
            insolationPointService = ServiceProvider.GetInsolationPointServiceFactory().Create(doc);
            selection = SelectionHelper.GetSelection(uidoc);
        }

        /// <summary>
        /// Basic precondition check to ensure services and 3D view are valid.
        /// </summary>
        public bool IsValid()
        {
            if (selection.GetElementIds().Count() != 0 && view3d != null) return true;
            return false;
        }
    }
}
