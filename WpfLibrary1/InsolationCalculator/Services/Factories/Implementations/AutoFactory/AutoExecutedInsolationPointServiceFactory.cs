using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.ElementExtractor;
using Insolation.Helpers;

namespace Insolation.InsolationCalculator
{
    /// <summary>
    /// Implemantation of <see cref="IExecutedInsolationPointServiceFactory"/>,
    /// where are the required dependencies taken from global context manager.
    /// </summary>
    public class AutoExecutedInsolationPointServiceFactory : IExecutedInsolationPointServiceFactory
    {
        private Document document;
        private IEnumerable<ElementId> insolationPointIds;
        private View3D view3D;

        /// <summary>
        /// Provides factories and shared services needed by the module.
        /// </summary>
        public virtual GlobalContextProviderBase serviceProvider { get; set; } = new InsolationPointServiceFactoryContextProvider();

        private ExecutedInsolationPointServiceFactory innerFactory;

        /// <summary>
        /// Creates instance of <see cref="AutoExecutedInsolationPointServiceFactory"/>.
        /// </summary>
        public AutoExecutedInsolationPointServiceFactory()
        {
            var globalContextManager = serviceProvider.GetIGlobalContextManager();

            document = globalContextManager
                .GetResult<ExternalCommandData>(SharedContextKeys.ExternalCommandData)
                .Application.ActiveUIDocument.Document;
            insolationPointIds = globalContextManager
                .GetResult<HashSet<ElementId>>(SharedContextKeys.ElementIds);
            view3D = View3DHelper.GetView3D(document);

            innerFactory = new ExecutedInsolationPointServiceFactory(document, insolationPointIds, view3D);
        }

        ///<inheritdoc/>
        public IExecutedInsolationPointService Create() => innerFactory.Create();
    }
}
