using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.ElementExtractor;
using Insolation.Helpers;

namespace Insolation.Rendering
{
    /// <summary>
    /// Implemantation of <see cref="ILinesDrawingServiceFactory"/>,
    /// where are the required dependencies taken from global context manager.
    /// </summary>
    public class AutoLinesDrawingServiceFactory : ILinesDrawingServiceFactory
    {
        private Document doc;
        private IEnumerable<ElementId> insolationPointIds;
        private View3D view3D;

        /// <summary>
        /// Provides factories and shared services needed by the module.
        /// </summary>
        public virtual GlobalContextProviderBase serviceProvider { get; set; } = new LinesDrawingServiceFactoryContextProvider();

        private ILinesDrawingServiceFactory innerFactory;

        /// <inheritdoc/>
        public ILinesDrawingService Create()
        {
            var globalContextManager = serviceProvider.GetIGlobalContextManager();

            doc = globalContextManager
                .GetResult<ExternalCommandData>(SharedContextKeys.ExternalCommandData)
                .Application.ActiveUIDocument.Document;
            insolationPointIds = globalContextManager
                .GetResult<HashSet<ElementId>>(SharedContextKeys.ElementIds);
            view3D = View3DHelper.GetView3D(doc);

            innerFactory = new LinesDrawingServiceFactory(doc, insolationPointIds, view3D);
            return innerFactory.Create();
        }
    }
}
