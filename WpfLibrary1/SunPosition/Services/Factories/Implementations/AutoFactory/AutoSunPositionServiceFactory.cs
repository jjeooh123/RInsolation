using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.Config;
using Insolation.ElementExtractor;

namespace Insolation.NS_SunPosition
{
    /// <summary>
    /// Implemantation of <see cref="ISunPositionServiceFactory"/>,
    /// where dependencies is taken from global context.
    /// </summary>
    public class AutoSunPositionServiceFactory : ISunPositionServiceFactory
    {
        private Configuration config;
        private Document doc;

        /// <summary>
        /// Provides factories and shared services needed by the module.
        /// </summary>
        public virtual GlobalContextProviderBase serviceProvider { get; set; } = new SunPositionServiceFactoryContextProvider();

        protected SunPositionServiceFactory innerFactory;

        /// <summary>
        /// Creates instance of <see cref="AutoSunPositionServiceFactory"/>.
        /// </summary>
        public AutoSunPositionServiceFactory()
        {
            var globalContextManager = serviceProvider.GetIGlobalContextManager();

            doc = globalContextManager
                .GetResult<ExternalCommandData>(SharedContextKeys.ExternalCommandData)
                .Application.ActiveUIDocument.Document;

            config = globalContextManager.GetResult<Configuration>(SharedContextKeys.Configuration);

            innerFactory = new SunPositionServiceFactory(config, doc);
        }

        ///<inheritdoc/>
        public ISunPositionService Create() => innerFactory.Create();
    }
}
