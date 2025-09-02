using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.ElementExtractor;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Implemantation of <see cref="IXYZExtractorFactoryResolver"/>,
    /// where active Revit documnet is taken from global context.
    /// </summary>
    public class AutoXYZExtractorFactoryResolver : IXYZExtractorFactoryResolver
    {
        private readonly Document document;
        private XYZExtractorFactoryResolver innerFactoryResolver;

        /// <summary>
        /// Provides factories and shared services needed by the module.
        /// </summary>
        public virtual GlobalContextProviderBase serviceProvider { get; set; } = new DefaultXYZExtractorContextProvider();

        /// <summary>
        /// Creates instance of <see cref="AutoXYZExtractorFactoryResolver"/>
        /// </summary>
        public AutoXYZExtractorFactoryResolver()
        {
            document = serviceProvider.GetIGlobalContextManager()
                .GetResult<ExternalCommandData>(SharedContextKeys.ExternalCommandData)
                .Application.ActiveUIDocument.Document;

            innerFactoryResolver = new XYZExtractorFactoryResolver(document);
        }

        /// <inheritdoc/>
        public IXYZExtractorFactory Resolve(XYZExtractionStrategy strategy) => innerFactoryResolver.Resolve(strategy);
    }
}
