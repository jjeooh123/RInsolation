using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.Config;
using Insolation.ElementExtractor;
using Insolation.InsolationCalculator;
using Insolation.NS_SunPosition;
using Insolation.Rendering;
using Insolation.XYZExtractor;

// TODO (for all app):
// 1. Consider move innerFactory creation in AutoFactories
//    from constructor to method (SRS + makes it easier to use decorators)
// 2. In Auto factories change type of inner factory from concrete to interface.

namespace Insolation
{
    /// <summary>
    /// main entry point for application.
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Application : IExternalApplication
    {
        private static String addinAssmeblyPath = typeof(Application).Assembly.Location;

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            INIT(); 
            CreateRibbonButtons(application);
            return Result.Succeeded;
        }

        /// <summary>
        /// Create panels and buttons in revit intreface.
        /// </summary>
        /// <param name="application"></param>
        public void CreateRibbonButtons(UIControlledApplication application)
        {
            application.CreateRibbonTab("Insolation");

            RibbonPanel ExecutionsPanel = application.CreateRibbonPanel("Insolation", "Run");
            RibbonPanel AddsPanel = application.CreateRibbonPanel("Insolation", "Add");
            RibbonPanel SettingsPanel = application.CreateRibbonPanel("Insolation", "Settings");
            RibbonPanel ViewsPanel = application.CreateRibbonPanel("Insolation", "View");
            RibbonPanel UtilitiesPanel = application.CreateRibbonPanel("Insolation", "Utility");

            PushButtonData CalculateAllInsolationButtonData = new PushButtonData(
                "CalculateAll",
                "Расчитать всё",
                addinAssmeblyPath,
                "Insolation.Commnads.CalculateInsolationCommand"
            );
            PushButton CalculateAllInsolationButton = ExecutionsPanel.AddItem(CalculateAllInsolationButtonData) as PushButton;
            CalculateAllInsolationButton.ToolTip = "Расчить инсоляцию для всех объектов инсоляции";

            PushButtonData CalculateObeAndPrintButtonData = new PushButtonData(
                "CalculateOne",
                "Построить лучи",
                addinAssmeblyPath,
                "Insolation.Commnads.DrawSunLinesCommand"
            );
            PushButton CalculateObeAndPrintButton = ExecutionsPanel.AddItem(CalculateObeAndPrintButtonData) as PushButton;
            CalculateObeAndPrintButton.ToolTip = "Построить лучи для выбранного элемента";

            PushButtonData buttonData = new PushButtonData(
                "ConfigButton",
                "Настройки",
                addinAssmeblyPath,
                "Insolation.Commnads.ShowSettingwindowCommand"
            );
            PushButton button = SettingsPanel.AddItem(buttonData) as PushButton;
            button.ToolTip = "Открыть настройки конфигурации";

            PushButtonData GetElementButtonData = new PushButtonData(
                "GetElement",
                "Выбрать элемент",
                addinAssmeblyPath,
                "Insolation.Commnads.ExtractElementsBySelectionCommand"
            );
            PushButton GetElementButton = AddsPanel.AddItem(GetElementButtonData) as PushButton;
            GetElementButton.ToolTip = "Добавить элемент в список инсоляций";

            PushButtonData GetElementsByFamilyNameButtonData = new PushButtonData(
                "GetElementByFN",
                "Выбрать семейство",
                addinAssmeblyPath,
                "Insolation.Commnads.ExtractElementsByFamilyNameCommand"
            );
            PushButton GetElementsByFamilyNameButton = AddsPanel.AddItem(GetElementsByFamilyNameButtonData) as PushButton;
            GetElementsByFamilyNameButton.ToolTip = "Добавить семейство в список инсоляций";

            PushButtonData SelectElementsInWindowButtonData = new PushButtonData(
                "SelectElementsInWindow",
                "Показать время элем.",
                addinAssmeblyPath,
                "Insolation.Commnads.FocusOnElementInResultCommand"
            );
            PushButton SelectElementsInWindowButton = ViewsPanel.AddItem(SelectElementsInWindowButtonData) as PushButton;
            SelectElementsInWindowButton.ToolTip = "выделить выбранный элемеент в окне результатов";

            PushButtonData PrintResultButtonData = new PushButtonData(
                "PrintResults",
                "Вывести результаты",
                addinAssmeblyPath,
                "Insolation.Commnads.ShowResultCommand"
            );
            PushButton PrintResultButton = ViewsPanel.AddItem(PrintResultButtonData) as PushButton;
            PrintResultButton.ToolTip = "Вывести все результаты";

            PushButtonData DeleteCreatedElementsButtonData = new PushButtonData(
                "DeleteCreatedElementsCommand",
                "Удалить построения",
                addinAssmeblyPath,
                "Insolation.Commnads.DeleteCreatedElementsCommand"
            );
            PushButton DeleteCreatedElementsButton = UtilitiesPanel.AddItem(DeleteCreatedElementsButtonData) as PushButton;
            DeleteCreatedElementsButton.ToolTip = "Удалить созданные элементы";
        }

        /// <summary>
        /// initialization factories and services for application.
        /// </summary>
        public void INIT()
        {
            var globalContextManager = new GlobalContextManager();
            globalContextManager.SubmitResult(SharedContextKeys.Configuration, new Configuration());
            globalContextManager.SubmitResult(SharedContextKeys.ElementIds, new HashSet<ElementId>());
            globalContextManager.SubmitResult(SharedContextKeys.ExecutedInsolation, new List<ExecutedInsolationPoint>());
            globalContextManager.SubmitResult(SharedContextKeys.CreatedElementsId, new List<CreatedElementsInfo>());

            ISunPositionServiceFactory sunPositionServiceFactory = new SunPositionServiceFactory();
            ISunPositionServiceFactory SunPositionServiceFactoryForDrawLines = new SunPositionServiceFactoryForDrawLines(sunPositionServiceFactory, 30.0);

            IExecutedInsolationPointServiceFactory executedInsolationPointServiceFactory =
                new ExecutedInsolationPointServiceFactory();
            IInsolationPointServiceFactory InsolationPointServiceFactory =
                new InsolationPointServiceFactory();
            ILinesDrawingServiceFactory linesDrawingServiceFactory = new LinesDrawingServiceFactory();
            IElementExtractoFactory elementExtractorFactory = new ElementExtractorFactory();
            //IResultWindowManager resultWindowManager = new ResultWindowManager();

            ServiceLocator.Register<IGlobalContextManager>(globalContextManager);
            ServiceLocator.Register<ISunPositionServiceFactory>(sunPositionServiceFactory, "SunPositionServiceFactory");
            ServiceLocator.Register<ISunPositionServiceFactory>(SunPositionServiceFactoryForDrawLines, "SunPositionServiceFactoryForDrawLines");

            ServiceLocator.Register<IExecutedInsolationPointServiceFactory>(executedInsolationPointServiceFactory);
            ServiceLocator.Register<IInsolationPointServiceFactory>(InsolationPointServiceFactory);
            ServiceLocator.Register<ILinesDrawingServiceFactory>(linesDrawingServiceFactory);
            ServiceLocator.Register<IElementExtractoFactory>(elementExtractorFactory);
            //ServiceLocator.Register<IResultWindowManager>(resultWindowManager);

        }
    }
}
